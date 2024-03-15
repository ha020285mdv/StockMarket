using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockMarket.Database.Model;
using System.Reflection.Metadata;

namespace StockMarket.Database.Model;

public class UserPortfolioEntityConfiguration : IEntityTypeConfiguration<UserPortfolioEntity>
{
    public void Configure(EntityTypeBuilder<UserPortfolioEntity> builder)
    {
        builder
            .HasKey(x => new { x.UserId, x.Ticker });

        builder
            .Property(x => x.Number)
            .IsRequired();

        builder
            .HasOne(e => e.User)
            .WithMany(e => e.Portfolio)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.Security)
            .WithMany(e => e.Portfolio)
            .HasForeignKey(e => e.Ticker)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
