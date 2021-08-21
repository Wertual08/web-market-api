using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using Api.Services.Result;

namespace Api.Services {
    public class OrdersService {
        private readonly OrdersRepository OrdersRepository;
        private readonly OrderProductsRepository OrderProductsRepository;

        public OrdersService(OrdersRepository ordersRepository, OrderProductsRepository orderProductsRepository)
        {
            OrdersRepository = ordersRepository;
            OrderProductsRepository = orderProductsRepository;
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
            var result = new Order {
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

            OrdersRepository.Create(result);
            await OrdersRepository.SaveAsync();

            result.OrderProducts = new List<OrderProduct>(products.Count);
            foreach (var product in products) {
                result.OrderProducts.Add(new OrderProduct {
                    ProductId = product.Key,
                    Amount = product.Value,
                });
            }

            OrderProductsRepository.Create(result.OrderProducts);

            await OrderProductsRepository.SaveAsync();

            return await OrdersRepository.FindAsync(result.Id);
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
    }
}