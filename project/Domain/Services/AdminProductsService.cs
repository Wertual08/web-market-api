using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Domain.Repositories;
using Api.Domain.Services.Result;
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

        public async Task<ServiceResult<Product>> PostAsync(
            decimal? oldPrice,
            decimal price, 
            string code,
            string name, 
            string description, 
            string privateInfo,
            List<long> records, 
            List<long> categories, 
            List<long> sections
        ) {
            if (await AdminProductsRepository.CodeExistsAsync(code)) {
                return new ServiceResult<Product>(ServiceResultStatus.Conflict, "Code");
            }
            
            var result = new Product{
                OldPrice = oldPrice,
                Price = price,
                Code = code,
                Name = name,
                Description = description,
                PrivateInfo = privateInfo,
            };
            AdminProductsRepository.Create(result);
            await AdminProductsRepository.SaveAsync();
            
            AdminProductsRepository.SetRecords(result.Id, records);
            AdminProductsRepository.SetCategories(result.Id, categories);
            AdminProductsRepository.SetSections(result.Id, sections);
            await AdminProductsRepository.SaveAsync();

            result = await AdminProductsRepository.FindAsync(result.Id);
            await SearchRepository.IndexAsync(new FTSProduct {
                Id = result.Id,
                Code = result.Code,
                Name = result.Name,
                Description = result.Description,
                OldPrice = result.OldPrice,
                Price = result.Price, 
                Image = result.Records.FirstOrDefault()?.Identifier.ToString("N"),
                Categories = categories,
                Sections = sections,
            });

            return result;
        }

        public async Task<ServiceResult<Product>> PutAsync(
            long id, 
            decimal? oldPrice,
            decimal price, 
            string code,
            string name, 
            string description, 
            string privateInfo,
            List<long> records, 
            List<long> categories, 
            List<long> sections
        ) {
            var result = await AdminProductsRepository.FindAsync(id);

            if (result is null) {
                return ServiceResultStatus.NotFound;
            } 

            if (result.Code != code && await AdminProductsRepository.CodeExistsAsync(code)) {
                return new ServiceResult<Product>(ServiceResultStatus.Conflict, "Code");
            }

            result.OldPrice = oldPrice;
            result.Price = price;
            result.Name = name;
            result.Code = code;
            result.Description = description;
            result.PrivateInfo = privateInfo;
            result.UpdatedAt = DateTime.Now;

            // FIXME: Update relations
            AdminProductsRepository.SetRecords(result.Id, records);
            AdminProductsRepository.SetCategories(result.Id, categories);
            AdminProductsRepository.SetSections(result.Id, sections);
            await AdminProductsRepository.SaveAsync();
            
            result = await AdminProductsRepository.FindAsync(id);
            await SearchRepository.IndexAsync(new FTSProduct {
                Id = result.Id,
                Code = result.Code,
                Name = result.Name,
                Description = result.Description,
                OldPrice = result.OldPrice,
                Price = result.Price, 
                Image = result.Records.FirstOrDefault()?.Identifier.ToString("N"),
                Categories = categories,
                Sections = sections,
            });

            return result;
        }

        public async Task<ServiceResult<Product>> DeleteAsync(long id) {
            var result = await AdminProductsRepository.FindAsync(id);

            if (result is null) {
                return ServiceResultStatus.NotFound;
            } 

            AdminProductsRepository.Delete(result);
            await AdminProductsRepository.SaveAsync();

            await SearchRepository.DeleteAsync(result.Id);

            return result;
        }

        public async Task<ServiceResult<Product>> PostIndexAsync(long id) {
            var result = await AdminProductsRepository.FindAsync(id);

            if (result is null) {
                return ServiceResultStatus.NotFound;
            } 

            var categories = new List<long>();
            foreach (var category in result.Categories) {
                categories.Add(category.Id);
            }
            var sections = new List<long>();
            foreach (var section in result.Sections) {
                sections.Add(section.Id);
            }
            
            result = await AdminProductsRepository.FindAsync(id);
            await SearchRepository.IndexAsync(new FTSProduct {
                Id = result.Id,
                Code = result.Code,
                Name = result.Name,
                Description = result.Description,
                OldPrice = result.OldPrice,
                Price = result.Price, 
                Image = result.Records.FirstOrDefault()?.Identifier.ToString("N"),
                Categories = categories,
                Sections = sections,
            });

            return result;
        }
    }
}