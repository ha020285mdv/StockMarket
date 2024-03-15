using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockMarket.Database.Model;
using System.Reflection.Metadata;

namespace StockMarket.Database.Model;

public class ActiveOrdersEntityConfiguration : IEntityTypeConfiguration<ActiveOrdersEntity>
{
    public void Configure(EntityTypeBuilder<ActiveOrdersEntity> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasOne(e => e.Security)
            .WithMany(e => e.ActiveOrders)
            .HasForeignKey(e => e.Ticker)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.User)
            .WithMany(e => e.ActiveOrders)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(x => x.Action)
            .HasConversion<int>()
            .IsRequired();

        builder
            .Property(x => x.CreatedDate)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("GETUTCDATE()");  

        builder
            .Property(x => x.ModifiedDate)
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql("GETUTCDATE()");  

        builder
            .Property(x => x.VolumeRemaining)
            .IsRequired();

        builder
            .Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18, 2)");

        builder
            .Property(x => x.ExpirationDate)
            .IsRequired();

        builder
            .Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder
            .HasOne(e => e.OrderHistory)                       
            .WithOne(e => e.ActiveOrder)
            .HasForeignKey<OrderHistoryEntity>(e => e.Id)
            .IsRequired();

    }
}

