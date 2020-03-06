using Microsoft.EntityFrameworkCore;
using StartupHouse.Database.Entities.dbo;

namespace StartupHouse.Database.Entities
{
    public class ShDbContext : DbContext
    {
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<CurrencyPrice> CurrencyPrices { get; set; }

        public ShDbContext() : base()
        {
        }

        public ShDbContext(DbContextOptions<ShDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>()
                .HasMany(c => c.Prices)
                .WithOne(cp => cp.Currency)
                .HasForeignKey(cp => cp.CurrencyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CurrencyPrice>()
                .HasKey(cp => new { cp.CurrencyId, cp.Day });

            modelBuilder.Entity<Currency>()
                .HasData(
                    new Currency()
                    {
                        Id = 1,
                        Code = "EUR",
                        Name = "Euro"
                    },
                    new Currency()
                    {
                        Id = 2,
                        Code = "USD",
                        Name = "Dolar amerykański"
                    });
            }
    }
}
