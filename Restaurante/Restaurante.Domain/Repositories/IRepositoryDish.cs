using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Repositories
{
    public interface IRepositoryDish : IRepositoryBase<Dish>
    {
        List<Dish> List();
        List<Dish> Search(string query);
        Domain.Entities.Dish GetByName(string name, int idRestaurant);
        bool DeleteDishForRestaurants(int idRestaurant);
    }
}
