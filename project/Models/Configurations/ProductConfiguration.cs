using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.Configurations {
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasMany(r => r.Records)
                .WithMany(p => p.Products)
                .UsingEntity<ProductRecord>(
                    p => p.HasOne(p => p.Record).WithMany(),
                    r => r.HasOne(r => r.Product).WithMany()
                );
        }
    }
}