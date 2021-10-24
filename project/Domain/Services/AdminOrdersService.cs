using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Domain.Repositories;
using Api.Domain.Services.Result;

namespace Api.Domain.Services {
    public class AdminOrdersService {
        private readonly AdminOrdersRepository AdminOrdersRepository;
        private readonly OrderProductsRepository OrderProductsRepository;
        private readonly ProductsRepository ProductsRepository;

        public AdminOrdersService(
            AdminOrdersRepository adminOrdersRepository, 
            OrderProductsRepository orderProductsRepository,
            ProductsRepository productsRepository) {
            AdminOrdersRepository = adminOrdersRepository;
            OrderProductsRepository = orderProductsRepository;
            ProductsRepository = productsRepository;
        }
        
        public async Task<ServiceResult<IEnumerable<Order>>> ListAsync(int page) {
            int pageSize = 32;
            var orders = await AdminOrdersRepository.ListAsync(page * pageSize, pageSize);

            return orders;
        }

        public async Task<ServiceResult<Order>> GetAsync(long id) {
            var result = await AdminOrdersRepository.FindAsync(id);

            if (result is null) {
                return ServiceResultStatus.NotFound;
            }

            return result;
        }

        public async Task<ServiceResult<Order>> UpdateAsync(long id, OrderStateId stateId) {
            var result = await AdminOrdersRepository.FindAsync(id);

            if (result is null) {
                return ServiceResultStatus.NotFound;
            } 

            result.State = stateId;
            result.FinishedAt = stateId == OrderStateId.Completed ? DateTime.Now : null;

            await AdminOrdersRepository.SaveAsync();

            return result;
        }

        public async Task<ServiceResult<IEnumerable<OrderProduct>>> GetProductsAsync(long id) {
            var result = await OrderProductsRepository.ListAsync(id);

            return result;
        }
    }
}