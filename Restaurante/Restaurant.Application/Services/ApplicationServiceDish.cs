using Flunt.Notifications;
using Restaurant.Application.Dto;
using Restaurant.Application.Interfaces;
using Restaurant.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Application.Services
{
    public class ApplicationServiceDish : IApplicationServiceDish
    {
        private readonly IServiceDish _service;
        private readonly IServiceRestaurant _serviceRestaurant;

        public ApplicationServiceDish(IServiceDish service, IServiceRestaurant serviceRestaurant)
        {
            _service = service;
            _serviceRestaurant = serviceRestaurant;
        }

        public ServiceResponse<List<DishDto>> List()
        {
            var result = new ServiceResponse<List<DishDto>>();

            try
            {
                var dishes = _service.List();
                if (dishes.Any())
                {
                    result.Object = new List<DishDto>();
                    dishes.ForEach(x => {
                        var restaurant = _serviceRestaurant.Get(x.IdRestaurant);

                        result.Object.Add(new DishDto()
                        {
                            Name = x.Name,
                            IdDish = x.IdDish,
                            Price = x.Price,
                            IdRestaurant = x.Restaurant.IdRestaurant,
                            Restaurant = new RestaurantDto()
                            {
                                IdRestaurant = restaurant.IdRestaurant,
                                Name = restaurant.Name
                            }
                        });
                    });
                    result.Result = true;
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add("Problems when try to list dishes: " + ex.Message);
            }

            return result;
        }

        public ServiceResponse<List<DishDto>> Search(string query)
        {
            var result = new ServiceResponse<List<DishDto>>();

            try
            {
                if(string.IsNullOrEmpty(query) || query.Length < 3)
                {
                    result.Messages.Add("Please, type 3 letters to search dishes");
                }
                else
                {
                    var dishes = _service.Search(query);
                    if (dishes.Any())
                    {
                        result.Object = new List<DishDto>();
                        dishes.ForEach(x => {
                            var restaurant = _serviceRestaurant.Get(x.IdRestaurant);

                            result.Object.Add(new DishDto()
                            {
                                Name = x.Name,
                                IdDish = x.IdDish,
                                Price = x.Price,
                                IdRestaurant = x.Restaurant.IdRestaurant,
                                Restaurant = new RestaurantDto()
                                {
                                    IdRestaurant = restaurant.IdRestaurant,
                                    Name = restaurant.Name
                                }
                            });
                        });

                        result.Result = true;
                    }
                    else
                    {
                        result.Messages.Add("Can't find dishes with the query informed.");
                    }
                }                
            }
            catch (Exception ex)
            {
                result.Messages.Add("Problems when try to search the list of dishes by name: " + ex.Message);
            }

            return result;
        }

        public ServiceResponse<DishDto> Get(int id)
        {
            var result = new ServiceResponse<DishDto>();

            try
            {
                var dish = _service.Get(id);
                if (dish != null)
                {
                    result.Object = new DishDto()
                    {
                        Name = dish.Name,
                        IdDish = dish.IdDish,
                        Price = dish.Price,
                        IdRestaurant = dish.Restaurant.IdRestaurant,
                        Restaurant = new RestaurantDto()
                        {
                            IdRestaurant = dish.Restaurant.IdRestaurant,
                            Name = dish.Restaurant.Name
                        }
                    };
                    result.Result = true;
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add("Problems when try to get the dish: " + ex.Message);
            }

            return result;
        }

        public ServiceResponse<bool> Insert(DishDto dto)
        {
            var result = new ServiceResponse<bool>();

            try
            {
                var restaurant = _serviceRestaurant.Get(dto.IdRestaurant);
                if(restaurant == null)
                {
                    result.Messages.Add("The restaurant informed don't exists in the system");
                }
                else
                {
                    var entity = new Domain.Entities.Dish(dto.Name, restaurant, dto.Price);
                    if (entity.Valid)
                    {
                        result.Result = result.Object = _service.Insert(entity);
                        if (!result.Result)
                        {
                            ((Notifiable)_service).Notifications
                                .ToList()
                                .ForEach(x => result.Messages.Add(x.Message));
                        }
                    }
                    else
                    {
                        entity.Notifications.ToList().ForEach(x => result.Messages.Add(x.Message));
                    }
                }                
            }
            catch (Exception ex)
            {
                result.Messages.Add("Problems when try to insert the dish: " + ex.Message);
            }

            return result;
        }

        public ServiceResponse<bool> Update(DishDto dto)
        {
            var result = new ServiceResponse<bool>();

            try
            {
                var entity = _service.Get(dto.IdDish);
                if (entity != null)
                {
                    var restaurant = _serviceRestaurant.Get(dto.IdRestaurant);
                    if(restaurant != null)
                    {
                        entity.Modify(dto.Name, restaurant, dto.Price);
                        if (entity.Valid)
                        {
                            result.Result = result.Object = _service.Update(entity);
                            if (!result.Result)
                            {
                                ((Notifiable)_service).Notifications
                                    .ToList()
                                    .ForEach(x => result.Messages.Add(x.Message));
                            }
                        }
                        else
                        {
                            entity.Notifications.ToList().ForEach(x => result.Messages.Add(x.Message));
                        }
                    }
                    else
                    {
                        result.Messages.Add("The restaurant informed in the request don't exists in th system");
                    }
                    
                }
                else
                {
                    result.Messages.Add("Can't find the dish to modify");
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add("Problems when try to update the dish: " + ex.Message);
            }

            return result;
        }

        public ServiceResponse<bool> Delete(int id)
        {
            var result = new ServiceResponse<bool>();

            try
            {
                result.Result = result.Object = _service.Delete(id);
                if (!result.Result)
                {
                    ((Notifiable)_service).Notifications
                        .ToList()
                        .ForEach(x => result.Messages.Add(x.Message));
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add("Problems when try to delete the restaurant: " + ex.Message);
            }

            return result;
        }
    }
}
