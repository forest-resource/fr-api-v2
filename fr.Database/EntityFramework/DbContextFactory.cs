using fr.Database.EntityFramework.Configuration;
using fr.Database.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace fr.Database.EntityFramework
{
    public class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        private readonly IAuditService auditService;
        private AppDbContext _dbContext;
        private readonly object _lockDbContext = new();

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
