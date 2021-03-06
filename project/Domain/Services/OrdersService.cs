using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Domain.Repositories;
using Api.Domain.Services.Result;

namespace Api.Domain.Services {
    public class OrdersService {
        private readonly OrdersRepository OrdersRepository;
        private readonly OrderProductsRepository OrderProductsRepository;
        private readonly ProductsRepository ProductsRepository;

        public OrdersService(
            OrdersRepository ordersRepository, 
            OrderProductsRepository orderProductsRepository,
            ProductsRepository productsRepository) {
            OrdersRepository = ordersRepository;
            OrderProductsRepository = orderProductsRepository;
            ProductsRepository = productsRepository;
        }
        
        public async Task<ServiceResult<IEnumerable<Order>>> ListAsync(long userId, int page) {
            int pageSize = 32;
            var orders = await OrdersRepository.ListAsync(userId, page * pageSize, pageSize);

            return orders;
        }

        public async Task<ServiceResult<Order>> GetAsync(long? userId, long id) {
            var result = await OrdersRepository.FindAsync(id);

            if (result is null) {
                return ServiceResultStatus.NotFound;
            }

            if (result.UserId != userId) {
                return ServiceResultStatus.Forbid;
            }

            return result;
        }

        public async Task<ServiceResult<Order>> CreateAsync(
            long? userId, 
            string email,
            string phone,
            string name,
            string surname,
            string address, 
            string promoCode, 
            string description,
            Dictionary<long, int> products
        ) {
            var order = new Order {
                UserId = userId,
                Email = email,
                Phone = phone,
                Name = name,
                Surname = surname,
                State = OrderStateId.Requested,
                Address = address,
                PromoCode = promoCode,
                Description = description,
            };

            OrdersRepository.Create(order);
            await OrdersRepository.SaveAsync();

            var orderProducts = new List<OrderProduct>(products.Count);
            var productModels = await ProductsRepository.FindAsync(from product in products select product.Key);
            foreach (var product in products) {
                orderProducts.Add(new OrderProduct {
                    OrderId = order.Id,
                    ProductId = product.Key,
                    Amount = product.Value,
                    Price = productModels.Find(p => p.Id == product.Key).Price,
                });
            }

            OrderProductsRepository.Create(orderProducts);

            await OrderProductsRepository.SaveAsync();

            return await OrdersRepository.FindAsync(order.Id);
        }

        public async Task<ServiceResult<Order>> CancelAsync(long userId, long id) {
            var result = await OrdersRepository.FindAsync(id);

            if (result is null) {
                return ServiceResultStatus.NotFound;
            } 

            if (result.UserId != userId) {
                return ServiceResultStatus.Forbid;
            }

            result.State = OrderStateId.Canceled;

            await OrdersRepository.SaveAsync();

            return result;
        }

        public async Task<ServiceResult<IEnumerable<OrderProduct>>> GetProductsAsync(long? userId, long id) {
            var order = await OrdersRepository.FindAsync(id);

            if (order is null) {
                return ServiceResultStatus.NotFound;
            }

            if (order.UserId != userId) {
                return ServiceResultStatus.Forbid;
            }

            var result = await OrderProductsRepository.ListAsync(id);

            return result;
        }
    }
}