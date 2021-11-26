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

        public async Task IndexCreateAsync<T>(string name) where T : class {
            var response = await Client.Indices.CreateAsync(name, x => x.Map<T>(x => x.AutoMap()));
            if (response.ServerError is not null) {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public async Task<bool> IndexExistsAsync(string name) {
            var response = await Client.Indices.ExistsAsync(name);
            if (response.ServerError is not null) {
                throw new Exception(response.ServerError.ToString());
            }
            return response.Exists;
        }

        public async Task IndexDeleteAsync(string name) {
            var response = await Client.Indices.DeleteAsync(name);
            if (response.ServerError is not null) {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public async Task IndexAsync<T>(string index, T item) where T : class {
            var response = await Client.IndexAsync(item, i => i.Index(index));
            if (response.ServerError is not null) {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public async Task DeleteAsync(string index, string id) {
            var response = await Client.DeleteAsync(new DeleteRequest(index, id));
            if (response.ServerError is not null) {
                throw new Exception(response.ServerError.ToString());
            }
        }

        public async Task DeleteAsync(string index, long id) {
            var response = await Client.DeleteAsync(new DeleteRequest(index, id));
            if (response.ServerError is not null) {
                throw new Exception(response.ServerError.ToString());
            }
        }
    }
}