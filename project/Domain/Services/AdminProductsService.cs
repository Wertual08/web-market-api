using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Domain.Repositories;
using Api.FullTextSearch.Models;

namespace Api.Domain.Services {
    public class AdminProductsService {
        private readonly AdminProductsRepository AdminProductsRepository;
        private readonly SearchRepository SearchRepository;

        public AdminProductsService(AdminProductsRepository adminProductsRepository, SearchRepository searchRepository)
        {
            AdminProductsRepository = adminProductsRepository;
            SearchRepository = searchRepository;
        }
        
        public async Task<IEnumerable<Product>> GetAsync(int page, List<long> categories, List<long> sections) {
            int pageSize = 32;
            return await AdminProductsRepository.ListAsync(page * pageSize, pageSize, categories, sections);
        }

        public async Task<Product> GetAsync(long id) {
            return await AdminProductsRepository.FindAsync(id);
        }

        public async Task<Product> PostAsync(
            decimal price, 
            string name, 
            string description, 
            List<long> records, 
            List<long> categories, 
            List<long> sections
        ) {
            var result = new Product{
                Price = price,
                Name = name,
                Description = description,
            };
            AdminProductsRepository.Create(result);
            await AdminProductsRepository.SaveAsync();
            
            AdminProductsRepository.SetRecords(result.Id, records);
            AdminProductsRepository.SetCategories(result.Id, categories);
            AdminProductsRepository.SetSections(result.Id, sections);
            await AdminProductsRepository.SaveAsync();

            await SearchRepository.IndexAsync(new FTSProduct {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Price = result.Price, 
                Image = "ASS_PLUG",
                Categories = categories,
                Sections = sections,
            });

            return result;
        }

        public async Task<Product> PutAsync(
            long id, 
            decimal price, 
            string name, 
            string description, 
            List<long> records, 
            List<long> categories, 
            List<long> sections
        ) {
            var result = await AdminProductsRepository.FindAsync(id);

            if (result is null) {
                return null;
            } 

            result.Price = price;
            result.Name = name;
            result.Description = description;
            result.UpdatedAt = DateTime.Now;

            // FIXME: Update relations
            AdminProductsRepository.SetRecords(result.Id, records);
            AdminProductsRepository.SetCategories(result.Id, categories);
            AdminProductsRepository.SetSections(result.Id, sections);
            await AdminProductsRepository.SaveAsync();
            
            await SearchRepository.IndexAsync(new FTSProduct {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Price = result.Price, 
                Image = "ASS_PLUG",
                Categories = categories,
                Sections = sections,
            });

            return result;
        }

        public async Task<Product> DeleteAsync(long id) {
            var result = await AdminProductsRepository.FindAsync(id);

            if (result is null) {
                return null;
            } 

            AdminProductsRepository.Delete(result);
            await AdminProductsRepository.SaveAsync();

            await SearchRepository.DeleteAsync(result.Id);

            return result;
        }
    }
}