using System;
using System.Linq;
using System.Collections.Generic;
using api.Database;
using app.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Products {
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase {
        private readonly ApplicationDbContext Context;

        public ProductsController(ApplicationDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductResponse>> Get() {
            return Ok(from product in Context.Products select new {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
            });
        }

        [HttpGet("{id}")]
        public ActionResult<ProductResponse> Get(long id) {
            var result = (
                from product in Context.Products
                where product.Id == id
                select new { 
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                }
            ).FirstOrDefault();

            if (result is not null) {
                return Ok(result);
            } else {
                return NotFound();
            }
        }
    }
}