using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Infrastructure.Repositories
{
    public class RepositoryRestaurant : RepositoryBase<Domain.Entities.Restaurant>, IRepositoryRestaurant
    {
        private readonly RestaurantContext _context;

        public RepositoryRestaurant(RestaurantContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public override Domain.Entities.Restaurant Get(int id)
        {
            return (from r in _context.Restaurants
                    where r.Status && r.IdRestaurant == id
                    select r).FirstOrDefault();
        }

        public List<Domain.Entities.Restaurant> List()
        {
            return (from r in _context.Restaurants
                    where r.Status
                    select r).ToList();
        }

        public List<Domain.Entities.Restaurant> Search(string query)
        {
            return (from r in _context.Restaurants
                    where r.Name.Contains(query) && r.Status
                    select r).ToList();
        }

        public Domain.Entities.Restaurant GetByName(string name)
        {
            return (from r in _context.Restaurants
                    where r.Name == name && r.Status
                    select r).FirstOrDefault();
        }
    }
}
