﻿using System;
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
    [Route("api/dish")]
    public class DishController : Controller
    {
        private readonly IApplicationServiceDish _service;

        /// <summary>
        /// Dish resource 
        /// </summary>
        public DishController(IApplicationServiceDish service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns a list of dish registered ins the API
        /// </summary>
        /// <returns> List od Dishes</returns>
        [HttpGet]
        [Route("")]
        public ServiceResponse<List<DishDto>> List()
        {
            return _service.List();
        }

        /// <summary>
        /// Returns a list of dish registered ins the API filtered by name
        /// </summary>
        /// <returns> List od Dishes</returns>
        [HttpGet]
        [Route("search")]
        public ServiceResponse<List<DishDto>> Search(string name)
        {
            return _service.Search(name);
        }

        /// <summary>
        /// Returns a specific dish registered ins the API by API
        /// </summary>
        /// <returns> List od Dishes</returns>
        [HttpGet]
        [Route("{id}")]
        public ServiceResponse<DishDto> Get(int id)
        {
            return _service.Get(id);
        }

        /// <summary>
        /// Insert a new dish in the API
        /// </summary>
        /// <returns> Boolean to confirm the operation</returns>
        [HttpPost]
        public ServiceResponse<bool> Insert([FromBody]DishDto dto)
        {
            return _service.Insert(dto);
        }

        /// <summary>
        /// Update a existed dish in the API
        /// </summary>
        /// <returns> Boolean to confirm the operation</returns>
        [HttpPut]
        public ServiceResponse<bool> Update([FromBody]DishDto dto)
        {
            return _service.Update(dto);
        }

        /// <summary>
        /// Delete a existed dish in the API
        /// </summary>
        /// <returns> Boolean to confirm the operation</returns>
        [HttpDelete]
        public ServiceResponse<bool> Delete(int id)
        {
            return _service.Delete(id);
        }
    }
}
