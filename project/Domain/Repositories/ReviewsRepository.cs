using Api.Database;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Repositories {
    public class ReviewsRepository : AbstractRepository<Order> {
        public ReviewsRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<Review> FindAsync(long id) {
            return (
                from review in DbContext.Reviews
                where review.Id == id
                select review
            ).FirstOrDefaultAsync();
        }

        public Task<List<Review>> ListAsync(int skip, int take) {
            return (
                from review in DbContext.Reviews 
                orderby review.CreatedAt descending
                select review
            ).Skip(skip).Take(take).ToListAsync();
        }

        public void Create(Review review) {
            DbContext.Reviews.Add(review);
        }

        public void Delete(Review review) {
            DbContext.Reviews.Remove(review);
        }
    }
}