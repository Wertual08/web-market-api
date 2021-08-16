using Api.Contexts;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories {
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

        public Task<User> FindAsync(long id, string login, string email, string phone) {
            return (
                from user in DbContext.Users
                where 
                    user.Id != id && (
                        user.Login == login || 
                        user.Email == email || 
                        user.Phone == phone
                    )
                select user
            ).FirstOrDefaultAsync();
        }
    }
}