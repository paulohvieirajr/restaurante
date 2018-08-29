using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Infrastructure.Repositories
{
    public class RepositoryDish : RepositoryBase<Dish>, IRepositoryDish
    {
        private readonly RestaurantContext _context;

        public RepositoryDish(RestaurantContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
