using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Responses;
using System.Threading.Tasks;
using Api.Domain.Requests;
using Api.Authorization;
using Api.Domain.Services;
using System;

namespace Api.Controllers {
    [ApiController]
    [Route("orders")]
    [Authorize]
    public class OrdersController : ServiceController {
        private readonly OrdersService Service;

        public OrdersController(OrdersService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAsync([FromQuery] int page) {
            long userId = (long)HttpContext.Items["UserId"];

            var result = await Service.ListAsync(userId, page);

            return MakeResponse(result, models => from item in models select new OrderResponse(item));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetAsync(long id) {
            long userId = (long)HttpContext.Items["UserId"];

            var result = await Service.GetAsync(userId, id);

            return MakeResponse(result, model => new OrderResponse(model));
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponse>> PostAsync(OrderCreateRequest request) {
            long? userId = HttpContext.Items["UserId"] as long?;
            
            var result = await Service.CreateAsync(
                userId,
                request.Email,
                request.Phone,
                request.Name,
                request.Surname,
                request.Address,
                request.PromoCode,
                request.Description,
                request.Products
            );

            return MakeResponse(result, model => new OrderResponse(model));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderResponse>> DeleteAsync(long id) {
            long userId = (long)HttpContext.Items["UserId"];

            var result = await Service.CancelAsync(userId, id);

            return MakeResponse(result, model => new OrderResponse(model));
        }
    }
}