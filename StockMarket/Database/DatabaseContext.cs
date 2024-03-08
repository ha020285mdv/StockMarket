using Microsoft.EntityFrameworkCore;
using StockMarket.Database.Model;

namespace StockMarket.Database;

public class DatabaseContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }

    public DbSet<UserPortfolioEntity> UsersPortfolios { get; set; }

    public DbSet<SecurityEntity> Securities { get; set; }

    public DbSet<OrderHistoryEntity> OrdersHistory { get; set; }

    public DbSet<OrderFillEntity> OrderFills { get; set; }

    public DatabaseContext(DbContextOptions options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserPortfolioEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SecurityEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderHistoryEntityConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderFillEntityConfiguration).Assembly);
    }
}
