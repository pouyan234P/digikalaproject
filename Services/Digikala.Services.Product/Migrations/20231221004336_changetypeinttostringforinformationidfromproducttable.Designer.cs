﻿// <auto-generated />
using System;
using Digikala.Services.Product.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Digikala.Services.Product.Migrations
{
    [DbContext(typeof(Digikalaproduct))]
    [Migration("20231221004336_changetypeinttostringforinformationidfromproducttable")]
    partial class changetypeinttostringforinformationidfromproducttable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Digikala.Services.Product.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryParent")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("Digikala.Services.Product.Models.Pointofview", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Commenttext")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Commenttitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Negativepoints")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Positivepoints")
                        .HasColumnType("varbinary(max)");

                    b.Property<double>("Score")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.ToTable("pointofviews");
                });

            modelBuilder.Entity("Digikala.Services.Product.Models.Products", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryidID")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Informationid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Insurance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nameforushghah")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.HasIndex("CategoryidID");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Digikala.Services.Product.Models.Questionandanswer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Productidid")
                        .HasColumnType("int");

                    b.Property<string>("Question")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Useranswerid")
                        .HasColumnType("int");

                    b.Property<int>("Userquestionid")
                        .HasColumnType("int");

                    b.Property<string>("answer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Productidid");

                    b.ToTable("questionandanswers");
                });

            modelBuilder.Entity("Digikala.Services.Product.Models.UserPoint", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Pointofiviewidid")
                        .HasColumnType("int");

                    b.Property<int?>("Productidid")
                        .HasColumnType("int");

                    b.Property<int>("Userid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Pointofiviewidid");

                    b.HasIndex("Productidid");

                    b.ToTable("userPoints");
                });

            modelBuilder.Entity("Digikala.Services.Product.Models.Products", b =>
                {
                    b.HasOne("Digikala.Services.Product.Models.Category", "Categoryid")
                        .WithMany()
                        .HasForeignKey("CategoryidID");

                    b.Navigation("Categoryid");
                });

            modelBuilder.Entity("Digikala.Services.Product.Models.Questionandanswer", b =>
                {
                    b.HasOne("Digikala.Services.Product.Models.Products", "Productid")
                        .WithMany()
                        .HasForeignKey("Productidid");

                    b.Navigation("Productid");
                });

            modelBuilder.Entity("Digikala.Services.Product.Models.UserPoint", b =>
                {
                    b.HasOne("Digikala.Services.Product.Models.Pointofview", "Pointofiviewid")
                        .WithMany()
                        .HasForeignKey("Pointofiviewidid");

                    b.HasOne("Digikala.Services.Product.Models.Products", "Productid")
                        .WithMany()
                        .HasForeignKey("Productidid");

                    b.Navigation("Pointofiviewid");

                    b.Navigation("Productid");
                });
#pragma warning restore 612, 618
        }
    }
}
