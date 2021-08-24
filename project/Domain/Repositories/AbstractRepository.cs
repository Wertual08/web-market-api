using System.Threading.Tasks;
using Api.Database;

namespace Api.Domain.Repositories {
    public abstract class AbstractRepository<T> {
        protected readonly ApplicationDbContext DbContext;

        public AbstractRepository(ApplicationDbContext dbContext) {
            DbContext = dbContext;
        }

        public Task<int> SaveAsync() {
            return DbContext.SaveChangesAsync();
        }
    }
}