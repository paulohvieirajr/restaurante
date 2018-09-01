using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Interfaces
{
    public interface IServiceRestaurant : IServiceBase<Domain.Entities.Restaurant>
    {
        List<Domain.Entities.Restaurant> List();
        List<Domain.Entities.Restaurant> Search(string query);
    }
}
