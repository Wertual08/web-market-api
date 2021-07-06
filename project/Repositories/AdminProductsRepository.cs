using Api.Contexts;
using Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories {
    public class AdminProductsRepository {
        private readonly ApplicationDbContext DbContext;

        public AdminProductsRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Task<Product> FindAsync(long id) {
            return Task.Run(() => (
                from product in DbContext.Products
                where product.Id == id
                select product
            ).FirstOrDefault());
        }

        public Task<IEnumerable<Product>> ListAsync(int skip, int take) {
            return Task.Run<IEnumerable<Product>>(() => (
                from product in DbContext.Products 
                select product
            ).Skip(skip).Take(take));
        }

        public void Create(Product product) {
            DbContext.Products.Add(product);
        }

        public void Delete(Product product) {
            DbContext.Products.Remove(product);
        }

        public Task<int> SaveAsync() {
            return DbContext.SaveChangesAsync();
        }
    }
}