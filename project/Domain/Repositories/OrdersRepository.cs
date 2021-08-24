using Api.Database;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Repositories {
    public class OrdersRepository : AbstractRepository<Order> {
        public OrdersRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<Order> FindAsync(long id) {
            return (
                from order in DbContext.Orders
                where order.Id == id
                select order
            ).FirstOrDefaultAsync();
        }

        public Task<List<Order>> ListAsync(long userId, int skip, int take) {
            return (
                from order in DbContext.Orders 
                where order.UserId == userId
                orderby order.Id
                select order
            ).Skip(skip).Take(take).ToListAsync();
        }

        public void Create(Order order) {
            DbContext.Orders.Add(order);
        }

        public void Delete(Order order) {
            DbContext.Orders.Remove(order);
        }
    }
}