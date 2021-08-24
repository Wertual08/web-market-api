using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Domain.Repositories;

namespace Api.Domain.Services {
    public class AdminCategoriesService {
        private readonly AdminCategoriesRepository AdminCategoriesRepository;

        public AdminCategoriesService(AdminCategoriesRepository adminCategoriesRepository)
        {
            AdminCategoriesRepository = adminCategoriesRepository;
        }
        
        public async Task<IEnumerable<Category>> GetAsync() {
            return await AdminCategoriesRepository.ListAsync();
        }

        public async Task<Category> GetAsync(long id) {
            return await AdminCategoriesRepository.FindAsync(id);
        }

        public async Task<Category> PostAsync(string name) {
            var result = new Category {
                Name = name,
            };
            AdminCategoriesRepository.Create(result);
            
            await AdminCategoriesRepository.SaveAsync();

            return result;
        }

        public async Task<Category> PutAsync(long id, string name) {
            var result = await AdminCategoriesRepository.FindAsync(id);

            if (result is null) {
                return null;
            } 

            result.Name = name;
            result.UpdatedAt = DateTime.Now;

            await AdminCategoriesRepository.SaveAsync();

            return result;
        }

        public async Task<Category> DeleteAsync(long id) {
            var result = await AdminCategoriesRepository.FindAsync(id);

            if (result is null) {
                return null;
            } 

            AdminCategoriesRepository.Delete(result);
            await AdminCategoriesRepository.SaveAsync();

            return result;
        }
    }
}