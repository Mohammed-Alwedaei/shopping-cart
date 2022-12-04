﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingCart.API.DbContexts;

#nullable disable

namespace ShoppingCart.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221203173123_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ShoppingCart.API.Models.ProductModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ShoppingCartDetailsId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShoppingCartDetailsModelId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingCartDetailsModelId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ShoppingCart.API.Models.ShoppingCartDetailsModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ShoppingCartDetailsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ShoppingCartHeaderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingCartDetailsId");

                    b.ToTable("ShoppingCartDetails");
                });

            modelBuilder.Entity("ShoppingCart.API.Models.ShoppingCartHeaderModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShippingAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShoppingCartDetailsId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShoppingCartHeaders");
                });

            modelBuilder.Entity("ShoppingCart.API.Models.ProductModel", b =>
                {
                    b.HasOne("ShoppingCart.API.Models.ShoppingCartDetailsModel", null)
                        .WithMany("Products")
                        .HasForeignKey("ShoppingCartDetailsModelId");
                });

            modelBuilder.Entity("ShoppingCart.API.Models.ShoppingCartDetailsModel", b =>
                {
                    b.HasOne("ShoppingCart.API.Models.ShoppingCartHeaderModel", null)
                        .WithMany("ShoppingCartDetails")
                        .HasForeignKey("ShoppingCartDetailsId");
                });

            modelBuilder.Entity("ShoppingCart.API.Models.ShoppingCartDetailsModel", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ShoppingCart.API.Models.ShoppingCartHeaderModel", b =>
                {
                    b.Navigation("ShoppingCartDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
