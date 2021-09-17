using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Repositories;
using Api.FullTextSearch.Models;

namespace Api.Domain.Services {
    public class SearchService {
        private readonly SearchRepository SearchRepository;

        public SearchService(SearchRepository searchRepository)
        {
            SearchRepository = searchRepository;
        }

        public Task<IEnumerable<FTSProduct>> SearchProductsAsync(
            string query, 
            int page, 
            List<long> categories, 
            List<long> sections,
            decimal? minPrice,
            decimal? maxPrice
        ) {
            int pageSize = 64;
            return SearchRepository.SearchProductsAsync(
                query,
                page * pageSize,
                pageSize,
                categories,
                sections,
                minPrice,
                maxPrice
            );
        }
    }
}