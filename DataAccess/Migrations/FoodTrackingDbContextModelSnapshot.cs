﻿// <auto-generated />
using System;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(FoodTrackingDbContext))]
    partial class FoodTrackingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DTO.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("CategoryId");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Name = "Thịt Heo"
                        },
                        new
                        {
                            CategoryId = 2,
                            Name = "Thịt Gà"
                        },
                        new
                        {
                            CategoryId = 3,
                            Name = "Thịt Bò"
                        });
                });

            modelBuilder.Entity("DTO.Entities.DistributorFood", b =>
                {
                    b.Property<int>("FoodId");

                    b.Property<int>("PremisesId");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("FoodId", "PremisesId");

                    b.HasIndex("PremisesId");

                    b.ToTable("DistributorFood");
                });

            modelBuilder.Entity("DTO.Entities.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Breed")
                        .IsRequired();

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("FarmId");

                    b.Property<bool>("IsCertification")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsFeeding")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsPackaging")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsTreatment")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<bool>("IsVaccination")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int?>("ProviderId");

                    b.Property<int?>("TreatmentId");

                    b.HasKey("FoodId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FarmId");

                    b.HasIndex("ProviderId");

                    b.HasIndex("TreatmentId");

                    b.ToTable("Food");
                });

            modelBuilder.Entity("DTO.Entities.FoodDetail", b =>
                {
                    b.Property<int>("DetailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlockNumber");

                    b.Property<int>("CreatedById");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("FoodId");

                    b.Property<string>("TransactionHash")
                        .IsRequired();

                    b.Property<int>("TypeId");

                    b.HasKey("DetailId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("FoodId");

                    b.HasIndex("TypeId");

                    b.ToTable("FoodDetail");
                });

            modelBuilder.Entity("DTO.Entities.FoodDetailType", b =>
                {
                    b.Property<int>("TypeId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("TypeId");

                    b.ToTable("FoodDetailType");

                    b.HasData(
                        new
                        {
                            TypeId = 1,
                            Name = "Tạo mới"
                        },
                        new
                        {
                            TypeId = 2,
                            Name = "Thêm thông tin thức ăn"
                        },
                        new
                        {
                            TypeId = 3,
                            Name = "Thêm thông tin vac-xin"
                        },
                        new
                        {
                            TypeId = 4,
                            Name = "Thêm thông tin kiểm định"
                        },
                        new
                        {
                            TypeId = 5,
                            Name = "Thêm nhà cung cấp"
                        },
                        new
                        {
                            TypeId = 6,
                            Name = "Thêm quy trình xử lý"
                        },
                        new
                        {
                            TypeId = 7,
                            Name = "Thêm thông tin đóng gói"
                        });
                });

            modelBuilder.Entity("DTO.Entities.Premises", b =>
                {
                    b.Property<int>("PremisesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("TypeId");

                    b.HasKey("PremisesId");

                    b.HasIndex("TypeId");

                    b.ToTable("Premises");
                });

            modelBuilder.Entity("DTO.Entities.PremisesType", b =>
                {
                    b.Property<int>("TypeId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("TypeId");

                    b.ToTable("PremisesType");

                    b.HasData(
                        new
                        {
                            TypeId = 1,
                            Name = "Farm"
                        },
                        new
                        {
                            TypeId = 2,
                            Name = "Provider"
                        },
                        new
                        {
                            TypeId = 3,
                            Name = "Distributor"
                        });
                });

            modelBuilder.Entity("DTO.Entities.RegisterInfo", b =>
                {
                    b.Property<int>("RegisterId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Fullname")
                        .IsRequired();

                    b.Property<bool?>("IsConfirm");

                    b.Property<string>("Phone");

                    b.Property<string>("PremisesAddress")
                        .IsRequired();

                    b.Property<string>("PremisesName")
                        .IsRequired();

                    b.Property<int>("PremisesTypeId");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("RegisterId");

                    b.HasIndex("PremisesTypeId");

                    b.ToTable("RegisterInfo");
                });

            modelBuilder.Entity("DTO.Entities.Role", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            RoleId = 2,
                            Name = "Manager"
                        },
                        new
                        {
                            RoleId = 3,
                            Name = "Staff"
                        });
                });

            modelBuilder.Entity("DTO.Entities.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ConfirmDate");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("FarmId");

                    b.Property<int>("FoodId");

                    b.Property<int>("ProviderId");

                    b.Property<int>("StatusId");

                    b.HasKey("TransactionId");

                    b.HasIndex("FarmId");

                    b.HasIndex("FoodId");

                    b.HasIndex("ProviderId");

                    b.HasIndex("StatusId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("DTO.Entities.TransactionStatus", b =>
                {
                    b.Property<int>("StatusId");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.HasKey("StatusId");

                    b.ToTable("TransactionStatus");

                    b.HasData(
                        new
                        {
                            StatusId = 1,
                            Status = "Đã Tạo"
                        },
                        new
                        {
                            StatusId = 2,
                            Status = "Đã Kiểm Định"
                        },
                        new
                        {
                            StatusId = 3,
                            Status = "Đã Xác Nhận"
                        },
                        new
                        {
                            StatusId = 4,
                            Status = "Đã Từ Chối"
                        });
                });

            modelBuilder.Entity("DTO.Entities.Treatment", b =>
                {
                    b.Property<int>("TreatmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PremisesId");

                    b.Property<int?>("TreatmentParentId");

                    b.HasKey("TreatmentId");

                    b.HasIndex("PremisesId");

                    b.HasIndex("TreatmentParentId");

                    b.ToTable("Treatment");
                });

            modelBuilder.Entity("DTO.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Fullname")
                        .IsRequired();

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("PhoneNo");

                    b.Property<int?>("PremisesId");

                    b.Property<int>("RoleId");

                    b.Property<string>("Salt")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.HasIndex("PremisesId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DTO.Entities.DistributorFood", b =>
                {
                    b.HasOne("DTO.Entities.Food", "Food")
                        .WithMany("DistributorFoods")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Premises", "Premises")
                        .WithMany("DistributorFoods")
                        .HasForeignKey("PremisesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DTO.Entities.Food", b =>
                {
                    b.HasOne("DTO.Entities.Category", "Category")
                        .WithMany("Foods")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Premises", "Farm")
                        .WithMany("FarmFoods")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DTO.Entities.Premises", "Provider")
                        .WithMany("ProviderFoods")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DTO.Entities.Treatment", "Treatment")
                        .WithMany("Foods")
                        .HasForeignKey("TreatmentId");
                });

            modelBuilder.Entity("DTO.Entities.FoodDetail", b =>
                {
                    b.HasOne("DTO.Entities.User", "CreatedBy")
                        .WithMany("UserCreatedFoodDetails")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Food", "Food")
                        .WithMany("FoodDetails")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.FoodDetailType", "Type")
                        .WithMany("FoodDetails")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DTO.Entities.Premises", b =>
                {
                    b.HasOne("DTO.Entities.PremisesType", "PremisesType")
                        .WithMany("Premises")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DTO.Entities.RegisterInfo", b =>
                {
                    b.HasOne("DTO.Entities.PremisesType", "PremisesType")
                        .WithMany("RegisterInfos")
                        .HasForeignKey("PremisesTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DTO.Entities.Transaction", b =>
                {
                    b.HasOne("DTO.Entities.Premises", "Farm")
                        .WithMany("FarmTransactions")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DTO.Entities.Food", "Food")
                        .WithMany("Transactions")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Premises", "Provider")
                        .WithMany("ProviderTransactions")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DTO.Entities.TransactionStatus", "TransactionStatus")
                        .WithMany("Transactions")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DTO.Entities.Treatment", b =>
                {
                    b.HasOne("DTO.Entities.Premises", "Premises")
                        .WithMany()
                        .HasForeignKey("PremisesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Treatment", "TreatmentParent")
                        .WithMany("Treatments")
                        .HasForeignKey("TreatmentParentId");
                });

            modelBuilder.Entity("DTO.Entities.User", b =>
                {
                    b.HasOne("DTO.Entities.Premises", "Premises")
                        .WithMany("Users")
                        .HasForeignKey("PremisesId");

                    b.HasOne("DTO.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
