using Moq;
using Restaurant.Application.Interfaces;
using System;
using Xunit;

namespace Restaurant.Tests
{
    public class RestaurantTest
    {
        public RestaurantTest()
        {

        }

        [Fact]
        public void Create_A_Restaurant_With_Empty_Name()
        {
            var entity = new Domain.Entities.Restaurant(string.Empty);

            Assert.False(entity.Valid);
        }

        [Fact]
        public void Create_A_Restaurant_With_Short_Name()
        {
            var entity = new Domain.Entities.Restaurant("Xp");

            Assert.False(entity.Valid);
        }

        [Fact]
        public void Create_A_Restaurant_With_Big_Name()
        {
            var entity = new Domain.Entities.Restaurant("asdfghjklçasdfghjklçasdfghjklçasdfghjklçasdfghjklça");

            Assert.False(entity.Valid);
        }

        [Fact]
        public void Create_A_Restaurant_With_Valid_Name()
        {
            var entity = new Domain.Entities.Restaurant("Jose's Restaurant");

            Assert.True(entity.Valid);
        }
    }
}
