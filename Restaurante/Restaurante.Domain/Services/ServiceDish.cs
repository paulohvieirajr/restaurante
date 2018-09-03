using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Services
{
    public class ServiceDish : ServiceBase<Dish>, IServiceDish
    {
        private readonly IRepositoryDish _repository;

        public ServiceDish(IRepositoryDish repository) : base(repository)
        {
            _repository = repository;
        }

        public List<Dish> List()
        {
            return _repository.List();
        }

        public List<Dish> Search(string query)
        {
            return _repository.Search(query);
        }

        public override bool Insert(Dish entity)
        {
            var dish = _repository.GetByName(entity.Name, entity.IdRestaurant);
            if (dish != null)
            {
                AddNotification("Dish.Name", "This dish's name exists in the system");
                return false;
            }
            else
            {
                return base.Insert(entity);
            }
        }

        public override bool Update(Dish entity)
        {
            var dish = _repository.GetByName(entity.Name, entity.IdRestaurant);
            if (dish != null && dish.IdRestaurant != entity.IdDish)
            {
                AddNotification("Dish.Name", "This dish's name exists in the system");
                return false;
            }
            else
            {
                return base.Update(entity);
            }
        }

        public override bool Delete(int id)
        {
            var entity = base.Get(id);
            if (entity != null)
            {
                entity.Disable();
                return base.Update(entity);
            }
            else
            {
                AddNotification("Dish.Status", "Can't find the dish to disable");
                return false;
            }
        }

        public bool DeleteDishForRestaurants(int idRestaurant)
        {
            return _repository.DeleteDishForRestaurants(idRestaurant);
        }
    }
}
