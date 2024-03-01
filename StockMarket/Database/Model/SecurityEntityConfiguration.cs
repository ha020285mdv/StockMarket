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
            .HasKey(x => x.Id);
        builder
            .Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .Property(x => x.Symbol)
            .IsRequired();

        builder
            .Property(x => x.Name)
            .IsRequired();

        builder
            .HasMany<UserPortfolioEntity>(e => e.Portfolio)
            .WithOne(e => e.Security)
            .HasForeignKey(e => e.IdSecurity)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder
            .HasMany<OrderEntity>(e => e.OrderBooks)
            .WithOne(e => e.Security)
            .HasForeignKey(e => e.IdSecurity)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
