using System.Threading.Tasks;
using Nest;

namespace Api.FullTextSearch {
    public interface IElasticsearchContext {
        IElasticClient Client { get; }

        Task IndexCreateAsync<T>(string name) where T : class;
        Task IndexDeleteAsync(string name);
        Task<bool> IndexExistsAsync(string name);

        Task IndexAsync<T>(string index, T item) where T : class;
        Task DeleteAsync(string index, string id);
        Task DeleteAsync(string index, long id);
    }
}