using Api.Contexts;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories {
    public class AdminProductsRepository : AbstractRepository<Product> {
        public AdminProductsRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<Product> FindAsync(long id) {
            return (
                from product in DbContext.Products
                where product.Id == id
                select product
            ).FirstOrDefaultAsync();
        }

        public Task<List<Product>> ListAsync(int skip, int take) {
            return (
                from product in DbContext.Products 
                orderby product.Id
                select product
            ).Skip(skip).Take(take).ToListAsync();
        }

        public void Create(Product product) {
            DbContext.Products.Add(product);
        }

        public void Delete(Product product) {
            DbContext.Products.Remove(product);
        }
    }
}