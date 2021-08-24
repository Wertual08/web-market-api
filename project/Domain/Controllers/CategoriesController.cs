using System;
using System.Linq;
using System.Collections.Generic;
using Api.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Repositories;
using Api.Domain.Responses;
using System.Threading.Tasks;
using Api.Domain.Requests;
using Api.Authorization;
using Api.Domain.Services;

namespace Api.Controllers {
    [ApiController]
    [Route("categories")]
    public class CategoriesController : ControllerBase {
        private readonly CategoriesService Service;

        public CategoriesController(CategoriesService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAsync() {
            var result = await Service.GetAsync();

            return Ok(from item in result select new CategoryResponse(item));
        }
    }
}