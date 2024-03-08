using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StockMarket.Database.Model;
using System.Reflection.Metadata;

namespace StockMarket.Database.Model;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()");

        builder
            .Property(x => x.XAPIKey)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(x => x.Funds)
            .IsRequired()
            .HasColumnType("decimal(18, 2)")
            .HasDefaultValue(0.0m);

        builder
            .HasMany(r => r.Portfolio)
            .WithOne(f => f.User)
            .HasForeignKey(f => f.IdUser);

        builder
            .HasMany<UserPortfolioEntity>(e => e.Portfolio)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.IdUser)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
