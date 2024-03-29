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
    [Migration("20190812082728_giantt-v11")]
    partial class gianttv11
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                            Name = "Heo"
                        },
                        new
                        {
                            CategoryId = 2,
                            Name = "Gà"
                        },
                        new
                        {
                            CategoryId = 3,
                            Name = "Bò"
                        });
                });

            modelBuilder.Entity("DTO.Entities.DistributorFood", b =>
                {
                    b.Property<int>("FoodId");

                    b.Property<int>("PremisesId");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("DBO.dReturnDate(getdate())");

                    b.HasKey("FoodId", "PremisesId");

                    b.HasIndex("PremisesId");

                    b.ToTable("DistributorFood");
                });

            modelBuilder.Entity("DTO.Entities.Feeding", b =>
                {
                    b.Property<int>("FeedingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreateById");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("DBO.dReturnDate(getdate())");

                    b.Property<bool>("IsDelete")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PremisesId");

                    b.Property<int>("UpdateById");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("FeedingId");

                    b.HasIndex("CreateById");

                    b.HasIndex("PremisesId");

                    b.HasIndex("UpdateById");

                    b.ToTable("Feeding");
                });

            modelBuilder.Entity("DTO.Entities.FeedingFood", b =>
                {
                    b.Property<int>("FoodId");

                    b.Property<int>("FeedingId");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("DBO.dReturnDate(getdate())");

                    b.HasKey("FoodId", "FeedingId");

                    b.HasIndex("FeedingId");

                    b.ToTable("FeedingFood");
                });

            modelBuilder.Entity("DTO.Entities.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Breed")
                        .IsRequired();

                    b.Property<int>("CategoryId");

                    b.Property<int>("CreateById");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("DBO.dReturnDate(getdate())");

                    b.Property<int>("FarmId");

                    b.HasKey("FoodId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreateById");

                    b.HasIndex("FarmId");

                    b.ToTable("Food");
                });

            modelBuilder.Entity("DTO.Entities.FoodDetail", b =>
                {
                    b.Property<int>("DetailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlockNumber");

                    b.Property<int>("CreateById");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("DBO.dReturnDate(getdate())");

                    b.Property<int>("FoodId");

                    b.Property<string>("TransactionHash")
                        .IsRequired();

                    b.Property<int>("TypeId");

                    b.HasKey("DetailId");

                    b.HasIndex("CreateById");

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
                            Name = "Thức ăn"
                        },
                        new
                        {
                            TypeId = 3,
                            Name = "Vac-xin"
                        },
                        new
                        {
                            TypeId = 4,
                            Name = "Kiểm định"
                        },
                        new
                        {
                            TypeId = 5,
                            Name = "Nhà cung cấp"
                        },
                        new
                        {
                            TypeId = 6,
                            Name = "Quy trình xử lý"
                        },
                        new
                        {
                            TypeId = 7,
                            Name = "Đóng gói"
                        });
                });

            modelBuilder.Entity("DTO.Entities.Premises", b =>
                {
                    b.Property<int>("PremisesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("DBO.dReturnDate(getdate())");

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

            modelBuilder.Entity("DTO.Entities.ProviderFood", b =>
                {
                    b.Property<int>("FoodId");

                    b.Property<int>("PremisesId");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("DBO.dReturnDate(getdate())");

                    b.Property<int?>("TreatmentId");

                    b.HasKey("FoodId", "PremisesId");

                    b.HasIndex("PremisesId");

                    b.HasIndex("TreatmentId");

                    b.ToTable("ProviderFood");
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

                    b.Property<int?>("CreateById");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("DBO.dReturnDate(getdate())");

                    b.Property<int>("FoodId");

                    b.Property<string>("ReceiverComment");

                    b.Property<int>("ReceiverId");

                    b.Property<int?>("RejectById");

                    b.Property<string>("RejectReason");

                    b.Property<int>("SenderId");

                    b.Property<int>("StatusId");

                    b.Property<DateTime?>("VerifyDate");

                    b.Property<string>("VeterinaryComment");

                    b.Property<int?>("VeterinaryId");

                    b.HasKey("TransactionId");

                    b.HasIndex("CreateById");

                    b.HasIndex("FoodId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("RejectById");

                    b.HasIndex("SenderId");

                    b.HasIndex("StatusId");

                    b.HasIndex("VeterinaryId");

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

                    b.Property<int>("CreateById");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("DBO.dReturnDate(getdate())");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PremisesId");

                    b.Property<int?>("TreatmentParentId");

                    b.Property<int>("UpdateById");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("TreatmentId");

                    b.HasIndex("CreateById");

                    b.HasIndex("PremisesId");

                    b.HasIndex("TreatmentParentId");

                    b.HasIndex("UpdateById");

                    b.ToTable("Treatment");
                });

            modelBuilder.Entity("DTO.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActivationCode");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("DBO.dReturnDate(getdate())");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Fullname")
                        .IsRequired();

                    b.Property<string>("Image")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("/app-assets/images/avatar.jpg");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool?>("IsConfirmEmail");

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

            modelBuilder.Entity("DTO.Entities.Vaccine", b =>
                {
                    b.Property<int>("VaccineId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreateById");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("DBO.dReturnDate(getdate())");

                    b.Property<bool>("IsDelete")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PremisesId");

                    b.Property<int>("UpdateById");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("VaccineId");

                    b.HasIndex("CreateById");

                    b.HasIndex("PremisesId");

                    b.HasIndex("UpdateById");

                    b.ToTable("Vaccin");
                });

            modelBuilder.Entity("DTO.Entities.VaccineFood", b =>
                {
                    b.Property<int>("FoodId");

                    b.Property<int>("VaccineId");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("DBO.dReturnDate(getdate())");

                    b.HasKey("FoodId", "VaccineId");

                    b.HasIndex("VaccineId");

                    b.ToTable("VaccineFood");
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

            modelBuilder.Entity("DTO.Entities.Feeding", b =>
                {
                    b.HasOne("DTO.Entities.User", "CreateBy")
                        .WithMany("UserCreatedFeedings")
                        .HasForeignKey("CreateById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Premises", "Premises")
                        .WithMany()
                        .HasForeignKey("PremisesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.User", "UpdateBy")
                        .WithMany("UserUpdateFeedings")
                        .HasForeignKey("UpdateById")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DTO.Entities.FeedingFood", b =>
                {
                    b.HasOne("DTO.Entities.Feeding", "Feeding")
                        .WithMany("FeedingFoods")
                        .HasForeignKey("FeedingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Food", "Food")
                        .WithMany("FeedingFoods")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DTO.Entities.Food", b =>
                {
                    b.HasOne("DTO.Entities.Category", "Category")
                        .WithMany("Foods")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.User", "CreateBy")
                        .WithMany("UserCreatedFoods")
                        .HasForeignKey("CreateById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Premises", "Farm")
                        .WithMany("FarmFoods")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DTO.Entities.FoodDetail", b =>
                {
                    b.HasOne("DTO.Entities.User", "CreateBy")
                        .WithMany("UserCreateFoodDetails")
                        .HasForeignKey("CreateById")
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

            modelBuilder.Entity("DTO.Entities.ProviderFood", b =>
                {
                    b.HasOne("DTO.Entities.Food", "Food")
                        .WithMany("ProviderFoods")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Premises", "Premises")
                        .WithMany("ProviderFoods")
                        .HasForeignKey("PremisesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Treatment", "Treatment")
                        .WithMany("ProviderFoods")
                        .HasForeignKey("TreatmentId");
                });

            modelBuilder.Entity("DTO.Entities.Transaction", b =>
                {
                    b.HasOne("DTO.Entities.User", "CreateBy")
                        .WithMany("UserCreateTransactions")
                        .HasForeignKey("CreateById");

                    b.HasOne("DTO.Entities.Food", "Food")
                        .WithMany("Transactions")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Premises", "Receiver")
                        .WithMany("ReceiverTransactions")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DTO.Entities.User", "RejectBy")
                        .WithMany("RejectByTransactions")
                        .HasForeignKey("RejectById");

                    b.HasOne("DTO.Entities.Premises", "Sender")
                        .WithMany("SenderTransactions")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DTO.Entities.TransactionStatus", "TransactionStatus")
                        .WithMany("Transactions")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.User", "Veterinary")
                        .WithMany("VeterinaryTransactions")
                        .HasForeignKey("VeterinaryId");
                });

            modelBuilder.Entity("DTO.Entities.Treatment", b =>
                {
                    b.HasOne("DTO.Entities.User", "CreateBy")
                        .WithMany("UserCreatedTreatments")
                        .HasForeignKey("CreateById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Premises", "Premises")
                        .WithMany()
                        .HasForeignKey("PremisesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Treatment", "TreatmentParent")
                        .WithMany("Treatments")
                        .HasForeignKey("TreatmentParentId");

                    b.HasOne("DTO.Entities.User", "UpdateBy")
                        .WithMany("UserUpdateTreatments")
                        .HasForeignKey("UpdateById")
                        .OnDelete(DeleteBehavior.Cascade);
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

            modelBuilder.Entity("DTO.Entities.Vaccine", b =>
                {
                    b.HasOne("DTO.Entities.User", "CreateBy")
                        .WithMany("UserCreatedVaccins")
                        .HasForeignKey("CreateById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Premises", "Premises")
                        .WithMany()
                        .HasForeignKey("PremisesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.User", "UpdateBy")
                        .WithMany("UserUpdateVaccins")
                        .HasForeignKey("UpdateById")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DTO.Entities.VaccineFood", b =>
                {
                    b.HasOne("DTO.Entities.Food", "Food")
                        .WithMany("VaccineFoods")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DTO.Entities.Vaccine", "Vaccine")
                        .WithMany("VaccineFoods")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
