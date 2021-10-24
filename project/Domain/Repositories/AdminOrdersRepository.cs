using Api.Database;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Repositories {
    public class AdminOrdersRepository : AbstractRepository<Order> {
        public AdminOrdersRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<Order> FindAsync(long id) {
            return (
                from order in DbContext.Orders
                where order.Id == id
                select order
            ).FirstOrDefaultAsync();
        }

        public Task<List<Order>> ListAsync(int skip, int take) {
            return (
                from order in DbContext.Orders 
                orderby order.CreatedAt descending
                select order
            ).Skip(skip).Take(take).ToListAsync();
        }

        public void Delete(Order order) {
            DbContext.Orders.Remove(order);
        }
    }
}