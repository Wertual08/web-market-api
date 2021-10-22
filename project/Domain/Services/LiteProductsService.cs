using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Domain.Repositories;
using Api.FullTextSearch.Models;

namespace Api.Domain.Services {
    public class LiteProductsService {
        private readonly ProductsRepository ProductsRepository;

        public LiteProductsService(ProductsRepository productsRepository)
        {
            ProductsRepository = productsRepository;
        }

        public async Task<Product> GetAsync(long id) {
            return await ProductsRepository.FindAsync(id);
        }

        public async Task<List<Product>> ListAsync(List<long> ids) {
            return await ProductsRepository.FindAsync(ids);
        }
    }
}