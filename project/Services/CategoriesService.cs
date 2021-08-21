using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;

namespace Api.Services {
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