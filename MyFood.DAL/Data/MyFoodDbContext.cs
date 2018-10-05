using MyFood.DAL.Models;
using System.Configuration;
using System.Data.Entity;

namespace MyFood.DAL.Data
{
    class MyFoodDbContext : DbContext
    {
        public MyFoodDbContext() : base(GetConnectionString()) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Rate> Rates { get; set; }
        private static string GetConnectionString() => ConfigurationManager.ConnectionStrings["MyFoodDb"].ConnectionString;
            
    }
}
