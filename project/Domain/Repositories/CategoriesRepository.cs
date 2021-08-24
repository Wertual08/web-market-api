using Api.Database;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Repositories {
    public class CategoriesRepository : AbstractRepository<Category> {
        public CategoriesRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<Category> FindAsync(long id) {
            return (
                from category in DbContext.Categories
                where category.Id == id
                select category
            ).FirstOrDefaultAsync();
        }

        public Task<List<Category>> ListAsync() {
            return (
                from category in DbContext.Categories 
                orderby category.Id
                select category
            ).ToListAsync();
        }
    }
}