using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using System;
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
    }
}
