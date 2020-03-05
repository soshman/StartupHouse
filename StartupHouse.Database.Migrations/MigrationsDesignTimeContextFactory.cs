using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using StartupHouse.Database.Entities;
using System.IO;

namespace StartupHouse.Database.Migrations
{
    public class MigrationsDesignTimeContextFactory : IDesignTimeDbContextFactory<ShDbContext>
    {
        public ShDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ShDbContext>();
            dbContextOptionsBuilder.UseSqlServer(config["ConnectionString"], b => b.MigrationsAssembly("StartupHouse.Database.Migrations"));

            return new ShDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
