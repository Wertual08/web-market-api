using System.Threading.Tasks;
using Nest;

namespace Api.FullTextSearch {
    public interface IElasticsearchContext {
        IElasticClient Client { get; }

        Task CreateIndexAsync<T>(string name) where T : class;
        Task DeleteIndexAsync(string name);

        Task IndexAsync<T>(string index, T item) where T : class;
        Task DeleteAsync(string index, string id);
        Task DeleteAsync(string index, long id);
    }
}