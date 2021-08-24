using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Database.Models;
using Api.Domain.Repositories;

namespace Api.Domain.Services {
    public class CategoriesService {
        private readonly CategoriesRepository CategoriesRepository;

        public CategoriesService(CategoriesRepository сategoriesRepository)
        {
            CategoriesRepository = сategoriesRepository;
        }
        
        public async Task<IEnumerable<Category>> GetAsync() {
            return await CategoriesRepository.ListAsync();
        }
    }
}