using Api.Contexts;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories {
    public class OrdersRepository : AbstractRepository<Order> {
        public OrdersRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<Order> FindAsync(long id) {
            return (
                from order in DbContext.Orders
                where order.Id == id
                select order
            ).Include(x => x.OrderProducts).ThenInclude(x => x.Product).FirstOrDefaultAsync();
        }

        public Task<List<Order>> ListAsync(long userId, int skip, int take) {
            return (
                from order in DbContext.Orders 
                where order.UserId == userId
                orderby order.Id
                select order
            ).Skip(skip).Take(take).Include(x => x.OrderProducts).ThenInclude(x => x.Product).ToListAsync();
        }

        public void Create(Order order) {
            DbContext.Orders.Add(order);
        }

        public void Delete(Order order) {
            DbContext.Orders.Remove(order);
        }
    }
}