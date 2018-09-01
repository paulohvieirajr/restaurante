using Moq;
using Restaurant.Domain.Repositories;
using Restaurant.Domain.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace Restaurant.Tests
{
    public class ServiceRestaurantTest
    {
        private readonly Mock<IRepositoryRestaurant> _repository;
        private readonly ServiceRestaurant _service;

        public ServiceRestaurantTest()
        {
            _repository = new Mock<IRepositoryRestaurant>();
            _repository.Setup(x => x.List()).Returns(new List<Domain.Entities.Restaurant>());
            _repository.Setup(x => x.Search(string.Empty)).Returns(new List<Domain.Entities.Restaurant>());
            _repository.Setup(x => x.GetByName(string.Empty)).Returns(new Domain.Entities.Restaurant(string.Empty));

            _service = new ServiceRestaurant(_repository.Object);
        }

        [Fact]
        public void List_Of_Restaurants_In_The_Domain_Services()
        {
            Assert.IsType<List<Domain.Entities.Restaurant>>(_service.List());
        }

        [Fact]
        public void List_Of_Restaurants_Searched_By_name_In_The_Domain_Services()
        {
            Assert.IsType<List<Domain.Entities.Restaurant>>(_service.Search(string.Empty));
        }

        [Fact]
        public void Insert_A_New_Restaurant_With_Duplicated_Name()
        {
            var restaurant = new Domain.Entities.Restaurant("Jose's Restaurant");

            Assert.False(_service.Insert(restaurant));
        }

        [Fact]
        public void Insert_A_New_Restaurant_WithValid_Name()
        {
            var restaurant = new Domain.Entities.Restaurant("Jose's Restaurant");

            _repository.Setup(x => x.GetByName(string.Empty)).Returns(It.IsAny<Domain.Entities.Restaurant>());
            _repository.Setup(x => x.Insert(restaurant)).Returns(true);

            Assert.True(_service.Insert(restaurant));
        }

        [Fact]
        public void Update_A_Restaurant_With_Duplicated_Name()
        {
            _repository.Setup(x => x.GetByName(string.Empty)).Returns(new Domain.Entities.Restaurant(string.Empty));
            var restaurant = new Domain.Entities.Restaurant("Jose's Restaurant");

            Assert.False(_service.Update(restaurant));
        }

        [Fact]
        public void Update_A_Restaurant_WithValid_Name()
        {
            var restaurant = new Domain.Entities.Restaurant("Jose's Restaurant");

            _repository.Setup(x => x.GetByName(string.Empty)).Returns(It.IsAny<Domain.Entities.Restaurant>());
            _repository.Setup(x => x.Update(restaurant)).Returns(true);

            Assert.True(_service.Update(restaurant));
        }

        [Fact]
        public void Delete_A_Restaurant_With_Invalid_Id()
        {
            var restaurant = new Domain.Entities.Restaurant("Jose's Restaurant");
            _repository.Setup(x => x.Get(0)).Returns(It.IsAny<Domain.Entities.Restaurant>());

            Assert.False(_service.Delete(0));
        }

        [Fact]
        public void Delete_A_Restaurant_With_Invalid_I()
        {
            var restaurant = new Domain.Entities.Restaurant("Jose's Restaurant");

            _repository.Setup(x => x.Get(0)).Returns(restaurant);
            _repository.Setup(x => x.Update(restaurant)).Returns(true);

            Assert.True(_service.Delete(0));
        }
    }
}
