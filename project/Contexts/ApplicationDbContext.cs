using System;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Contexts {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {
        }
        
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }
        public event EventHandler<ModelEventArgs<Product>> ProductCreated;
        public event EventHandler<ModelEventArgs<Product>> ProductUpdated;
        public event EventHandler<ModelEventArgs<Product>> ProductDeleted;
        
        public DbSet<TokenBan> TokenBans { get; set; }

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
        public override int SaveChanges() {
            // TODO: It would be much safer to fire events after changes were saved
            NotifyProducts();
            return base.SaveChanges();
        }
    }
}