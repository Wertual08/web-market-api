using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Api.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Database {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Record> Records { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<OrderState> OrderStates { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }
        public event EventHandler<ModelEventArgs<Product>> ProductCreated;
        public event EventHandler<ModelEventArgs<Product>> ProductUpdated;
        public event EventHandler<ModelEventArgs<Product>> ProductDeleted;

        private void NotifyProducts() {
            foreach (var entry in ChangeTracker.Entries<Product>()) {
                switch (entry.State) {
                    case EntityState.Added: ProductCreated?.Invoke(
                        this, 
                        new ModelEventArgs<Product> { 
                            Model = entry.Entity 
                        }
                    ); break;
                    case EntityState.Modified: ProductUpdated?.Invoke(
                        this, 
                        new ModelEventArgs<Product> { 
                            Model = entry.Entity 
                        }
                    ); break;
                    case EntityState.Deleted: ProductDeleted?.Invoke(
                        this, 
                        new ModelEventArgs<Product> { 
                            Model = entry.Entity 
                        }
                    ); break;
                }
            }
        }

        public DbSet<Category> Categories { get; set; }
        
        public DbSet<Section> Sections { get; set; }

        public DbSet<ProductRecord> ProductRecords { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSection> ProductSections { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }

        public override int SaveChanges() {
            // TODO: It would be much safer to fire events after changes were saved
            NotifyProducts();
            return base.SaveChanges();
        }


        public void SeedConstants() {
            var userRoles = UserRoles.ToList();
            foreach (var item in Enum.GetValues<UserRoleId>()) {
                var userRole = userRoles.FirstOrDefault(userRole => userRole.Id == (int)item);
                if (userRole is null) {
                    userRole = new UserRole {
                        Id = (int)item,
                    };
                    UserRoles.Add(userRole);
                }
                userRole.Name = item.ToString();
            }

            var orderStates = OrderStates.ToList();
            foreach (var item in Enum.GetValues<OrderStateId>()) {
                var orderState = orderStates.FirstOrDefault(orderState => orderState.Id == (int)item);
                if (orderState is null) {
                    orderState = new OrderState {
                        Id = (int)item,
                    };
                    OrderStates.Add(orderState);
                }
                orderState.Name = item.ToString();
            }
            
            this.SaveChanges();
        }
    }
}