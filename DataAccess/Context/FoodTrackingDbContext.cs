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
        public DbSet<Product> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<MaterialCategories> MaterialCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("User");
            builder.Entity<Role>().ToTable("Role");
            builder.Entity<Product>().ToTable("Product");
            builder.Entity<Categories>().ToTable("Categories");
            builder.Entity<Material>().ToTable("Material");
            builder.Entity<MaterialCategories>().ToTable("MaterialCategories");
        }
        
    }
}
