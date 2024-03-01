using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockMarket.Database.Model;
using System.Reflection.Metadata;

namespace StockMarket.Database.Model;

public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .Property(x => x.Action)
            .HasConversion<int>()
            .IsRequired();

        builder
            .Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder
            .Property(x => x.Volume)
            .IsRequired();

        builder
            .Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");

        builder
            .Property(x => x.Expires)
            .IsRequired();

        builder
            .HasOne(e => e.Security)
            .WithMany(e => e.OrderBooks)
            .HasForeignKey(e => e.IdSecurity)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany<OrderFillEntity>(e => e.BuyOrderFills)
            .WithOne(e => e.BuyOrder)
            .HasForeignKey(e => e.IdBuyOrder)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder
            .HasMany<OrderFillEntity>(e => e.SellOrderFills)
            .WithOne(e => e.SellOrder)
            .HasForeignKey(e => e.IdSellOrder)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

    }
}

