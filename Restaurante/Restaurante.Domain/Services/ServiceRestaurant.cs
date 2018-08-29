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
    }
}
