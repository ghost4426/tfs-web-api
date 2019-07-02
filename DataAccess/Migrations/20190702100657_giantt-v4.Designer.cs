﻿// <auto-generated />
using System;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(FoodTrackingDbContext))]
    [Migration("20190702100657_giantt-v4")]
    partial class gianttv4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DTO.Entities.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DTO.Entities.DistributorFood", b =>
                {
                    b.Property<int>("FoodId");

                    b.Property<int>("PremisesId");

                    b.HasKey("FoodId", "PremisesId");

                    b.HasIndex("FoodId")
                        .IsUnique();

                    b.HasIndex("PremisesId")
                        .IsUnique();

                    b.ToTable("DistributorFood");
                });

            modelBuilder.Entity("DTO.Entities.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoriesId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("FarmerId");

                    b.Property<bool>("IsCertification");

                    b.Property<bool>("IsFeeding");

                    b.Property<bool>("IsPackaging");

                    b.Property<bool>("IsTreatment");

                    b.Property<bool>("IsVaccination");

                    b.Property<int?>("ProviderId");

                    b.Property<int?>("TreatmentId");

                    b.HasKey("FoodId");

                    b.HasIndex("CategoriesId");

                    b.HasIndex("FarmerId");

                    b.HasIndex("ProviderId");

                    b.HasIndex("TreatmentId");

                    b.ToTable("Food");
                });

            modelBuilder.Entity("DTO.Entities.Premises", b =>
                {
                    b.Property<int>("PremisesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Name");

                    b.Property<int>("TypeId");

                    b.HasKey("PremisesId");

                    b.HasIndex("TypeId");

                    b.ToTable("Premises");
                });

            modelBuilder.Entity("DTO.Entities.PremisesType", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Name");

                    b.HasKey("TypeId");

                    b.ToTable("PremisesType");
                });

            modelBuilder.Entity("DTO.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("DTO.Entities.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ConfirmDate");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("FarmerId");

                    b.Property<int>("FoodId");

                    b.Property<int>("ProviderId");

                    b.Property<int>("StatusId");

                    b.HasKey("TransactionId");

                    b.HasIndex("FarmerId");

                    b.HasIndex("FoodId");

                    b.HasIndex("ProviderId");

                    b.HasIndex("StatusId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("DTO.Entities.TransactionStatus", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Status");

                    b.HasKey("StatusId");

                    b.ToTable("TransactionStatus");
                });

            modelBuilder.Entity("DTO.Entities.Treatment", b =>
                {
                    b.Property<int>("TreatmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("ParentTreatmentId");

                    b.Property<int>("PremisesId");

                    b.HasKey("TreatmentId");

                    b.HasIndex("PremisesId");

                    b.ToTable("Treatment");
                });

            modelBuilder.Entity("DTO.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<string>("Fullname");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNo");

                    b.Property<int?>("PremisesId");

                    b.Property<int>("RoleId");

                    b.Property<string>("Salt");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.HasIndex("PremisesId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DTO.Entities.DistributorFood", b =>
                {
                    b.HasOne("DTO.Entities.Food", "Food")
                        .WithOne("DistributorFood")
                        .HasForeignKey("DTO.Entities.DistributorFood", "FoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Premises", "Premises")
                        .WithOne("DistributorFood")
                        .HasForeignKey("DTO.Entities.DistributorFood", "PremisesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DTO.Entities.Food", b =>
                {
                    b.HasOne("DTO.Entities.Categories", "Categories")
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Premises", "Farmer")
                        .WithMany()
                        .HasForeignKey("FarmerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Premises", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId");

                    b.HasOne("DTO.Entities.Treatment", "Treatment")
                        .WithMany()
                        .HasForeignKey("TreatmentId");
                });

            modelBuilder.Entity("DTO.Entities.Premises", b =>
                {
                    b.HasOne("DTO.Entities.PremisesType", "PremisesType")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DTO.Entities.Transaction", b =>
                {
                    b.HasOne("DTO.Entities.Premises", "Farmer")
                        .WithMany()
                        .HasForeignKey("FarmerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Premises", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.TransactionStatus", "TransactionStatus")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DTO.Entities.Treatment", b =>
                {
                    b.HasOne("DTO.Entities.Premises", "Premises")
                        .WithMany()
                        .HasForeignKey("PremisesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DTO.Entities.User", b =>
                {
                    b.HasOne("DTO.Entities.Premises", "Premises")
                        .WithMany()
                        .HasForeignKey("PremisesId");

                    b.HasOne("DTO.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
