using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Restaurant.Infrastructure.Repositories
{
    public class RepositoryDish : RepositoryBase<Dish>, IRepositoryDish
    {
        private readonly RestaurantContext _context;

        public RepositoryDish(RestaurantContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public override Dish Get(int id)
        {
            return (from d in _context.Dishes
                    where d.Status && d.IdDish == id
                    select d).FirstOrDefault();
        }

        public List<Dish> List()
        {
            return (from d in _context.Dishes
                    where d.Status
                    select d).ToList();
        }

        public List<Dish> Search(string query)
        {
            return (from d in _context.Dishes
                    join r in _context.Restaurants on d.IdRestaurant equals r.IdRestaurant
                    where d.Status && d.Name.Contains(query)
                    select d).ToList();
        }

        public Domain.Entities.Dish GetByName(string name, int idRestaurant)
        {
            return (from d in _context.Dishes
                    where d.Name == name && d.Status && d.IdRestaurant == idRestaurant
                    select d).FirstOrDefault();
        }

        public bool DeleteDishForRestaurants(int idRestaurant)
        {
            var dishes = _context.Dishes.Where(x => x.IdRestaurant == idRestaurant).ToList();

            dishes.ForEach(x => x.Disable());

            return _context.SaveChanges() > 0;
        }
    }
}
