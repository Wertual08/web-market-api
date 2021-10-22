using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Domain.Repositories;
using Api.Domain.Services.Result;

namespace Api.Domain.Services {
    public class CartService {
        private readonly CartProductsRepository CartProductsRepository;

        public CartService(CartProductsRepository cartProductsRepository)
        {
            CartProductsRepository = cartProductsRepository;
        }

        public async Task<ServiceResult<IEnumerable<CartProduct>>> GetAsync(long userId, int page) {
            int pageSize = 32;

            var result = await CartProductsRepository.ListAsync(userId, page * pageSize, pageSize);

            return result;
        }

        public async Task<ServiceResult<CartProduct>> AddProductAsync(
            long userId, 
            long productId,
            int amount
        ) {
            var cartProduct = new CartProduct {
                UserId = userId,
                ProductId = productId,
                Amount = amount,
            };

            CartProductsRepository.Create(cartProduct);

            await CartProductsRepository.SaveAsync();

            return await CartProductsRepository.FindAsync(cartProduct.UserId, cartProduct.ProductId);
        }

        public async Task<ServiceResult<IEnumerable<CartProduct>>> UpdateProductsAsync(long userId, Dictionary<long, int> products) {
            await CartProductsRepository.ClearAsync(userId);

            var models = new List<CartProduct>(products.Count);
            foreach (var product in products) {
                models.Add(new CartProduct {
                    UserId = product.Key,
                    ProductId = product.Value,
                });
            }
            CartProductsRepository.Create(models);

            await CartProductsRepository.SaveAsync();

            return await CartProductsRepository.ListAsync(userId);
        }

        public async Task<ServiceResult<CartProduct>> RemoveProductAsync(long userId, long productId) {
            var result = await CartProductsRepository.FindAsync(userId, productId);

            if (result is null) {
                return ServiceResultStatus.NotFound;
            }

            CartProductsRepository.Delete(result);

            await CartProductsRepository.SaveAsync();

            return result;
        }
    }
}