using Restaurant.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Application.Interfaces
{
    public interface IApplicationServiceDish
    {
        ServiceResponse<List<DishDto>> List();
        ServiceResponse<List<DishDto>> Search(string query);
        ServiceResponse<DishDto> Get(int id);
        ServiceResponse<bool> Insert(DishDto dto);
        ServiceResponse<bool> Update(DishDto dto);
        ServiceResponse<bool> Delete(int id);
    }
}
