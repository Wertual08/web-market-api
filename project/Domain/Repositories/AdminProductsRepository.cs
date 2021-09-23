using Api.Database;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Repositories {
    public class AdminProductsRepository : AbstractRepository<Product> {
        public AdminProductsRepository(ApplicationDbContext dbContext) : base(dbContext) {
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

        public Task<List<Product>> ListAsync(int skip, int take, List<long> categories = null, List<long> sections = null) {
            var query = (
                from product in DbContext.Products 
                orderby product.Id
                select product
            )
            .Include(x => x.Records)
            .Include(x => x.Categories)
            .Include(x => x.Sections)
            .AsSplitQuery()
            .Skip(skip).Take(take);

            if (categories is not null) {
                query = query.Where(x => x.Categories.Any(x => categories.Contains(x.Id)));
            }
            if (sections is not null) {
                query = query.Where(x => x.Sections.Any(x => sections.Contains(x.Id)));
            }
            
            return query.ToListAsync();
        }

        public void Create(Product product) {
            DbContext.Products.Add(product);
        }

        public void Delete(Product product) {
            DbContext.Products.Remove(product);
        }

        
        public void SetRecords(long productId, List<long> recordIds) {
            DbContext.ProductRecords.RemoveRange(
                from productRecord in DbContext.ProductRecords
                where productRecord.ProductId == productId
                select productRecord
            );
            if (recordIds is not null) {
                int i = 0;
                foreach (var recordId in recordIds) {
                    DbContext.ProductRecords.Add(new ProductRecord {
                        ProductId = productId,
                        RecordId = recordId,
                        Position = i++,
                    });
                }
            }
        }

        public void SetCategories(long productId, List<long> categoryIds) {
            DbContext.ProductCategories.RemoveRange(
                from productCategory in DbContext.ProductCategories
                where productCategory.ProductId == productId
                select productCategory
            );
            if (categoryIds is not null) {
                foreach (var categoryId in categoryIds) {
                    DbContext.ProductCategories.Add(new ProductCategory {
                        ProductId = productId,
                        CategoryId = categoryId,
                    });
                }
            }
        }

        public void SetSections(long productId, List<long> sectionIds) {
            DbContext.ProductSections.RemoveRange(
                from productSection in DbContext.ProductSections
                where productSection.ProductId == productId
                select productSection
            );
            if (sectionIds is not null) {
                foreach (var sectionId in sectionIds) {
                    DbContext.ProductSections.Add(new ProductSection {
                        ProductId = productId,
                        SectionId = sectionId,
                    });
                }
            }
        }
    }
}