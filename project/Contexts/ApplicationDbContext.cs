using app.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Contexts {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {

        }

        public DbSet<Product> Products { get; set; }
    }
}