using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.FullTextSearch;
using Api.FullTextSearch.Models;
using Elasticsearch.Net;
using Nest;

namespace Api.Domain.Repositories {
    public class SearchRepository {
        private const string Index = "products";
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
            List<long> sections,
            decimal? minPrice,
            decimal? maxPrice
        ) {
            var searchQuery = new QueryContainer(
                new MatchPhrasePrefixQuery {
                    Field = Infer.Field<FTSProduct>(d => d.Name),
                    Query = query,
                } ||
                new MatchQuery {
                    Field = Infer.Field<FTSProduct>(d => d.Name),
                    Fuzziness = Fuzziness.Auto,
                    Query = query
                }
            );
            
            if (sections is not null) {
                searchQuery &= new TermsQuery {
                    Field = Infer.Field<FTSProduct>(d => d.Sections),
                    Terms = sections.Cast<object>().ToArray(),
                };
            }
            
            if (categories is not null) {
                // Maybe + (filter) ???
                // Context.Client.RequestResponseSerializer.SerializeToString(searchQuery)
                searchQuery &= new TermsQuery {
                    Field = Infer.Field<FTSProduct>(d => d.Categories),
                    Terms = categories.Cast<object>().ToArray(),
                };
            }
            
            if (minPrice is not null || maxPrice is not null) {
                searchQuery &= new NumericRangeQuery {
                   Field = Infer.Field<FTSProduct>(d => d.Price),
                   GreaterThanOrEqualTo = (double?)minPrice,
                   LessThanOrEqualTo = (double?)maxPrice,
                };
            }

            var result = await Context.Client.SearchAsync<FTSProduct>(
                s => s.Query(q => searchQuery) // && q.Bool(b => b.Filter(f => f.Terms(t => t.Field(f => f.Categories).Terms(categories))))
                .Index(Index)
                .Skip(skip)
                .Take(take)
            );
            
            if (result.ServerError is not null) {
                throw new Exception(result.ServerError.ToString());
            }

            return result.Documents;
        }
    }
}