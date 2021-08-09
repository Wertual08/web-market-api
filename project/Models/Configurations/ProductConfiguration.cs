using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.Configurations {
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> bldr)
        {
            bldr.HasMany(x => x.Records)
                .WithMany(x => x.Products)
                .UsingEntity<ProductRecord>(
                    x => x.HasOne(x => x.Record).WithMany(),
                    x => x.HasOne(x => x.Product).WithMany()
                );
            
            bldr.HasMany(x => x.Categories)
                .WithMany(x => x.Products)
                .UsingEntity<ProductCategory>(
                    x => x.HasOne(x => x.Category).WithMany(),
                    x => x.HasOne(x => x.Product).WithMany()
                );
            
            bldr.HasMany(x => x.Sections)
                .WithMany(x => x.Products)
                .UsingEntity<ProductSection>(
                    x => x.HasOne(x => x.Section).WithMany(),
                    x => x.HasOne(x => x.Product).WithMany()
                );
        }
    }
}