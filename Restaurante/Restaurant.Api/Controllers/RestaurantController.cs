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
    [EnableCors("CorsPolicy")]
    [Route("api/restaurant")]
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
        public ServiceResponse<List<RestaurantDto>> List()
        {
            return _service.List();
        }

        /// <summary>
        /// Returns a list of restaurant registered in the API filtered by name
        /// </summary>
        /// <returns> List of Restaurants</returns>
        [HttpGet]
        [Route("search")]
        public ServiceResponse<List<RestaurantDto>> Search(string name)
        {
            return _service.Search(name);
        }

        /// <summary>
        /// Returns a specific restaurant registered in the API by API
        /// </summary>
        /// <returns> Restaurant</returns>
        [HttpGet]
        [Route("{id}")]
        public ServiceResponse<RestaurantDto> Get(int id)
        {
            return _service.Get(id);
        }

        /// <summary>
        /// Insert a new restaurant in the API
        /// </summary>
        /// <returns> Boolean to confirm the operation</returns>
        [HttpPost]
        public ServiceResponse<bool> Insert([FromBody]RestaurantDto dto)
        {
            return _service.Insert(dto);
        }

        /// <summary>
        /// Update a existed restaurant in the API
        /// </summary>
        /// <returns> Boolean to confirm the operation</returns>
        [HttpPut]
        public ServiceResponse<bool> Update([FromBody]RestaurantDto dto)
        {
            return _service.Update(dto);
        }

        /// <summary>
        /// Delete a existed restaurant in the API
        /// </summary>
        /// <returns> Boolean to confirm the operation</returns>
        [HttpDelete]
        public ServiceResponse<bool> Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}
