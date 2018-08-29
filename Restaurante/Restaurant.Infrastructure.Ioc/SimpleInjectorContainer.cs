using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Interfaces;
using Restaurant.Application.Services;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using Restaurant.Domain.Services;
using Restaurant.Infrastructure.Repositories;
using SimpleInjector;
using System;

namespace Restaurant.Infrastructure.Ioc
{
    public class SimpleInjectorContainer
    {
        public static Container Register(DbContextOptions<RestaurantContext> options)
        {
            var container = new Container();

            container.Register<RestaurantContext>(() => {
                return new RestaurantContext(options);
            }, ScopedLifestyle.Singleton);

            // Application
            container.Register<IApplicationServiceDish, ApplicationServiceDish>();
            container.Register<IApplicationServiceRestaurant, ApplicationServiceRestaurant>();

            // Domain
            container.Register<IServiceDish, ServiceDish>();
            container.Register<IServiceRestaurant, ServiceRestaurant>();

            // Infrastructure
            container.Register<IRepositoryDish, RepositoryDish>();
            container.Register<IRepositoryRestaurant, RepositoryRestaurant>();

            container.Verify();

            return container;
        }
    }
}
