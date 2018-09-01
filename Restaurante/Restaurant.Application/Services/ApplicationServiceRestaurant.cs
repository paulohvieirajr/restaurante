using Flunt.Notifications;
using Restaurant.Application.Dto;
using Restaurant.Application.Interfaces;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Application.Services
{
    public class ApplicationServiceRestaurant : IApplicationServiceRestaurant
    {
        private readonly IServiceRestaurant _service;

        public ApplicationServiceRestaurant(IServiceRestaurant service)
        {
            _service = service;
        }

        public ServiceResponse<List<RestaurantDto>> List()
        {
            var result = new ServiceResponse<List<RestaurantDto>>();

            try
            {
                var restaurants = _service.List();
                if(restaurants.Any())
                {
                    result.Object = new List<RestaurantDto>();
                    restaurants.ForEach(x => result.Object.Add(new RestaurantDto()
                    {
                        IdRestaurant = x.IdRestaurant, 
                        Name = x.Name
                    }));
                    result.Result = true;
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add("Problems when try to list restaurants: " + ex.Message);
            }

            return result;
        }

        public ServiceResponse<List<RestaurantDto>> Search(string query)
        {
            var result = new ServiceResponse<List<RestaurantDto>>();

            try
            {
                if (string.IsNullOrEmpty(query) || query.Length < 3)
                {
                    result.Messages.Add("Please, type 3 letters to search dishes");
                }
                else
                {
                    var restaurants = _service.Search(query);
                    if (restaurants.Any())
                    {
                        result.Object = new List<RestaurantDto>();
                        restaurants.ForEach(x => result.Object.Add(new RestaurantDto()
                        {
                            IdRestaurant = x.IdRestaurant,
                            Name = x.Name
                        }));
                        result.Result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add("Problems when try to search restaurants: " + ex.Message);
            }

            return result;
        }

        public ServiceResponse<RestaurantDto> Get(int id)
        {
            var result = new ServiceResponse<RestaurantDto>();

            try
            {
                var restaurant = _service.Get(id);
                if (restaurant != null)
                {
                    result.Object = new RestaurantDto()
                    {
                        IdRestaurant = restaurant.IdRestaurant,
                        Name = restaurant.Name
                    };

                    result.Result = true;
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add("Problems when try to get the restaurant: " + ex.Message);
            }

            return result;
        }

        public ServiceResponse<bool> Insert(RestaurantDto dto)
        {
            var result = new ServiceResponse<bool>();

            try
            {
                var entity = new Domain.Entities.Restaurant(dto.Name);
                if(entity.Valid)
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
            catch (Exception ex)
            {
                result.Messages.Add("Problems when try to insert the restaurant: " + ex.Message);
            }

            return result;
        }

        public ServiceResponse<bool> Update(RestaurantDto dto)
        {
            var result = new ServiceResponse<bool>();

            try
            {
                var entity = _service.Get(dto.IdRestaurant);
                if(entity != null)
                {
                    entity.ModifyName(dto.Name);
                    if(entity.Valid)
                    {
                        result.Result = result.Object = _service.Update(entity);
                        if(!result.Result)
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
                    result.Messages.Add("Can't find the resturant to modify");
                }
            }
            catch (Exception ex)
            {
                result.Messages.Add("Problems when try to update the restaurant: " + ex.Message);
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
