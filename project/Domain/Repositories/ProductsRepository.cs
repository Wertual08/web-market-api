using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Database;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Domain.Repositories {
    public class ProductsRepository : AbstractRepository<Product> {
        public ProductsRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<Product> FindAsync(long id) {
            return (
                from product in DbContext.Products
                where product.Id == id
                select product
            )
            .Include(x => x.Records)
            .Include(x => x.Categories)
            .Include(x => x.Sections)
            .AsSplitQuery()
            .FirstOrDefaultAsync();
        }

        public Task<List<Product>> FindAsync(IEnumerable<long> ids) {
            return (
                from product in DbContext.Products
                where ids.Contains(product.Id)
                select product
            )
            .ToListAsync();
        }
        
        public Task<List<Product>> ListAsync(int skip, int take, List<long> categories, List<long> sections) {
            var query = (
                from product in DbContext.Products
                orderby product.Id 
                select product 
            )
            .Include(x => x.Records)
            .Include(x => x.Categories)
            .Include(x => x.Sections)
            .Skip(skip).Take(take).AsSplitQuery();

            if (categories is not null) {
                query = query.Where(x => x.Categories.Any(x => categories.Contains(x.Id)));
            }
            if (sections is not null) {
                query = query.Where(x => x.Sections.Any(x => sections.Contains(x.Id)));
            }
            
            return query.ToListAsync();
        }

        public Task<decimal> GetMaxPriceAsync() {
            return DbContext.Products.MaxAsync(x => x.Price);
        }

        public Task<decimal> GetMinPriceAsync() {
            return DbContext.Products.MinAsync(x => x.Price);
        }
    }
}