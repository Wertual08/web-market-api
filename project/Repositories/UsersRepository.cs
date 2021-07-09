using Api.Contexts;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories {
    public class UsersRepository : AbstractRepository<User> {
        public UsersRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<User> FindAsync(string login, string email, string phone) {
            return (
                from user in DbContext.Users
                where 
                    user.Login == login || 
                    user.Email == email ||
                    (user.Phone != null && user.Phone == phone)
                select user
            ).FirstOrDefaultAsync();
        }

        public Task<User> FindWithTokenAsync(string login, string email, string phone) {
            return (
                from user in DbContext.Users
                join token in DbContext.RefreshTokens
                on user.Id equals token.UserId
                where 
                    user.Login == login || 
                    user.Email == email ||
                    (user.Phone != null && user.Phone == phone)
                select new User {
                    Id = user.Id,
                    Role = user.Role,
                    Login = user.Login,
                    Password = user.Password,
                    RefreshToken = token,
                }
            ).FirstOrDefaultAsync();
        }

        public Task<User> FindByTokenAsync(string refreshToken) {
            return (
                from user in DbContext.Users
                join token in DbContext.RefreshTokens
                on user.Id equals token.UserId
                where token.Name == refreshToken
                select new User {
                    Id = user.Id,
                    Role = user.Role,
                    Login = user.Login,
                }
            ).FirstOrDefaultAsync();
        }

        public void Create(User model) {
            DbContext.Users.Add(model);
        }
    }
}