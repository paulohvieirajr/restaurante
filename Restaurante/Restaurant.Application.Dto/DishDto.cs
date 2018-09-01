using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Application.Dto
{
    public class DishDto
    {
        public int IdDish { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int IdRestaurant { get; set; }

        public RestaurantDto Restaurant { get; set; }
    }
}
