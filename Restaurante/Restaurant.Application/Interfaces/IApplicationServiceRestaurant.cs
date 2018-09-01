using Restaurant.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Application.Interfaces
{
    public interface IApplicationServiceRestaurant
    {
        ServiceResponse<List<RestaurantDto>> List();
        ServiceResponse<List<RestaurantDto>> Search(string query);
        ServiceResponse<RestaurantDto> Get(int id);
        ServiceResponse<bool> Insert(RestaurantDto dto);
        ServiceResponse<bool> Update(RestaurantDto dto);
        ServiceResponse<bool> Delete(int id);

    }
}
