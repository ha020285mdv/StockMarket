using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockMarket.Database.Model;
using System.Reflection.Metadata;

namespace StockMarket.Database.Model;

public class OrderFillEntityConfiguration : IEntityTypeConfiguration<OrderFillEntity>
{
    public void Configure(EntityTypeBuilder<OrderFillEntity> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .Property(x => x.Volume)
            .IsRequired();

        builder
            .Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");

        builder
            .Property(x => x.IsSettled)
            .IsRequired();

        builder
            .HasOne(e => e.BuyOrder)
            .WithMany(e => e.BuyOrderFills)
            .HasForeignKey(e => e.IdBuyOrder)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.SellOrder)
            .WithMany(e => e.SellOrderFills)
            .HasForeignKey(e => e.IdSellOrder)
            .OnDelete(DeleteBehavior.Restrict);
    }
}



