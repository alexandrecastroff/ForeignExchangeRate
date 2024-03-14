namespace Application.Messaging.Models
{
    using Microsoft.EntityFrameworkCore;

    public class ForeignExchangeRateDbContext : DbContext
    {
        public ForeignExchangeRateDbContext(DbContextOptions<ForeignExchangeRateDbContext> options) : base(options) { }

        public DbSet<ForeignExchangeRateCreated> ForeignExchangeRateUpdates { get; set; }
    }
}
