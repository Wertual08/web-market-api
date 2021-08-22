using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Responses;
using System.Threading.Tasks;
using Api.Requests;
using Api.Authorization;
using Api.Services;
using System;

namespace Api.Controllers {
    [ApiController]
    [Route("cart")]
    [Authorize]
    public class CartController : ServiceController {
        private readonly CartService Service;

        public CartController(CartService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartProductResponse>>> GetAsync([FromQuery] int page = 0) {
            long userId = (long)HttpContext.Items["UserId"];

            var result = await Service.GetAsync(userId, page);

            return MakeResponse(result, models => from item in models select new CartProductResponse(item));
        }

        [HttpPost]
        public async Task<ActionResult<CartProductResponse>> PostAsync(CartAddRequest request) {
            long userId = (long)HttpContext.Items["UserId"];
            
            var result = await Service.AddProductAsync(
                userId,
                request.ProductId,
                request.Amount
            );

            return MakeResponse(result, model => new CartProductResponse(model));
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult<CartProductResponse>> DeleteAsync(long productId) {
            long userId = (long)HttpContext.Items["UserId"];

            var result = await Service.RemoveProductAsync(userId, productId);

            return MakeResponse(result, model => new CartProductResponse(model));
        }
    }
}