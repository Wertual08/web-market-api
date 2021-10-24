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
    [ApiController, Route("admin/orders"), Authorize(UserRoleId.Admin)]
    public class AdminOrdersController : ServiceController {
        private readonly AdminOrdersService Service;

        public AdminOrdersController(AdminOrdersService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAsync(int page) {
            var result = await Service.ListAsync(page);

            return MakeResponse(result, models => from model in models select new OrderResponse(model));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetAsync(long id) {
            var result = await Service.GetAsync(id);

            return MakeResponse(result, model => new OrderResponse(model));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderResponse>> PutAsync(long id, AdminOrderUpdateRequest request) {
            if (!Enum.TryParse<OrderStateId>(request.State, out var stateId)) {
                return UnprocessableEntity();
            }

            var result = await Service.UpdateAsync(id, stateId);

            return MakeResponse(result, model => new OrderResponse(model));
        }

        [HttpGet("{id}/products")]
        public async Task<ActionResult<IEnumerable<OrderProductResponse>>> GetProductsAsync(long id) {
            var result = await Service.GetProductsAsync(id);

            return MakeResponse(result, models => from model in models select new OrderProductResponse(model));
        }
    }
}