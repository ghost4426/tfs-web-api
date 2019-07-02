using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DTO.Entities;

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
        public DbSet<Categories> Categories { get; set; }
        public DbSet<PremisesType> PremisesType { get; set; }
        public DbSet<Premises> Premises { get; set; }
        public DbSet<DistributorFood> DistributorFood { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<TransactionStatus> TransactionStatus { get; set; }
        public DbSet<Treatment> Treatment { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<User>();
            //builder.Entity<Role>();

            //builder.Entity<Food>();

            //builder.Entity<Categories>().ToTable("Categories");

            //builder.Entity<Premises>();

            //builder.Entity<PremisesType>();

            builder.Entity<DistributorFood>()
                .HasKey(df => new { df.FoodId, df.PremisesId });
            //builder.Entity<DistributorFood>()
            //    .HasOne(df => df.Premises)
            //    //.WithMany(d => d.DistributorFood)
            //    .HasForeignKey(df => df.PremisesId);
            //builder.Entity<DistributorFood>()
            //    .HasOne(df => df.Food)
            //    //.WithMany(f => f.DistributorFood)
            //    .HasForeignKey(df => df.FoodId);

            //builder.Entity<Transaction>();

            //builder.Entity<TransactionStatus>();

            //builder.Entity<Treatment>();



        }

    }
}
