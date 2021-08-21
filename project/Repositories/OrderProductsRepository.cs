
using Api.Contexts;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositories {
    public class OrderProductsRepository : AbstractRepository<OrderProduct> {
        public OrderProductsRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public void Create(OrderProduct orderProduct) {
            DbContext.OrderProducts.Add(orderProduct);
        }

        public void Create(ICollection<OrderProduct> orderProduct) {
            DbContext.OrderProducts.AddRange(orderProduct);
        }

        public void Delete(OrderProduct orderProduct) {
            DbContext.OrderProducts.Remove(orderProduct);
        }
    }
}