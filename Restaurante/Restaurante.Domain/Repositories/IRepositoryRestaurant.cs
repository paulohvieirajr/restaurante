using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Repositories
{
    public interface IRepositoryRestaurant : IRepositoryBase<Domain.Entities.Restaurant>
    {
        List<Entities.Restaurant> List();
        List<Entities.Restaurant> Search(string query);
        Domain.Entities.Restaurant GetByName(string name);
    }
}
