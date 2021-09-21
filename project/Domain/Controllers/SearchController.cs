using System;
using System.Linq;
using System.Collections.Generic;
using Api.Database;
using Api.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Repositories;
using Api.Domain.Responses;
using System.Threading.Tasks;
using Api.Domain.Requests;
using Api.Domain.Services;

namespace Api.Controllers {
    [ApiController, Route("search")]
    public class SearchController : ControllerBase {
        private readonly SearchService Service;

        public SearchController(SearchService service)
        {
            Service = service;
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<LiteProductResponse>>> GetAsync([FromQuery] SearchProductsRequest request) {
            var products = await Service.SearchProductsAsync(
                request.Query,
                request.Page, 
                request.Categories, 
                request.Sections,
                request.MinPrice,
                request.MaxPrice
            );
            return Ok(from item in products select new LiteProductResponse(item));
        }
    }
}