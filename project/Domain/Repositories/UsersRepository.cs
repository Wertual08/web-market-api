using Api.Database;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Repositories {
    public class UsersRepository : AbstractRepository<User> {
        public UsersRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<User> FindAsync(string email, string phone) {
            return (
                from user in DbContext.Users
                where 
                    user.Email == email ||
                    (user.Phone != null && user.Phone == phone)
                select user
            ).FirstOrDefaultAsync();
        }

        public Task<User> FindAsync(long id) {
            return (
                from user in DbContext.Users
                where user.Id == id
                select user
            ).FirstOrDefaultAsync();
        }

        public Task<User> FindByTokenAsync(string refreshToken) {
            return (
                from user in DbContext.Users
                join token in DbContext.RefreshTokens
                on user.Id equals token.UserId
                where token.Name == refreshToken
                select user
            ).FirstOrDefaultAsync();
        }

        public void Create(User model) {
            DbContext.Users.Add(model);
        }
    }
}