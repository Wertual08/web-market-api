
using Api.Database;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Repositories {
    public class OrderProductsRepository : AbstractRepository<OrderProduct> {
        public OrderProductsRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public Task<List<OrderProduct>> ListAsync(long orderId) {
            return (
                from orderProduct in DbContext.OrderProducts
                where orderProduct.OrderId == orderId
                select orderProduct
            )
            .Include(x => x.Product)
            .ThenInclude(x => x.Records)
            .ToListAsync();
        }

        public void Create(OrderProduct orderProduct) {
            DbContext.OrderProducts.Add(orderProduct);
        }

        public void Create(IEnumerable<OrderProduct> orderProduct) {
            DbContext.OrderProducts.AddRange(orderProduct);
        }

        public void Delete(OrderProduct orderProduct) {
            DbContext.OrderProducts.Remove(orderProduct);
        }
    }
}