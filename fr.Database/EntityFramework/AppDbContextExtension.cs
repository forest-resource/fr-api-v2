using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace fr.Database.EntityFramework
{
    public static class AppDbContextExtension
    {
        public static DbContextOptionsBuilder UseAppDbContext(this DbContextOptionsBuilder<AppDbContext> builder, IConfiguration configuration)
            => (builder as DbContextOptionsBuilder).UseAppDbContext(configuration);
        public static DbContextOptionsBuilder UseAppDbContext(this DbContextOptionsBuilder builder, IConfiguration configuration)
        {
            builder.UseNpgsql(configuration.GetConnectionString("default"))
                .ReplaceService<IDesignTimeDbContextFactory<AppDbContext>, DbContextFactory>();
            return builder;
        }
    }
}
