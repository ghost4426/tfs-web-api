using DTO.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class FoodTrackerDbContext : DbContext
    {

        public FoodTrackerDbContext(DbContextOptions<FoodTrackerDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("");
        //    base.OnConfiguring(optionsBuilder);

        //    if (optionsBuilder.IsConfigured)
        //        return;

        //  string connectionString = Configuration.GetValue<string>("ConnectionStrings:StoreDbConnection");
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("User");
            builder.Entity<Role>().ToTable("Role");

        }
    }
}
