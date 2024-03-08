using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockMarket.Database.Model;
using System.Reflection.Metadata;

namespace StockMarket.Database.Model;

public class SecurityEntityConfiguration : IEntityTypeConfiguration<SecurityEntity>
{
    public void Configure(EntityTypeBuilder<SecurityEntity> builder)
    {
        builder
            .HasKey(x => x.Ticker);
        builder
            .Property(x => x.Ticker)
            .IsRequired();
        builder
            .Property(x => x.Name)
            .IsRequired();

        builder
            .HasMany<UserPortfolioEntity>(e => e.Portfolio)
            .WithOne(e => e.Security)
            .HasForeignKey(e => e.SecurityTicker)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder
            .HasMany<OrderHistoryEntity>(e => e.OrderBooks)
            .WithOne(e => e.Security)
            .HasForeignKey(e => e.Ticker)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
