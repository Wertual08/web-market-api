using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Database;
using Api.Database.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Api.Domain.Repositories {
    public class RefreshTokensRepository : AbstractRepository<RefreshToken> {
        public RefreshTokensRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<RefreshToken> FindAsync(string name) {
            return (
                from token in DbContext.RefreshTokens
                where token.Name == name
                select token
            ).FirstOrDefaultAsync();
        }

        public Task<RefreshToken> FindAsync(long userId) {
            return (
                from token in DbContext.RefreshTokens
                where token.UserId == userId
                select token
            ).FirstOrDefaultAsync();
        }

        public Task<List<RefreshToken>> ListAsync(long userId) {
            return (
                from token in DbContext.RefreshTokens
                where token.UserId == userId
                select token
            ).ToListAsync();
        }

        public void Create(RefreshToken token) {
            DbContext.RefreshTokens.Add(token);
        }
    }
}