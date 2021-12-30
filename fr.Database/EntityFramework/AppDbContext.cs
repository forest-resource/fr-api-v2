using fr.Database.Model.Entities.Icons;
using fr.Database.Services;
using Microsoft.EntityFrameworkCore;

namespace fr.Database.EntityFramework
{
    public class AppDbContext : AppDbContextBase
    {
        public DbSet<Icon> Icons { get; set; }
        public AppDbContext(DbContextOptions options, IAuditService auditService)
            : base(options, auditService)
        {
        }
    }
}
