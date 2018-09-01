using Moq;
using Restaurant.Application.Interfaces;
using System;
using Xunit;

namespace Restaurant.Tests
{
    public class DishTest
    {
        private readonly Domain.Entities.Restaurant restaurant;

        public DishTest()
        {
            restaurant = new Domain.Entities.Restaurant("Jose's Restaurant");
        }

        [Fact]
        public void Create_A_Dish_With_Empty_Name()
        {
            var entity = new Domain.Entities.Dish(string.Empty, restaurant, 0);

            Assert.False(entity.Valid);
        }

        [Fact]
        public void Create_A_Dish_With_Short_Name()
        {
            var entity = new Domain.Entities.Dish("XP", restaurant, 0);

            Assert.False(entity.Valid);
        }

        [Fact]
        public void Create_A_Dish_With_Big_Name()
        {
            var entity = new Domain.Entities.Dish("qwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiop2", restaurant, 0);

            Assert.False(entity.Valid);
        }

        [Fact]
        public void Create_A_Dish_With_Nullable_Restaurant()
        {
            var entity = new Domain.Entities.Dish("Soap", null, 20);

            Assert.False(entity.Valid);
        }

        [Fact]
        public void Create_A_Dish_With_Invalid_Price()
        {
            var entity = new Domain.Entities.Dish("Soap", restaurant, 0);

            Assert.False(entity.Valid);
        }

        [Fact]
        public void Create_A_Dish_With_Valid_Parameters()
        {
            var entity = new Domain.Entities.Dish("Soap", restaurant, 50);

            Assert.True(entity.Valid);
        }
    }
}
