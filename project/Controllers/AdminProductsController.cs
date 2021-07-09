using System;
using System.Linq;
using System.Collections.Generic;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Api.Repositories;
using Api.Responses;
using System.Threading.Tasks;
using Api.Requests;
using Api.Authorization;

namespace Api.Controllers {
    [ApiController]
    [Route("admin/products")]
    [Authorize(AccessLevel.Admin)]
    public class AdminProductsController : ControllerBase {
        private readonly AdminProductsRepository AdminProductsRepository;

        public AdminProductsController(AdminProductsRepository adminProductsRepository)
        {
            AdminProductsRepository = adminProductsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminProductResponse>>> GetAsync([FromQuery] int page = 0) {
            int pageSize = 32;
            var result = await AdminProductsRepository.ListAsync(page * pageSize, pageSize);

            return Ok(from item in result select new AdminProductResponse(item));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminProductResponse>> GetAsync(long id) {
            var result = await AdminProductsRepository.FindAsync(id);

            if (result is not null) {
                return Ok(new AdminProductResponse(result));
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<AdminProductResponse>> PostAsync(AdminProductCreateRequest request) {
            var result = new Product{
                Name = request.Name,
                Description = request.Description,
            };
            AdminProductsRepository.Create(result);
            await AdminProductsRepository.SaveAsync();

            return Ok(new AdminProductResponse(result));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AdminProductResponse>> PutAsync(int id, AdminProductUpdateRequest request) {
            var result = await AdminProductsRepository.FindAsync(id);

            if (result is null) {
                return NotFound();
            } 

            result.Name = request.Name;
            result.Description = request.Description;
            result.UpdatedAt = DateTime.Now;
            
            await AdminProductsRepository.SaveAsync();

            return Ok(new AdminProductResponse(result));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AdminProductResponse>> DeleteAsync(long id) {
            var result = await AdminProductsRepository.FindAsync(id);

            if (result is null) {
                return NotFound();
            } 

            AdminProductsRepository.Delete(result);
            await AdminProductsRepository.SaveAsync();

            return Ok(new AdminProductResponse(result));
        }
    }
}