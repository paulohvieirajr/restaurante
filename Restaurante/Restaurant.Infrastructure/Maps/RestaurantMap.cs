using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Infrastructure.Maps
{
    public class RestaurantMap : IEntityTypeConfiguration<Domain.Entities.Restaurant>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Restaurant> builder)
        {
            // Key
            builder.HasKey(x => x.IdRestaurant);

            // Columns
            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(50).IsRequired();

            // Relatioships
            builder.HasMany(x => x.Dishes)
                .WithOne(x => x.Restaurant)
                .HasForeignKey(x => x.IdRestaurant);

            // Table name
            builder.ToTable("Restaurant");
        }
    }
}
