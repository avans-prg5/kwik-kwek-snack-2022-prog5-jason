﻿// <auto-generated />
using System;
using KwikKwekSnack.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KwikKwekSnack.Domain.Migrations
{
    [DbContext(typeof(KwikKwekSnackContext))]
    [Migration("20221029172711_removed_foreign_key_EF_SnackOrder_DrinkOrder_to_Order")]
    partial class removed_foreign_key_EF_SnackOrder_DrinkOrder_to_Order
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("KwikKwekSnack.Domain.Drink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MinimalPrice")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Drinks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = false,
                            Description = "Bruine frisdrank met koolzuur",
                            ImageURL = "https://imgs.search.brave.com/s1tlzTSdN6odOOFO1fLwEPOUyq4gnuw6DfxckSH0ylM/rs:fit:1000:1000:1/g:ce/aHR0cHM6Ly9henRl/Y21leGljYW5wcm9k/dWN0c2FuZGxpcXVv/ci5jb20uYXUvd3At/Y29udGVudC91cGxv/YWRzLzIwMjAvMDUv/UmVkLUNvbGEtY2Fu/cy1henRlYy1tZXhp/Y2FuLmpwZw",
                            MinimalPrice = 2.5,
                            Name = "Cola"
                        },
                        new
                        {
                            Id = 2,
                            Active = false,
                            Description = "Water zonder koolzuur",
                            MinimalPrice = 1.5,
                            Name = "Spa Blauw"
                        },
                        new
                        {
                            Id = 3,
                            Active = false,
                            Description = "Water met koolzuur",
                            MinimalPrice = 1.5,
                            Name = "Spa Rood"
                        },
                        new
                        {
                            Id = 4,
                            Active = false,
                            Description = "Chocolademelk",
                            MinimalPrice = 3.0,
                            Name = "Chocomel"
                        });
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.DrinkExtra", b =>
                {
                    b.Property<int>("ExtraId")
                        .HasColumnType("int");

                    b.Property<int>("DrinkId")
                        .HasColumnType("int");

                    b.HasKey("ExtraId", "DrinkId");

                    b.HasIndex("DrinkId");

                    b.ToTable("DrinkExtras");

                    b.HasData(
                        new
                        {
                            ExtraId = 1,
                            DrinkId = 1
                        },
                        new
                        {
                            ExtraId = 2,
                            DrinkId = 1
                        },
                        new
                        {
                            ExtraId = 1,
                            DrinkId = 2
                        },
                        new
                        {
                            ExtraId = 1,
                            DrinkId = 3
                        },
                        new
                        {
                            ExtraId = 3,
                            DrinkId = 4
                        });
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.DrinkOrder", b =>
                {
                    b.Property<int>("DrinkOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DrinkOrderId"), 1L, 1);

                    b.Property<int>("DrinkId")
                        .HasColumnType("int");

                    b.Property<int>("DrinkSizeId")
                        .HasColumnType("int");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("DrinkOrderId");

                    b.HasIndex("DrinkId");

                    b.HasIndex("DrinkSizeId");

                    b.HasIndex("OrderId");

                    b.ToTable("DrinkOrders");
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.DrinkOrderExtra", b =>
                {
                    b.Property<int>("ExtraId")
                        .HasColumnType("int");

                    b.Property<int>("DrinkOrderId")
                        .HasColumnType("int");

                    b.HasKey("ExtraId", "DrinkOrderId");

                    b.HasIndex("DrinkOrderId");

                    b.ToTable("DrinkOrderExtras");
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.DrinkSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PriceMultiplier")
                        .HasColumnType("float");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.ToTable("DrinkSizes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FullName = "Small",
                            PriceMultiplier = 1.0,
                            ShortName = "S"
                        },
                        new
                        {
                            Id = 2,
                            FullName = "Medium",
                            PriceMultiplier = 1.25,
                            ShortName = "M"
                        },
                        new
                        {
                            Id = 3,
                            FullName = "Large",
                            PriceMultiplier = 1.5,
                            ShortName = "L"
                        });
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.Extra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Extras");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = false,
                            Name = "IJsklontjes",
                            Price = 0.10000000000000001
                        },
                        new
                        {
                            Id = 2,
                            Active = false,
                            Name = "Rietje",
                            Price = 0.14999999999999999
                        },
                        new
                        {
                            Id = 3,
                            Active = false,
                            Name = "Slagroom",
                            Price = 0.20000000000000001
                        },
                        new
                        {
                            Id = 4,
                            Active = false,
                            Name = "Sla",
                            Price = 0.25
                        },
                        new
                        {
                            Id = 5,
                            Active = false,
                            Name = "Mayonnaise",
                            Price = 0.25
                        });
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeliveryType")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.Snack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("StandardPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Snacks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = false,
                            Description = "Rundvlees hamburger",
                            ImageURL = "https://imgs.search.brave.com/DyyLM6KzO1StGQnV_w3sPjLgSZyelGWt7GQcDkUDXqA/rs:fit:612:539:1/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vcGhvdG9z/L2ZyZXNoLWJ1cmdl/ci1pc29sYXRlZC1w/aWN0dXJlLWlkMTEy/NTE0OTE4Mz9rPTYm/bT0xMTI1MTQ5MTgz/JnM9NjEyeDYxMiZ3/PTAmaD1LeFNmVlVr/M0tQM0JnSFZZYm95/TDlhUkxIcC1mUlly/ZlBjRmVhMHc2OE93/PQ",
                            Name = "Beef Burger",
                            StandardPrice = 2.5
                        },
                        new
                        {
                            Id = 2,
                            Active = false,
                            Description = "Gefrituurde aardappelen",
                            Name = "Friet",
                            StandardPrice = 1.5
                        },
                        new
                        {
                            Id = 3,
                            Active = false,
                            Description = "Gefrituurde vleesrol",
                            ImageURL = "https://imgs.search.brave.com/zjRHgiREOLZtvFh-TiPRqiUPLR_1JGBTiErIRUY89UI/rs:fit:800:568:1/g:ce/aHR0cHM6Ly90aHVt/YnMuZHJlYW1zdGlt/ZS5jb20vYi9mcmlr/YW5kZWwtdW0tcGV0/aXNjby1ob2xhbmQl/QzMlQUFzLXRyYWRp/Y2lvbmFsLW1laW8t/Y2FjaG9ycm8tcXVl/bnRlLXRyaXR1cmFk/by1kYS1jYXJuZS1w/ciVDMyVCM3hpbW8t/YWNpbWEtZG8tMTUy/MTc2MzMwLmpwZw",
                            Name = "Frikandel",
                            StandardPrice = 1.0
                        },
                        new
                        {
                            Id = 4,
                            Active = false,
                            Description = "Pizza met plakjes salami",
                            ImageURL = "http://www.clker.com/cliparts/3/9/1/d/1451508004467611065wallpaper-sliced-pizza.jpg",
                            Name = "Pizza Salami",
                            StandardPrice = 5.0
                        });
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.SnackExtra", b =>
                {
                    b.Property<int>("ExtraId")
                        .HasColumnType("int");

                    b.Property<int>("SnackId")
                        .HasColumnType("int");

                    b.HasKey("ExtraId", "SnackId");

                    b.HasIndex("SnackId");

                    b.ToTable("SnackExtras");

                    b.HasData(
                        new
                        {
                            ExtraId = 4,
                            SnackId = 1
                        },
                        new
                        {
                            ExtraId = 5,
                            SnackId = 1
                        },
                        new
                        {
                            ExtraId = 5,
                            SnackId = 2
                        },
                        new
                        {
                            ExtraId = 5,
                            SnackId = 3
                        });
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.SnackOrder", b =>
                {
                    b.Property<int>("SnackOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SnackOrderId"), 1L, 1);

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("SnackId")
                        .HasColumnType("int");

                    b.HasKey("SnackOrderId");

                    b.HasIndex("OrderId");

                    b.HasIndex("SnackId");

                    b.ToTable("SnackOrders");
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.SnackOrderExtra", b =>
                {
                    b.Property<int>("ExtraId")
                        .HasColumnType("int");

                    b.Property<int>("SnackOrderId")
                        .HasColumnType("int");

                    b.HasKey("ExtraId", "SnackOrderId");

                    b.HasIndex("SnackOrderId");

                    b.ToTable("SnackOrderExtras");
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.DrinkExtra", b =>
                {
                    b.HasOne("KwikKwekSnack.Domain.Drink", "Drink")
                        .WithMany("AvailableExtras")
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KwikKwekSnack.Domain.Extra", "Extra")
                        .WithMany("ExtraOfDrink")
                        .HasForeignKey("ExtraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drink");

                    b.Navigation("Extra");
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.DrinkOrder", b =>
                {
                    b.HasOne("KwikKwekSnack.Domain.Drink", "Drink")
                        .WithMany()
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KwikKwekSnack.Domain.DrinkSize", "DrinkSize")
                        .WithMany()
                        .HasForeignKey("DrinkSizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KwikKwekSnack.Domain.Order", null)
                        .WithMany("DrinkOrders")
                        .HasForeignKey("OrderId");

                    b.Navigation("Drink");

                    b.Navigation("DrinkSize");
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.DrinkOrderExtra", b =>
                {
                    b.HasOne("KwikKwekSnack.Domain.DrinkOrder", "DrinkOrder")
                        .WithMany("ChosenExtras")
                        .HasForeignKey("DrinkOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KwikKwekSnack.Domain.Extra", "Extra")
                        .WithMany("ExtraOfDrinkOrder")
                        .HasForeignKey("ExtraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DrinkOrder");

                    b.Navigation("Extra");
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.SnackExtra", b =>
                {
                    b.HasOne("KwikKwekSnack.Domain.Extra", "Extra")
                        .WithMany("ExtraOfSnack")
                        .HasForeignKey("ExtraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KwikKwekSnack.Domain.Snack", "Snack")
                        .WithMany("AvailableExtras")
                        .HasForeignKey("SnackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Extra");

                    b.Navigation("Snack");
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.SnackOrder", b =>
                {
                    b.HasOne("KwikKwekSnack.Domain.Order", null)
                        .WithMany("SnackOrders")
                        .HasForeignKey("OrderId");

                    b.HasOne("KwikKwekSnack.Domain.Snack", "Snack")
                        .WithMany()
                        .HasForeignKey("SnackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Snack");
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.SnackOrderExtra", b =>
                {
                    b.HasOne("KwikKwekSnack.Domain.Extra", "Extra")
                        .WithMany("ExtraOfSnackOrder")
                        .HasForeignKey("ExtraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KwikKwekSnack.Domain.SnackOrder", "SnackOrder")
                        .WithMany("ChosenExtras")
                        .HasForeignKey("SnackOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Extra");

                    b.Navigation("SnackOrder");
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.Drink", b =>
                {
                    b.Navigation("AvailableExtras");
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.DrinkOrder", b =>
                {
                    b.Navigation("ChosenExtras");
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.Extra", b =>
                {
                    b.Navigation("ExtraOfDrink");

                    b.Navigation("ExtraOfDrinkOrder");

                    b.Navigation("ExtraOfSnack");

                    b.Navigation("ExtraOfSnackOrder");
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.Order", b =>
                {
                    b.Navigation("DrinkOrders");

                    b.Navigation("SnackOrders");
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.Snack", b =>
                {
                    b.Navigation("AvailableExtras");
                });

            modelBuilder.Entity("KwikKwekSnack.Domain.SnackOrder", b =>
                {
                    b.Navigation("ChosenExtras");
                });
#pragma warning restore 612, 618
        }
    }
}
