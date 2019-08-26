using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DTO.Entities;
using Common.Constant;
using Common.Utils;

namespace DataAccess.Context
{
    public class FoodTrackingDbContext : DbContext
    {

        public FoodTrackingDbContext(DbContextOptions<FoodTrackingDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<FoodDetail> FoodDetail { get; set; }
        public DbSet<FoodDetailType> FoodDetailType { get; set; }
        public DbSet<Premises> Premises { get; set; }
        public DbSet<PremisesType> PremisesType { get; set; }
        public DbSet<Treatment> Treatment { get; set; }
        public DbSet<DistributorFood> DistributorFood { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<TransactionStatus> TransactionStatus { get; set; }
        public DbSet<ProviderFood> ProviderFood { get; set; }
        public DbSet<Feeding> Feeding { get; set; }
        public DbSet<Vaccine> Vaccine { get; set; }
        public DbSet<FeedingFood> FeedingFood { get; set; }
        public DbSet<VaccineFood> VaccineFood { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region User
            builder.Entity<User>()
             .Property(f => f.CreateDate)
             .HasDefaultValueSql("DBO.dReturnDate(getdate())");

            builder.Entity<User>()
             .Property(f => f.IsActive)
             .HasDefaultValue(true);

            builder.Entity<User>()
             .Property(f => f.Image)
             .HasDefaultValue("/app-assets/images/avatar.jpg");
            #endregion

            #region Food
            //builder.Entity<Food>()
            //    .HasOne(df => df.Farm)
            //    .WithMany(f => f.FarmFoods)
            //    .HasForeignKey(df => df.FarmId)
            //    .OnDelete(DeleteBehavior.);

            ////builder.Entity<Food>()
            ////    .HasOne(df => df.Provider)
            ////    .WithMany(f => f.ProviderFoods)
            ////    .HasForeignKey(df => df.ProviderId)
            ////    .OnDelete(DeleteBehavior.SetNull);

            //builder.Entity<Food>()
            //   .Property(f => f.IsCertification)
            //   .HasDefaultValue(false);

            //builder.Entity<Food>()
            //   .Property(f => f.IsFeeding)
            //   .HasDefaultValue(false);

            //builder.Entity<Food>()
            //   .Property(f => f.IsPackaging)
            //   .HasDefaultValue(false);

            //builder.Entity<Food>()
            //   .Property(f => f.IsTreatment)
            //   .HasDefaultValue(false);

            //builder.Entity<Food>()
            //   .Property(f => f.IsVaccination)
            //   .HasDefaultValue(false);

            builder.Entity<Food>()
              .Property(f => f.CreateDate)
              .HasDefaultValueSql("DBO.dReturnDate(getdate())");
            #endregion

            #region Food Detail
            builder.Entity<FoodDetail>()
             .Property(f => f.CreateDate)
             .HasDefaultValueSql("DBO.dReturnDate(getdate())");
            #endregion

            #region Premises
            builder.Entity<Premises>()
             .Property(p => p.CreateDate)
             .HasDefaultValueSql("DBO.dReturnDate(getdate())");
            builder.Entity<Premises>()
               .Property(f => f.IsActive)
               .HasDefaultValue(true);
            #endregion

            #region DistributorFood
            builder.Entity<DistributorFood>()
                .HasKey(df => new { df.FoodId, df.PremisesId });
            builder.Entity<DistributorFood>()
                .HasOne(df => df.Premises)
                .WithMany(d => d.DistributorFoods)
                .HasForeignKey(df => df.PremisesId);
            builder.Entity<DistributorFood>()
                .HasOne(df => df.Food)
                .WithMany(f => f.DistributorFoods)
                .HasForeignKey(df => df.FoodId);
            builder.Entity<DistributorFood>()
            .Property(f => f.CreateDate)
            .HasDefaultValueSql("DBO.dReturnDate(getdate())");
            #endregion

            #region ProviderFood
            builder.Entity<ProviderFood>()
                .HasKey(pf => new { pf.FoodId, pf.PremisesId });
            builder.Entity<ProviderFood>()
                .HasOne(pf => pf.Premises)
                .WithMany(p => p.ProviderFoods)
                .HasForeignKey(pf => pf.PremisesId);
            builder.Entity<ProviderFood>()
                .HasOne(pf => pf.Food)
                .WithMany(f => f.ProviderFoods)
                .HasForeignKey(pf => pf.FoodId);
            builder.Entity<ProviderFood>()
                .Property(pf => pf.IsTreatmented)
                .HasDefaultValue(false);
            builder.Entity<ProviderFood>()
                .Property(f => f.IsPacked)
                .HasDefaultValue(false);
            builder.Entity<ProviderFood>()
            .Property(f => f.CreateDate)
            .HasDefaultValueSql("DBO.dReturnDate(getdate())");
            #endregion

            #region Transaction
            builder.Entity<Transaction>()
                .HasOne(df => df.Sender)
                .WithMany(f => f.SenderTransactions)
                .HasForeignKey(df => df.SenderId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Transaction>()
                .HasOne(df => df.Receiver)
                .WithMany(f => f.ReceiverTransactions)
                .HasForeignKey(df => df.ReceiverId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Transaction>()
               .Property(f => f.CreateDate)
               .HasDefaultValueSql("DBO.dReturnDate(getdate())");

            builder.Entity<Transaction>()
                .HasOne(df => df.CreateBy)
                .WithMany(f => f.UserCreateTransactions)
                .HasForeignKey(df => df.CreateById);

            builder.Entity<Transaction>()
                .HasOne(df => df.Veterinary)
                .WithMany(f => f.VeterinaryTransactions)
                .HasForeignKey(df => df.VeterinaryId);

            builder.Entity<Transaction>()
               .HasOne(df => df.RejectBy)
               .WithMany(f => f.RejectByTransactions)
               .HasForeignKey(df => df.RejectById);

            #endregion RegisterInfo

            #region Treatment
            builder.Entity<Treatment>()
                .HasOne(df => df.CreateBy)
                .WithMany(f => f.UserCreatedTreatments)
                .HasForeignKey(df => df.CreateById);
            builder.Entity<Treatment>()
                .HasOne(df => df.UpdateBy)
                .WithMany(f => f.UserUpdateTreatments)
                .HasForeignKey(df => df.UpdateById);
            builder.Entity<Treatment>()
             .Property(f => f.CreateDate)
             .HasDefaultValueSql("DBO.dReturnDate(getdate())");
            #endregion

            #region Feeding
            builder.Entity<Feeding>()
                .HasOne(df => df.CreateBy)
                .WithMany(f => f.UserCreatedFeedings)
                .HasForeignKey(df => df.CreateById);
            builder.Entity<Feeding>()
                .HasOne(df => df.UpdateBy)
                .WithMany(f => f.UserUpdateFeedings)
                .HasForeignKey(df => df.UpdateById);
            builder.Entity<Feeding>()
             .Property(f => f.CreateDate)
             .HasDefaultValueSql("DBO.dReturnDate(getdate())");
            builder.Entity<Feeding>()
            .Property(f => f.IsDelete)
            .HasDefaultValue(false);
            #endregion

            #region Vaccine
            builder.Entity<Vaccine>()
               .HasOne(df => df.CreateBy)
               .WithMany(f => f.UserCreatedVaccins)
               .HasForeignKey(df => df.CreateById);
            builder.Entity<Vaccine>()
                .HasOne(df => df.UpdateBy)
                .WithMany(f => f.UserUpdateVaccins)
                .HasForeignKey(df => df.UpdateById);
            builder.Entity<Vaccine>()
             .Property(f => f.CreateDate)
             .HasDefaultValueSql("DBO.dReturnDate(getdate())");
            builder.Entity<Vaccine>()
           .Property(f => f.IsDelete)
           .HasDefaultValue(false);
            #endregion

            #region FeedingFood
            builder.Entity<FeedingFood>()
                .HasKey(df => new { df.FoodId, df.FeedingId });
            builder.Entity<FeedingFood>()
                .HasOne(df => df.Feeding)
                .WithMany(d => d.FeedingFoods)
                .HasForeignKey(df => df.FeedingId);
            builder.Entity<FeedingFood>()
                .HasOne(df => df.Food)
                .WithMany(f => f.FeedingFoods)
                .HasForeignKey(df => df.FoodId);
            builder.Entity<FeedingFood>()
            .Property(f => f.CreateDate)
            .HasDefaultValueSql("DBO.dReturnDate(getdate())");
            #endregion

            #region VaccinFood
            builder.Entity<VaccineFood>()
                .HasOne(df => df.Vaccine)
                .WithMany(d => d.VaccineFoods)
                .HasForeignKey(df => df.VaccineId);
            builder.Entity<VaccineFood>()
                .HasOne(df => df.Food)
                .WithMany(f => f.VaccineFoods)
                .HasForeignKey(df => df.FoodId);
            builder.Entity<VaccineFood>()
            .Property(f => f.CreateDate)
            .HasDefaultValueSql("DBO.dReturnDate(getdate())");
            #endregion

            #region Init Data
            builder.Entity<Role>().HasData(
            new Role { RoleId = RoleDataConstant.ADMIN_ID, Name = RoleDataConstant.ADMIN },
            new Role { RoleId = RoleDataConstant.MANAGER_ID, Name = RoleDataConstant.MANAGER },
            new Role { RoleId = RoleDataConstant.STAFF_ID, Name = RoleDataConstant.STAFF }
            );

            builder.Entity<PremisesType>().HasData(
            new PremisesType { TypeId = PremisesTypeDataConstant.FARM_ID, Name = PremisesTypeDataConstant.FARM },
            new PremisesType { TypeId = PremisesTypeDataConstant.PROVIDER_ID, Name = PremisesTypeDataConstant.PROVIDER },
            new PremisesType { TypeId = PremisesTypeDataConstant.DISTRIBUTOR_ID, Name = PremisesTypeDataConstant.DISTRIBUTOR }
            );

            builder.Entity<Category>().HasData(
            new Category { CategoryId = CategoryDataConstant.PORK_ID, Name = CategoryDataConstant.PORK },
            new Category { CategoryId = CategoryDataConstant.CHICKEN_ID, Name = CategoryDataConstant.CHICKEN },
            new Category { CategoryId = CategoryDataConstant.BEEF_ID, Name = CategoryDataConstant.BEEF }
            );

            builder.Entity<TransactionStatus>().HasData(
            new TransactionStatus { StatusId = TransactionStatusDataConstant.CREATED_ID, Status = TransactionStatusDataConstant.CREATED },
            new TransactionStatus { StatusId = TransactionStatusDataConstant.CERTIFICATION_ID, Status = TransactionStatusDataConstant.CERTIFICATION },
            new TransactionStatus { StatusId = TransactionStatusDataConstant.CONFIRMED_ID, Status = TransactionStatusDataConstant.CONFIRMED },
            new TransactionStatus { StatusId = TransactionStatusDataConstant.REJECTED_ID, Status = TransactionStatusDataConstant.REJECTED }
            );

            builder.Entity<FoodDetailType>().HasData(
            new FoodDetailType { TypeId = FoodDetailTypeDataConstant.CREATE_NEW_ID, Name = FoodDetailTypeDataConstant.CREATE_NEW },
            new FoodDetailType { TypeId = FoodDetailTypeDataConstant.ADD_FEEDING_ID, Name = FoodDetailTypeDataConstant.ADD_FEEDING },
            new FoodDetailType { TypeId = FoodDetailTypeDataConstant.ADD_VACCINATION_ID, Name = FoodDetailTypeDataConstant.ADD_VACCINATION },
            new FoodDetailType { TypeId = FoodDetailTypeDataConstant.ADD_VERIFY_ID, Name = FoodDetailTypeDataConstant.ADD_VERIFY },
            new FoodDetailType { TypeId = FoodDetailTypeDataConstant.ADD_PROVIDER_ID, Name = FoodDetailTypeDataConstant.ADD_PROVIDER },
            new FoodDetailType { TypeId = FoodDetailTypeDataConstant.ADD_TREATMENT_ID, Name = FoodDetailTypeDataConstant.ADD_TREATMENT },
            new FoodDetailType { TypeId = FoodDetailTypeDataConstant.ADD_PACKAGING_ID, Name = FoodDetailTypeDataConstant.ADD_PACKAGING }
           );
            #endregion

        }
    }
}
