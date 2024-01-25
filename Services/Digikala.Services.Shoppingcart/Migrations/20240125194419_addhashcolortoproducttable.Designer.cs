﻿// <auto-generated />
using System;
using Digikala.Services.Shoppingcart.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Digikala.Services.Shoppingcart.Migrations
{
    [DbContext(typeof(ShoppingcartDatacontext))]
    [Migration("20240125194419_addhashcolortoproducttable")]
    partial class addhashcolortoproducttable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Digikala.Services.Shoppingcart.Models.CartHeader", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Userid")
                        .HasColumnType("int");

                    b.Property<int>("digicouponId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("cartHeaders");
                });

            modelBuilder.Entity("Digikala.Services.Shoppingcart.Models.Cartdetail", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int?>("Headeridid")
                        .HasColumnType("int");

                    b.Property<int?>("productidid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Headeridid");

                    b.HasIndex("productidid");

                    b.ToTable("cartdetails");
                });

            modelBuilder.Entity("Digikala.Services.Shoppingcart.Models.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("hashColor")
                        .HasColumnType("int");

                    b.Property<int>("productid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Digikala.Services.Shoppingcart.Models.Cartdetail", b =>
                {
                    b.HasOne("Digikala.Services.Shoppingcart.Models.CartHeader", "Headerid")
                        .WithMany()
                        .HasForeignKey("Headeridid");

                    b.HasOne("Digikala.Services.Shoppingcart.Models.Product", "productid")
                        .WithMany()
                        .HasForeignKey("productidid");

                    b.Navigation("Headerid");

                    b.Navigation("productid");
                });
#pragma warning restore 612, 618
        }
    }
}
