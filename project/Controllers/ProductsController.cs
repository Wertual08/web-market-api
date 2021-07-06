using System;
using System.Linq;
using System.Collections.Generic;
using Api.Contexts;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Api.Repositories;
using Api.Responses;
using System.Threading.Tasks;

namespace Api.Controllers {
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase {
        private readonly ProductsRepository ProductsRepository;

        public ProductsController(ProductsRepository productsRepository)
        {
            ProductsRepository = productsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAsync([FromQuery] int page = 0) {
            int pageSize = 32;
            var products = await ProductsRepository.ListAsync(page * pageSize, pageSize);

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
    }
}