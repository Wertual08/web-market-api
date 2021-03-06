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
    [ApiController, Route("api/cart"), Authorize]
    public class CartController : ServiceController {
        private readonly CartService Service;

        public CartController(CartService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartProductResponse>>> GetAsync() {
            long userId = (long)HttpContext.Items["UserId"];

            var result = await Service.GetAsync(userId);

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

        [HttpPut]
        public async Task<ActionResult<IEnumerable<CartProductResponse>>> PutAsync(CartUpdateRequest request) {
            long userId = (long)HttpContext.Items["UserId"];
            
            var result = await Service.UpdateProductsAsync(userId, request.Products);

            return MakeResponse(result, models => from item in models select new CartProductResponse(item));
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult<CartProductResponse>> DeleteAsync(long productId) {
            long userId = (long)HttpContext.Items["UserId"];

            var result = await Service.RemoveProductAsync(userId, productId);

            return MakeResponse(result, model => new CartProductResponse(model));
        }
    }
}