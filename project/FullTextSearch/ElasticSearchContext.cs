using System;
using System.Threading.Tasks;
using Nest;

namespace Api.FullTextSearch {
    public class ElasticsearchContext : IElasticsearchContext
    {
        public IElasticClient Client { get; private set; }

        public ElasticsearchContext(string connection) {
            Client = new ElasticClient(
                new Uri($"http://{connection}")
            );
        }

        public Task CreateIndexAsync<T>(string name) where T : class {
            return Client.Indices.CreateAsync(name, x => x.Map<T>(x => x.AutoMap()));
        }

        public Task DeleteIndexAsync(string name) {
            return Client.Indices.DeleteAsync(name);
        }

        public Task IndexAsync<T>(string index, T item) where T : class {
            return Client.IndexAsync(item, i => i.Index(index));
        }

        public Task DeleteAsync(string index, string id) {
            return Client.DeleteAsync(new DeleteRequest(index, id));
        }

        public Task DeleteAsync(string index, long id) {
            return Client.DeleteAsync(new DeleteRequest(index, id));
        }
    }
}