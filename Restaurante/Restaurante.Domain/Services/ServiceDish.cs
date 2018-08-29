using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Domain.Services
{
    public class ServiceDish : ServiceBase<Dish>, IServiceDish
    {
        private readonly IRepositoryDish _repository;

        public ServiceDish(IRepositoryDish repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
