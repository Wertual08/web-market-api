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
    [Route("admin/sections")]
    [Authorize(UserRoleId.Admin)]
    public class AdminSectionsController : ControllerBase {
        private readonly AdminSectionsService Service;

        public AdminSectionsController(AdminSectionsService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminSectionResponse>>> GetAsync() {
            var result = await Service.GetAsync();

            return Ok(from item in result select new AdminSectionResponse(item));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminSectionResponse>> GetAsync(long id) {
            var result = await Service.GetAsync(id);

            if (result is not null) {
                return Ok(new AdminSectionResponse(result));
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<AdminSectionResponse>> PostAsync(AdminSectionCreateRequest request) {
            var result = await Service.PostAsync(
                request.SectionId,
                request.Name
            );

            return Ok(new AdminSectionResponse(result));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AdminSectionResponse>> PutAsync(long id, AdminSectionUpdateRequest request) {
            var result = await Service.PutAsync(
                id, 
                request.SectionId,
                request.Name
            );

            if (result is null) {
                return NotFound();
            } else {
                return Ok(new AdminSectionResponse(result));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AdminSectionResponse>> DeleteAsync(long id) {
            var result = await Service.DeleteAsync(id);

            if (result is null) {
                return NotFound();
            } else {
                return Ok(new AdminSectionResponse(result));
            }
        }
    }
}