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
    [ApiController, Route("api/profile"), Authorize]
    public class ProfileController : ControllerBase {
        private readonly ProfileService Service;

        public ProfileController(ProfileService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ProfileResponse>> GetAsync() {
            long userId = (long)HttpContext.Items["UserId"];
            
            var result = await Service.GetAsync(userId);

            if (result is not null) {
                return Ok(new ProfileResponse(result));
            } else {
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<ActionResult<ProfileResponse>> PutAsync(ProfileUpdateRequest request) {
            long userId = (long)HttpContext.Items["UserId"];
            
            var (result, conflict) = await Service.SetAsync(
                userId, 
                request.Login, 
                request.Email, 
                request.Phone,
                request.Name,
                request.Surname
            );

            if (conflict is not null) {
                return Conflict(new ConflictResponse(conflict));
            }

            if (result is null) {
                return NotFound();
            } else {
                return Ok(new ProfileResponse(result));
            }
        }
    }
}