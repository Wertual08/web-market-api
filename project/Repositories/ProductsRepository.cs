using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Contexts;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories {
    public class ProductsRepository {
        private ApplicationDbContext DbContext;

        public ProductsRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Task<Product> FindAsync(long id) {
            return Task.Run(() => (
                from product in DbContext.Products
                where product.Id == id
                select new Product { 
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                }
            ).FirstOrDefault());
        }
        
        public Task<IEnumerable<Product>> ListAsync(int skip, int take) {
            return Task.Run<IEnumerable<Product>>(() => (
                from product in DbContext.Products
                orderby product.Id
                select new Product { 
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                }
            ).Skip(skip).Take(take));
        }
    }
}