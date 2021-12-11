using fr.Database.EntityFramework.Configuration;
using fr.Database.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace fr.Database.EntityFramework
{
    public interface IDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        DbContext DbContext { get; }
    }

    public class DbContextFactory : IDesignTimeDbContextFactory
    {
        private readonly IAuditService auditService;
        private AppDbContext _dbContext;
        private readonly object _lockDbContext = new();

        public DbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    return CreateDbContext(Array.Empty<string>());
                }
                return _dbContext;
            }
        }

        public DbContextFactory(IAuditService auditService)
        {
            this.auditService = auditService;
        }

        public DbContextFactory()
        {
            auditService = new AuditService
            {
                UserName = "System"
            };
        }

        public AppDbContext CreateDbContext(string[] args)
        {
            if (_dbContext == null)
            {
                lock (_lockDbContext)
                {
                    var builder = new DbContextOptionsBuilder<AppDbContext>();

                    var configuration = AppConfiguration.Get(DirectoryFinder.CalculateContentRootFolder());

                    builder.UseAppDbContext(configuration);

                    _dbContext = new AppDbContext(builder.Options, auditService);
                }
            }

            return _dbContext;
        }
    }
}
