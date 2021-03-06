
using Api.Database;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Repositories {
    public class CartProductsRepository : AbstractRepository<CartProduct> {
        public CartProductsRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<List<CartProduct>> ListAsync(long userId) {
            return (
                from cartProduct in DbContext.CartProducts 
                where cartProduct.UserId == userId
                orderby cartProduct.ProductId
                select cartProduct
            ).ToListAsync();
        }

        public Task<CartProduct> FindAsync(long userId, long productId) {
            return (
                from cartProduct in DbContext.CartProducts 
                where cartProduct.UserId == userId && cartProduct.ProductId == productId
                select cartProduct
            ).FirstOrDefaultAsync();
        }

        public async Task ClearAsync(long userId) {
            var cartProducts = await (
                from cartProduct in DbContext.CartProducts 
                where cartProduct.UserId == userId
                select cartProduct
            ).ToListAsync();
            DbContext.CartProducts.RemoveRange();
        }

        public void Create(CartProduct cartProduct) {
            DbContext.CartProducts.Add(cartProduct);
        }

        public void Create(IEnumerable<CartProduct> cartProduct) {
            DbContext.CartProducts.AddRange(cartProduct);
        }

        public void Delete(CartProduct cartProduct) {
            DbContext.CartProducts.Remove(cartProduct);
        }
    }
}