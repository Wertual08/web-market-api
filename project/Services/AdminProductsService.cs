using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;

namespace Api.Services {
    public class AdminProductsService {
        private readonly AdminProductsRepository AdminProductsRepository;

        public AdminProductsService(AdminProductsRepository adminProductsRepository)
        {
            AdminProductsRepository = adminProductsRepository;
        }
        
        public async Task<IEnumerable<Product>> GetAsync(int page) {
            int pageSize = 32;
            return await AdminProductsRepository.ListAsync(page * pageSize, pageSize);
        }

        public async Task<Product> GetAsync(long id) {
            return await AdminProductsRepository.FindAsync(id);
        }

        public async Task<Product> PostAsync(decimal price, string name, string description) {
            var result = new Product{
                Price = price,
                Name = name,
                Description = description,
            };
            AdminProductsRepository.Create(result);
            await AdminProductsRepository.SaveAsync();

            return result;
        }

        public async Task<Product> PutAsync(long id, decimal price, string name, string description) {
            var result = await AdminProductsRepository.FindAsync(id);

            if (result is null) {
                return null;
            } 

            result.Price = price;
            result.Name = name;
            result.Description = description;
            result.UpdatedAt = DateTime.Now;
            
            await AdminProductsRepository.SaveAsync();

            return result;
        }

        public async Task<Product> DeleteAsync(long id) {
            var result = await AdminProductsRepository.FindAsync(id);

            if (result is null) {
                return null;
            } 

            AdminProductsRepository.Delete(result);
            await AdminProductsRepository.SaveAsync();

            return result;
        }
    }
}