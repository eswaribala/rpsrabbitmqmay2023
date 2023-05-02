﻿// <auto-generated />
using System;
using InventoryAPI.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryAPI.Migrations
{
    [DbContext(typeof(InventoryContext))]
    partial class InventoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("InventoryAPI.Models.Catalog", b =>
                {
                    b.Property<long>("CatalogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Catalog_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CatalogId"), 1L, 1);

                    b.Property<string>("CatalogName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Catalog_Name");

                    b.HasKey("CatalogId");

                    b.ToTable("Catalog");
                });

            modelBuilder.Entity("InventoryAPI.Models.Product", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Product_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ProductId"), 1L, 1);

                    b.Property<long>("CatalogId")
                        .HasColumnType("bigint")
                        .HasColumnName("Catalog_Id_FK");

                    b.HasKey("ProductId");

                    b.HasIndex("CatalogId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("InventoryAPI.Models.Product", b =>
                {
                    b.HasOne("InventoryAPI.Models.Catalog", "Catalog")
                        .WithMany()
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("InventoryAPI.Models.ProductDescription", "ProductDescription", b1 =>
                        {
                            b1.Property<long>("ProductId")
                                .HasColumnType("bigint");

                            b1.Property<DateTime>("DOP")
                                .HasColumnType("datetime2")
                                .HasColumnName("Purchase_Date");

                            b1.Property<string>("Description")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Description");

                            b1.Property<int>("ProductType")
                                .HasColumnType("int")
                                .HasColumnName("Product_Type");

                            b1.Property<long>("UnitPrice")
                                .HasColumnType("bigint")
                                .HasColumnName("Unit_Price");

                            b1.HasKey("ProductId");

                            b1.ToTable("Product");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Catalog");

                    b.Navigation("ProductDescription");
                });
#pragma warning restore 612, 618
        }
    }
}