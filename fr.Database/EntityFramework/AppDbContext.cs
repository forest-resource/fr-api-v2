using fr.Database.Model.Entities.Icons;
using fr.Database.Model.Entities.Plots;
using fr.Database.Model.Entities.Trees;
using fr.Database.Services;
using Microsoft.EntityFrameworkCore;

namespace fr.Database.EntityFramework
{
    public class AppDbContext : AppDbContextBase
    {
        public DbSet<Icon> Icons { get; set; }
        public DbSet<Tree> Trees { get; set; }
        public DbSet<TreeDetail> TreeDetails { get; set; }
        public DbSet<Plot> Plots { get; set; }
        public DbSet<PlotPoint> PlotPoints { get; set; }

        public AppDbContext(DbContextOptions options, IAuditService auditService)
            : base(options, auditService)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Plot>()
                .HasMany(r => r.PlotPoints)
                .WithOne(r => r.Plot)
                .HasForeignKey(r => r.PlotId);

            builder.Entity<Tree>()
                .HasMany(r => r.TreeDetails)
                .WithOne(r => r.Tree)
                .HasForeignKey(r => r.TreeId);
        }
    }
}
