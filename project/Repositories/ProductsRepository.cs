using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Contexts;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories {
    public class ProductsRepository : AbstractRepository<Product> {
        public ProductsRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<Product> FindAsync(long id) {
            return (
                from product in DbContext.Products
                where product.Id == id
                select new Product { 
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                }
            ).FirstOrDefaultAsync();
        }
        
        public Task<List<Product>> ListAsync(int skip, int take) {
            return (
                from product in DbContext.Products
                orderby product.Id
                select new Product { 
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                }
            ).Skip(skip).Take(take).ToListAsync();
        }
    }
}