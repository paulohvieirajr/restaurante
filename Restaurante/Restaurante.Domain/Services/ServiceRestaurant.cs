using Flunt.Notifications;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Services
{
    public class ServiceRestaurant : ServiceBase<Domain.Entities.Restaurant>, IServiceRestaurant
    {
        private readonly IRepositoryRestaurant _repository;

        public ServiceRestaurant(IRepositoryRestaurant repository) : base(repository)
        {
            _repository = repository;
        }

        public List<Entities.Restaurant> List()
        {
            return _repository.List();
        }

        public List<Entities.Restaurant> Search(string query)
        {
            return _repository.Search(query);
        }

        public override bool Insert(Entities.Restaurant entity)
        {
            var restaurant = _repository.GetByName(entity.Name);
            if(restaurant != null)
            {
                AddNotification("Restaurant.Name", "This restaurant name exists in the database");
                return false;
            }
            else
            {
                return base.Insert(entity);
            }
        }

        public override bool Update(Entities.Restaurant entity)
        {
            var restaurant = _repository.GetByName(entity.Name);
            if (restaurant != null && restaurant.IdRestaurant != entity.IdRestaurant)
            {
                AddNotification("Restaurant.Name", "This restaurant name exists in the database");
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
            if(entity != null)
            {
                entity.Disable();
                return base.Update(entity);
            }
            else
            {
                AddNotification("Restaurant.Status", "Can't find the restaurant to disable");
                return false;
            }
        }
    }
}
