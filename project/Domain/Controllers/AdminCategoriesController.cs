using System.Linq;
using System.Collections.Generic;
using Api.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Responses;
using System.Threading.Tasks;
using Api.Domain.Requests;
using Api.Authorization;
using Api.Domain.Services;

namespace Api.Controllers {
    [ApiController, Route("api/admin/categories"), Authorize(UserRoleId.Admin)]
    public class AdminCategoriesController : ControllerBase {
        private readonly AdminCategoriesService Service;

        public AdminCategoriesController(AdminCategoriesService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminCategoryResponse>>> GetAsync() {
            var result = await Service.GetAsync();

            return Ok(from item in result select new AdminCategoryResponse(item));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminCategoryResponse>> GetAsync(long id) {
            var result = await Service.GetAsync(id);

            if (result is not null) {
                return Ok(new AdminCategoryResponse(result));
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<AdminCategoryResponse>> PostAsync(AdminCategoryCreateRequest request) {
            var result = await Service.PostAsync(
                request.RecordId,
                request.Name
            );

            return Ok(new AdminCategoryResponse(result));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AdminCategoryResponse>> PutAsync(long id, AdminCategoryUpdateRequest request) {
            var result = await Service.PutAsync(
                id, 
                request.RecordId,
                request.Name
            );

            if (result is null) {
                return NotFound();
            } else {
                return Ok(new AdminCategoryResponse(result));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AdminCategoryResponse>> DeleteAsync(long id) {
            var result = await Service.DeleteAsync(id);

            if (result is null) {
                return NotFound();
            } else {
                return Ok(new AdminCategoryResponse(result));
            }
        }
    }
}