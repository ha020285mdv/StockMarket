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
            .HasKey(x => x.Id);
        builder
            .Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
            .Property(x => x.Number)
            .IsRequired();

        builder
            .HasOne(e => e.User)
            .WithMany(e => e.Portfolio)
            .HasForeignKey(e => e.IdUser)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.Security)
            .WithMany(e => e.Portfolio)
            .HasForeignKey(e => e.IdSecurity)
            .OnDelete(DeleteBehavior.Restrict);
    }
}



