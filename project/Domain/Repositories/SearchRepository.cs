using System.Collections.Generic;
using System.Threading.Tasks;
using Api.FullTextSearch;
using Api.FullTextSearch.Models;

namespace Api.Domain.Repositories {
    public class SearchRepository {
        private static readonly string Index = "products";
        private readonly IElasticsearchContext Context;
        public SearchRepository(IElasticsearchContext context) {
            Context = context;
        }

        public Task IndexAsync(FTSProduct product) {
            return Context.IndexAsync(Index, product);
        }

        public Task DeleteAsync(long id) {
            return Context.DeleteAsync(Index, id);
        }

        public async Task<IEnumerable<FTSProduct>> SearchProductsAsync(
            string query,
            int skip,
            int take,
            List<long> categories,
            List<long> sections
        ) {
            var result = await Context.Client.SearchAsync<FTSProduct>(
                s => s.Query(
                    q => q.MatchAll()
                )
            );
            return result.Documents;
        }
    }
}