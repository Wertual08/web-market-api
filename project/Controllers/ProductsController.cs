using System;
using System.Collections.Generic;
using api.Database;
using app.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase {
        private readonly ApplicationDbContext Context;

        public ProductsController(ApplicationDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IEnumerable<Product> Get() {
            return Context.Products;
        }

        [HttpGet("{id}")]
        public Product Get(long id) {
            return new Product {
                Id = 2, 
                Name = "spider", 
                CreatedAt = DateTime.Now, 
                UpdatedAt = DateTime.Now,
            };
        }

        [HttpPost]
        public void Post(Product product) {

        }

        [HttpPut("{id}")]
        public void Put(int id, Product product) {

        }

        [HttpDelete("{id}")]
        public void Delete(long id) {
        }
    }
}