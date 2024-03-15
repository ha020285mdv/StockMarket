using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockMarket.Database.Model;
using System.Reflection.Metadata;

namespace StockMarket.Database.Model;

public class OrderHistoryEntityConfiguration : IEntityTypeConfiguration<OrderHistoryEntity>
{
    public void Configure(EntityTypeBuilder<OrderHistoryEntity> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()");

        builder
            .HasOne(e => e.Security)
            .WithMany(e => e.OrderHistory)
            .HasForeignKey(e => e.Ticker)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.User)
            .WithMany(e => e.Orders)
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
            .Property(x => x.VolumeRequested)
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
            .HasOne(e => e.ActiveOrder)
            .WithOne(e => e.OrderHistory)
            .HasForeignKey<ActiveOrdersEntity>(e => e.Id);

    }
}

