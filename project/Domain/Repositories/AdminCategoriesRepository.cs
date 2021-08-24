using Api.Database;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Repositories {
    public class AdminCategoriesRepository : AbstractRepository<Category> {
        public AdminCategoriesRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<Category> FindAsync(long id) {
            return (
                from category in DbContext.Categories
                where category.Id == id
                select category
            ).Include(x => x.Record).FirstOrDefaultAsync();
        }

        public Task<List<Category>> ListAsync() {
            return (
                from category in DbContext.Categories 
                orderby category.Id
                select category
            ).Include(x => x.Record).ToListAsync();
        }

        public void Create(Category category) {
            DbContext.Categories.Add(category);
        }

        public void Delete(Category category) {
            DbContext.Categories.Remove(category);
        }
    }
}