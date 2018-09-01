using Moq;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Domain.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace Restaurant.Tests
{
    public class ServiceDishTest
    {
        private readonly Mock<IRepositoryDish> _repository;
        private readonly ServiceDish _service;
        private readonly Domain.Entities.Restaurant restaurant;

        public ServiceDishTest()
        {
            _repository = new Mock<IRepositoryDish>();
            _repository.Setup(x => x.List()).Returns(new List<Domain.Entities.Dish>());
            _repository.Setup(x => x.Search(string.Empty)).Returns(new List<Domain.Entities.Dish>());
            restaurant = new Domain.Entities.Restaurant("Jose's Restaurant");
            var dish = new Dish("Xpto", null, 0);
            _repository.Setup(x => x.GetByName(string.Empty, 0)).Returns(dish);

            _service = new ServiceDish(_repository.Object);
        }

        [Fact]
        public void List_Of_Restaurants_In_The_Domain_Services()
        {
            Assert.IsType<List<Domain.Entities.Dish>>(_service.List());
        }

        [Fact]
        public void List_Of_Restaurants_Searched_By_name_In_The_Domain_Services()
        {
            Assert.IsType<List<Domain.Entities.Dish>>(_service.Search(string.Empty));
        }

        [Fact]
        public void Insert_A_New_Dish_With_Duplicated_Name_For_A_Restaurant()
        {
            var dish = new Domain.Entities.Dish("Jose's Restaurant Dish", restaurant, 0);

            Assert.False(_service.Insert(dish));
        }

        [Fact]
        public void Insert_A_New_Dish_WithValid_Name()
        {
            var dish = new Domain.Entities.Dish("Jose's Restaurant Dish", restaurant, 0);

            _repository.Setup(x => x.GetByName(string.Empty, 0)).Returns(It.IsAny<Domain.Entities.Dish>());
            _repository.Setup(x => x.Insert(dish)).Returns(true);

            Assert.True(_service.Insert(dish));
        }

        [Fact]
        public void Update_A_Restaurant_With_Duplicated_Name()
        {
            var dish = new Domain.Entities.Dish("Jose's Restaurant Dish", restaurant, 0);
            _repository.Setup(x => x.GetByName(string.Empty, 0)).Returns(dish);

            Assert.False(_service.Update(dish));
        }

        [Fact]
        public void Update_A_Restaurant_WithValid_Name()
        {
            var dish = new Domain.Entities.Dish("Jose's Restaurant Dish", restaurant, 0);

            _repository.Setup(x => x.GetByName(string.Empty, 0)).Returns(It.IsAny<Domain.Entities.Dish>());
            _repository.Setup(x => x.Update(dish)).Returns(true);

            Assert.True(_service.Update(dish));
        }

        [Fact]
        public void Delete_A_Restaurant_With_Invalid_Id()
        {
            _repository.Setup(x => x.Get(0)).Returns(It.IsAny<Domain.Entities.Dish>());

            Assert.False(_service.Delete(0));
        }

        [Fact]
        public void Delete_A_Restaurant_With_Invalid_I()
        {
            var dish = new Domain.Entities.Dish("Jose's Restaurant Dish", restaurant, 0);

            _repository.Setup(x => x.Get(0)).Returns(dish);
            _repository.Setup(x => x.Update(dish)).Returns(true);

            Assert.True(_service.Delete(0));
        }
    }
}
