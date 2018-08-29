using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Dto;
using Restaurant.Application.Interfaces;

namespace Restaurant.Api.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/dish")]
    public class RestaurantController : Controller
    {
        private readonly IApplicationServiceRestaurant _service;

        /// <summary>
        /// Restaurant resource 
        /// </summary>
        public RestaurantController(IApplicationServiceRestaurant service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns a list of restaurants registered in the API
        /// </summary>
        /// <returns> List of Restaurants</returns>
        [HttpGet]
        [Route("")]
        public ServiceResponse<List<DishDto>> List()
        {
            return null;
            //return _service.ListarComDescontos();
        }

        /// <summary>
        /// Returns a list of restaurant registered in the API filtered by name
        /// </summary>
        /// <returns> List of Restaurants</returns>
        [HttpGet]
        [Route("")]
        public ServiceResponse<List<DishDto>> Search(string name)
        {
            return null;
            //return _service.ListarComDescontos();
        }

        /// <summary>
        /// Returns a specific restaurant registered in the API by API
        /// </summary>
        /// <returns> Restaurant</returns>
        [HttpGet]
        [Route("{id}")]
        public ServiceResponse<DishDto> Get(int id)
        {
            return null;
            //return _service.ListarComDescontos();
        }

        /// <summary>
        /// Insert a new restaurant in the API
        /// </summary>
        /// <returns> Boolean to confirm the operation</returns>
        [HttpPost]
        public ServiceResponse<bool> Insert([FromBody]DishDto dto)
        {
            return null;
        }

        /// <summary>
        /// Update a existed restaurant in the API
        /// </summary>
        /// <returns> Boolean to confirm the operation</returns>
        [HttpPut]
        public ServiceResponse<bool> Update([FromBody]DishDto dto)
        {
            return null;
        }

        /// <summary>
        /// Delete a existed restaurant in the API
        /// </summary>
        /// <returns> Boolean to confirm the operation</returns>
        [HttpDelete]
        public ServiceResponse<bool> Delete(int id)
        {
            return null;
        }
    }
}
