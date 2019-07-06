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
        public DbSet<RegisterInfo> RegisterInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region User
            builder.Entity<User>()
             .Property(f => f.CreatedDate)
             .HasDefaultValueSql("getdate()");

            builder.Entity<User>()
             .Property(f => f.IsActive)
             .HasDefaultValue(true);
            #endregion

            #region Food
            builder.Entity<Food>()
                .HasOne(df => df.Farm)
                .WithMany(f => f.FarmFoods)
                .HasForeignKey(df => df.FarmId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Food>()
                .HasOne(df => df.Provider)
                .WithMany(f => f.ProviderFoods)
                .HasForeignKey(df => df.ProviderId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Food>()
               .Property(f => f.IsCertification)
               .HasDefaultValue(false);

            builder.Entity<Food>()
               .Property(f => f.IsFeeding)
               .HasDefaultValue(false);

            builder.Entity<Food>()
               .Property(f => f.IsPackaging)
               .HasDefaultValue(false);

            builder.Entity<Food>()
               .Property(f => f.IsTreatment)
               .HasDefaultValue(false);

            builder.Entity<Food>()
               .Property(f => f.IsVaccination)
               .HasDefaultValue(false);

            builder.Entity<Food>()
              .Property(f => f.CreatedDate)
              .HasDefaultValueSql("getdate()");
            #endregion

            #region Food Detail
            builder.Entity<FoodDetail>()
             .Property(f => f.CreatedDate)
             .HasDefaultValueSql("getdate()");
            #endregion

            #region Premises
            builder.Entity<Premises>()
             .Property(f => f.CreatedDate)
             .HasDefaultValueSql("getdate()");
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
            .Property(f => f.CreatedDate)
            .HasDefaultValueSql("getdate()");
            #endregion

            #region Transaction
            builder.Entity<Transaction>()
                .HasOne(df => df.Farm)
                .WithMany(f => f.FarmTransactions)
                .HasForeignKey(df => df.FarmId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Transaction>()
                .HasOne(df => df.Provider)
                .WithMany(f => f.ProviderTransactions)
                .HasForeignKey(df => df.ProviderId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Transaction>()
               .Property(f => f.CreatedDate)
               .HasDefaultValueSql("getdate()");

            #endregion RegisterInfo

            #region RegisterInfo
            builder.Entity<RegisterInfo>()
              .Property(f => f.CreatedDate)
              .HasDefaultValueSql("getdate()");
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
            new FoodDetailType { TypeId = FoodDetailTypeDataConstant.ADD_CERTIFICATION_ID, Name = FoodDetailTypeDataConstant.ADD_CERTIFICATION },
            new FoodDetailType { TypeId = FoodDetailTypeDataConstant.ADD_PROVIDER_ID, Name = FoodDetailTypeDataConstant.ADD_PROVIDER },
            new FoodDetailType { TypeId = FoodDetailTypeDataConstant.ADD_TREATMENT_ID, Name = FoodDetailTypeDataConstant.ADD_TREATMENT },
            new FoodDetailType { TypeId = FoodDetailTypeDataConstant.ADD_PACKAGING_ID, Name = FoodDetailTypeDataConstant.ADD_PACKAGING }
           );
            #endregion

        }
    }
}
