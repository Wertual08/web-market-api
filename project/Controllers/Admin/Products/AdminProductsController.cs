using System;
using System.Linq;
using System.Collections.Generic;
using api.Contexts;
using app.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Admin.Products {
    [ApiController]
    [Route("admin/products")]
    public class AdminProductsController : ControllerBase {
        private readonly ApplicationDbContext Context;

        public AdminProductsController(ApplicationDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AdminProductResponse>> Get() {
            return Ok(from product in Context.Products select new {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
            });
        }

        [HttpGet("{id}")]
        public ActionResult<AdminProductResponse> Get(long id) {
            var result = (
                from product in Context.Products
                where product.Id == id
                select new { 
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt,
                }
            ).FirstOrDefault();

            if (result is not null) {
                return Ok(result);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<AdminProductResponse> Post(AdminProductCreateRequest request) {
            var result = new Product{
                Name = request.Name,
                Description = request.Description,
            };
            Context.Products.Add(result);
            Context.SaveChanges();

            return Ok(new AdminProductResponse {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                CreatedAt = result.CreatedAt,
                UpdatedAt = result.UpdatedAt,
            });
        }

        [HttpPut("{id}")]
        public ActionResult<AdminProductResponse> Put(int id, AdminProductUpdateRequest request) {
            var result = (
                from product in Context.Products
                where product.Id == id
                select product
            ).FirstOrDefault();

            if (result is null) {
                return NotFound();
            } 

            result.Name = request.Name;
            result.Description = request.Description;
            result.UpdatedAt = DateTime.Now;
            Context.SaveChanges();

            return Ok(new AdminProductResponse {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                CreatedAt = result.CreatedAt,
                UpdatedAt = result.UpdatedAt,
            });
        }

        [HttpDelete("{id}")]
        public ActionResult<AdminProductResponse> Delete(long id) {
            var result = (
                from product in Context.Products
                where product.Id == id
                select product
            ).FirstOrDefault();

            if (result is null) {
                return NotFound();
            } 

            Context.Products.Remove(result);
            Context.SaveChanges();

            return Ok(new AdminProductResponse {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                CreatedAt = result.CreatedAt,
                UpdatedAt = result.UpdatedAt,
            });
        }
    }
}