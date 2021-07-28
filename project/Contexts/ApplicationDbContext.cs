using System;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Contexts {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductRecord>().HasKey(nameof(ProductRecord.ProductId), nameof(ProductRecord.RecordId));
        }

        public DbSet<Record> Records { get; set; }

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

        public DbSet<ProductRecord> ProductRecords { get; set; }

        public override int SaveChanges() {
            // TODO: It would be much safer to fire events after changes were saved
            NotifyProducts();
            return base.SaveChanges();
        }
    }
}