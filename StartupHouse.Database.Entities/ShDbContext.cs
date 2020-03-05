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
                .HasKey(c => c.Id); //This works out-of-the-box without this entry. Just for consistency.

            modelBuilder.Entity<Currency>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Currency>()
                .HasMany(c => c.Prices)
                .WithOne(cp => cp.Currency)
                .HasForeignKey(cp => cp.CurrencyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CurrencyPrice>()
                .HasKey(cp => new { cp.CurrencyId, cp.Day });
        }
    }
}
