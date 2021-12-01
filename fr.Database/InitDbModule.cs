using Autofac;
using fr.Database.EntityFramework;
using fr.Database.Services;
using Microsoft.EntityFrameworkCore.Design;

namespace fr.Database
{
    public class InitDbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<AuditService>()
                .As<IAuditService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DbContextFactory>()
                .As<IDesignTimeDbContextFactory<AppDbContext>>()
                .InstancePerLifetimeScope();
        }
    }
}
