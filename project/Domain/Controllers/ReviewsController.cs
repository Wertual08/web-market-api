using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Responses;
using System.Threading.Tasks;
using Api.Domain.Requests;
using Api.Authorization;
using Api.Domain.Services;
using System;
using Api.Database.Models;

namespace Api.Controllers {
    [ApiController, Route("api/reviews")]
    public class ReviewsController : ServiceController {
        private readonly ReviewsService Service;

        public ReviewsController(ReviewsService service) {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetAsync([FromQuery] int page) {
            var result = await Service.ListAsync(page);

            return MakeResponse(result, models => from item in models select new ReviewResponse(item));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewResponse>> GetAsync(long id) {

            var result = await Service.GetAsync(id);

            return MakeResponse(result, model => new ReviewResponse(model));
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<ReviewResponse>> PostAsync(ReviewCreateRequest request) {
            long? userId = HttpContext.Items["UserId"] as long?;
            
            var result = await Service.CreateAsync(
                userId,
                request.Grade,
                request.Name,
                request.Email,
                request.Address,
                request.Description
            );

            return MakeResponse(result, model => new ReviewResponse(model));
        }

        [HttpDelete("{id}"), Authorize(UserRoleId.Admin)]
        public async Task<ActionResult<ReviewResponse>> DeleteAsync(long id) {
            var result = await Service.DeleteAsync(id);

            return MakeResponse(result, model => new ReviewResponse(model));
        }
    }
}