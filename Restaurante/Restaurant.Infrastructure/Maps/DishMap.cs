using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Infrastructure.Maps
{
    public class DishMap : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            // Key
            builder.HasKey(x => x.IdDish);

            // Columns
            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(50).IsRequired();
            builder.Property(x => x.IdRestaurant).HasColumnName("IdRestaurant").IsRequired();

            // Relatioships
            builder
                .HasOne(x => x.Restaurant)
                .WithMany(x => x.Dishes)
                .HasForeignKey(x => x.IdRestaurant);

            // Table name
            builder.ToTable("Dish");
        }
    }
}
