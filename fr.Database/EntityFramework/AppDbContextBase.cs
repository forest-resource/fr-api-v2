using fr.Core.Timing;
using fr.Database.Model.Entities.Users;
using fr.Database.Model.Interfaces;
using fr.Database.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace fr.Database.EntityFramework
{
    public class AppDbContextBase : IdentityDbContext<Users, Roles, Guid>
    {
        private IAuditService AuditService { get; set; }
        public AppDbContextBase(DbContextOptions options, IAuditService auditService) : base(options)
        {
            AuditService = auditService;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var type in builder.Model.GetEntityTypes())
            {
                if (type.BaseType != null || !typeof(IDeletedModel).IsAssignableFrom(type.ClrType))
                {
                    continue;
                }

                builder.Entity(type.ClrType)
                       .HasQueryFilter(GetSoftDeleteFilter(type.ClrType));
            }
        }

        public override int SaveChanges()
        {
            ApplyDatabaseConcepts();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyDatabaseConcepts();
            return base.SaveChangesAsync(cancellationToken);
        }

        private static LambdaExpression GetSoftDeleteFilter(Type type)
        {
            var parameter = Expression.Parameter(type);
            var property = Expression.Property(parameter, nameof(IDeletedModel.IsDeleted));
            var falseConstant = Expression.Constant(false);
            var expression = Expression.Equal(property, falseConstant);

            return Expression.Lambda(expression, parameter);
        }

        private void ApplyDatabaseConcepts()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                MapState(entry).Invoke();
            }
        }

        private Action MapState(EntityEntry entry) => entry.State switch
        {
            EntityState.Added => () => SetCreationAuditProperties(entry.Entity),
            EntityState.Modified => () => SetModificationAuditProperties(entry.Entity),
            EntityState.Deleted => () => SetDeletionAuditProperties(entry),
            EntityState.Detached => () => { }
            ,
            EntityState.Unchanged => () => { }
            ,
            _ => () => throw new ArgumentOutOfRangeException(entry.State.ToString())
        };

        private void SetDeletionAuditProperties(EntityEntry entry)
        {
            if (entry.Entity is not IDeletedModel model)
            {
                return;
            }

            entry.State = EntityState.Unchanged;
            model.IsDeleted = true;
            model.DeletedTime = Clock.Now;
            model.DeletedBy = AuditService.UserName;
        }

        private void SetModificationAuditProperties(object entity)
        {
            if (entity is IUpdatedModel model)
            {
                model.UpdatedTime = Clock.Now;
                model.UpdatedBy = AuditService.UserName;
            }
        }

        private void SetCreationAuditProperties(object entity)
        {
            if (entity is ICreatedModel model)
            {
                model.CreatedTime = Clock.Now;
                model.CreatedBy = AuditService.UserName;
            }
            SetModificationAuditProperties(entity);
        }
    }
}
