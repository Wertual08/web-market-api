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

namespace Api.Controllers {
    [ApiController, Route("products")]
    public class ProductsController : ControllerBase {
        private readonly ProductsRepository ProductsRepository;

        public ProductsController(ProductsRepository productsRepository)
        {
            ProductsRepository = productsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAsync([FromQuery] ProductsListRequest request) {
            int pageSize = 32;
            var products = await ProductsRepository.ListAsync(
                request.Page * pageSize, 
                pageSize, 
                request.Categories, 
                request.Sections
            );

            return Ok(from item in products select new ProductResponse(item));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetAsync(long id) {
            var result = await ProductsRepository.FindAsync(id);

            if (result is not null) {
                return Ok(new ProductResponse(result));
            } else {
                return NotFound();
            }
        }

        [HttpGet("stats")]
        public async Task<ActionResult<ProductsStatsResponse>> GetStatsAsync() {
            return new ProductsStatsResponse {
                MinPrice = await ProductsRepository.GetMinPriceAsync(),
                MaxPrice = await ProductsRepository.GetMaxPriceAsync(),
            };
        }
    }
}