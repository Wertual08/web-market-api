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
    [ApiController, Route("admin/products"), Authorize(UserRoleId.Admin)]
    public class AdminProductsController : ControllerBase {
        private readonly AdminProductsService Service;

        public AdminProductsController(AdminProductsService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminProductResponse>>> GetAsync([FromQuery] ProductsListRequest request) {
            var result = await Service.GetAsync(request.Page, request.Categories, request.Sections);

            return Ok(from item in result select new AdminProductResponse(item));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminProductResponse>> GetAsync(long id) {
            var result = await Service.GetAsync(id);

            if (result is not null) {
                return Ok(new AdminProductResponse(result));
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<AdminProductResponse>> PostAsync(AdminProductCreateRequest request) {
            var result = await Service.PostAsync(
                request.OldPrice,
                request.Price, 
                request.Name, 
                request.Description,
                request.Records,
                request.Categories,
                request.Sections
            );

            return Ok(new AdminProductResponse(result));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AdminProductResponse>> PutAsync(long id, AdminProductUpdateRequest request) {
            var result = await Service.PutAsync(
                id, 
                request.OldPrice,
                request.Price, 
                request.Name, 
                request.Description,
                request.Records,
                request.Categories,
                request.Sections
            );

            if (result is null) {
                return NotFound();
            } else {
                return Ok(new AdminProductResponse(result));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AdminProductResponse>> DeleteAsync(long id) {
            var result = await Service.DeleteAsync(id);

            if (result is null) {
                return NotFound();
            } else {
                return Ok(new AdminProductResponse(result));
            }
        }
    }
}