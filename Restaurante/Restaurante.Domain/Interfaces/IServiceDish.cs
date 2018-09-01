using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Interfaces
{
    public interface IServiceDish : IServiceBase<Dish>
    {
        List<Domain.Entities.Dish> List();
        List<Domain.Entities.Dish> Search(string query);
    }
}
