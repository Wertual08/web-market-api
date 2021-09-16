using Api.Database;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Repositories {
    public class ProfileRepository : AbstractRepository<User> {
        public ProfileRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<User> FindAsync(long id) {
            return (
                from user in DbContext.Users
                where user.Id == id
                select user
            ).FirstOrDefaultAsync();
        }

        public Task<User> FindAsync(long id, string email, string phone) {
            return (
                from user in DbContext.Users
                where 
                    user.Id != id && (
                        user.Email == email || 
                        user.Phone == phone
                    )
                select user
            ).FirstOrDefaultAsync();
        }
    }
}