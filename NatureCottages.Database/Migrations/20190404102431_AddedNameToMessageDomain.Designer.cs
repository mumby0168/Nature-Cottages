﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NatureCottages.Database.Persitance;

namespace NatureCottages.Database.Migrations
{
    [DbContext(typeof(CottageDbContext))]
    [Migration("20190404102431_AddedNameToMessageDomain")]
    partial class AddedNameToMessageDomain
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("NatureCottages.Database.Domain.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountType");

                    b.Property<byte[]>("Password");

                    b.Property<byte[]>("Salt");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("NatureCottages.Database.Domain.Attraction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("ImageGroupId");

                    b.Property<bool>("IsVisibleToClient");

                    b.Property<string>("Link")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ImageGroupId");

                    b.ToTable("Attractions");
                });

            modelBuilder.Entity("NatureCottages.Database.Domain.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CottageId");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.Property<bool>("IsPendingApproval");

                    b.Property<double>("TotalPrice");

                    b.HasKey("Id");

                    b.HasIndex("CottageId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("NatureCottages.Database.Domain.ClientMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateClosed");

                    b.Property<DateTime>("DateSent");

                    b.Property<string>("Email");

                    b.Property<string>("Message");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ClientMessages");
                });

            modelBuilder.Entity("NatureCottages.Database.Domain.Cottage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<int>("ImageGroupId");

                    b.Property<bool>("IsVisibleToClient");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("PricePerNight");

                    b.HasKey("Id");

                    b.HasIndex("ImageGroupId");

                    b.ToTable("Cottages");
                });

            modelBuilder.Entity("NatureCottages.Database.Domain.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<int>("PhoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("NatureCottages.Database.Domain.FacebookPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PostUrl")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("FacebookPosts");
                });

            modelBuilder.Entity("NatureCottages.Database.Domain.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ImageGroupId");

                    b.Property<string>("ImagePath");

                    b.HasKey("Id");

                    b.HasIndex("ImageGroupId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("NatureCottages.Database.Domain.ImageGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("ImageGroups");
                });

            modelBuilder.Entity("NatureCottages.Database.Domain.PasswordReset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("Code");

                    b.Property<string>("Email");

                    b.Property<DateTime>("RequestCreated");

                    b.HasKey("Id");

                    b.ToTable("PasswordResets");
                });

            modelBuilder.Entity("NatureCottages.Database.Domain.Attraction", b =>
                {
                    b.HasOne("NatureCottages.Database.Domain.ImageGroup", "ImageGroup")
                        .WithMany()
                        .HasForeignKey("ImageGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NatureCottages.Database.Domain.Booking", b =>
                {
                    b.HasOne("NatureCottages.Database.Domain.Cottage", "Cottage")
                        .WithMany("Bookings")
                        .HasForeignKey("CottageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NatureCottages.Database.Domain.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NatureCottages.Database.Domain.Cottage", b =>
                {
                    b.HasOne("NatureCottages.Database.Domain.ImageGroup", "ImageGroup")
                        .WithMany()
                        .HasForeignKey("ImageGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NatureCottages.Database.Domain.Customer", b =>
                {
                    b.HasOne("NatureCottages.Database.Domain.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NatureCottages.Database.Domain.Image", b =>
                {
                    b.HasOne("NatureCottages.Database.Domain.ImageGroup")
                        .WithMany("Images")
                        .HasForeignKey("ImageGroupId");
                });
#pragma warning restore 612, 618
        }
    }
}