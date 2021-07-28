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
using Api.Services;

namespace Api.Controllers {
    [ApiController]
    [Route("admin/products")]
    [Authorize(AccessLevel.Admin)]
    public class AdminProductsController : ControllerBase {
        private readonly AdminProductsService AdminProductsService;

        public AdminProductsController(AdminProductsService adminProductsService)
        {
            AdminProductsService = adminProductsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminProductResponse>>> GetAsync([FromQuery] int page = 0) {
            var result = await AdminProductsService.GetAsync(page);

            return Ok(from item in result select new AdminProductResponse(item));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminProductResponse>> GetAsync(long id) {
            var result = await AdminProductsService.GetAsync(id);

            if (result is not null) {
                return Ok(new AdminProductResponse(result));
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<AdminProductResponse>> PostAsync(AdminProductCreateRequest request) {
            var result = await AdminProductsService.PostAsync(request.Price, request.Name, request.Description);

            return Ok(new AdminProductResponse(result));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AdminProductResponse>> PutAsync(long id, AdminProductUpdateRequest request) {
            var result = await AdminProductsService.PutAsync(id, request.Price, request.Name, request.Description);

            if (result is null) {
                return NotFound();
            } else {
                return Ok(new AdminProductResponse(result));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AdminProductResponse>> DeleteAsync(long id) {
            var result = await AdminProductsService.DeleteAsync(id);

            if (result is null) {
                return NotFound();
            } else {
                return Ok(new AdminProductResponse(result));
            }
        }
    }
}