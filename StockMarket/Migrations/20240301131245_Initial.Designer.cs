﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockMarket.Database;

#nullable disable

namespace StockMarket.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240301131245_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StockMarket.Database.Model.OrderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Action")
                        .HasColumnType("int");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdSecurity")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Volume")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdSecurity");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("StockMarket.Database.Model.OrderFillEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdBuyOrder")
                        .HasColumnType("int");

                    b.Property<int>("IdSellOrder")
                        .HasColumnType("int");

                    b.Property<bool>("IsSettled")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Volume")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdBuyOrder");

                    b.HasIndex("IdSellOrder");

                    b.ToTable("OrderFills");
                });

            modelBuilder.Entity("StockMarket.Database.Model.SecurityEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Securities");
                });

            modelBuilder.Entity("StockMarket.Database.Model.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Funds")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18, 2)")
                        .HasDefaultValue(0.0m);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("XAPIKey")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StockMarket.Database.Model.UserPortfolioEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdSecurity")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdSecurity");

                    b.HasIndex("IdUser");

                    b.ToTable("UsersPortfolios");
                });

            modelBuilder.Entity("StockMarket.Database.Model.OrderEntity", b =>
                {
                    b.HasOne("StockMarket.Database.Model.SecurityEntity", "Security")
                        .WithMany("OrderBooks")
                        .HasForeignKey("IdSecurity")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Security");
                });

            modelBuilder.Entity("StockMarket.Database.Model.OrderFillEntity", b =>
                {
                    b.HasOne("StockMarket.Database.Model.OrderEntity", "BuyOrder")
                        .WithMany("BuyOrderFills")
                        .HasForeignKey("IdBuyOrder")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StockMarket.Database.Model.OrderEntity", "SellOrder")
                        .WithMany("SellOrderFills")
                        .HasForeignKey("IdSellOrder")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BuyOrder");

                    b.Navigation("SellOrder");
                });

            modelBuilder.Entity("StockMarket.Database.Model.UserPortfolioEntity", b =>
                {
                    b.HasOne("StockMarket.Database.Model.SecurityEntity", "Security")
                        .WithMany("Portfolio")
                        .HasForeignKey("IdSecurity")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StockMarket.Database.Model.UserEntity", "User")
                        .WithMany("Portfolio")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Security");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StockMarket.Database.Model.OrderEntity", b =>
                {
                    b.Navigation("BuyOrderFills");

                    b.Navigation("SellOrderFills");
                });

            modelBuilder.Entity("StockMarket.Database.Model.SecurityEntity", b =>
                {
                    b.Navigation("OrderBooks");

                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("StockMarket.Database.Model.UserEntity", b =>
                {
                    b.Navigation("Portfolio");
                });
#pragma warning restore 612, 618
        }
    }
}