using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Infrastructure.Maps;
using System;

namespace Restaurant.Infrastructure
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options)
                : base(options)
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=1234;database=RestaurantDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();
            modelBuilder.ApplyConfiguration(new DishMap());
            modelBuilder.ApplyConfiguration(new RestaurantMap());
        }

        public DbSet<Dish> Dishes { get; set; }

        public DbSet<Domain.Entities.Restaurant> Restaurants { get; set; }
    }
}
