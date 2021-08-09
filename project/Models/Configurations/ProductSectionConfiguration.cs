using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.Configurations {
    public class ProductSectionConfiguration : IEntityTypeConfiguration<ProductSection>
    {
        public void Configure(EntityTypeBuilder<ProductSection> builder)
        {
            builder.HasKey(pr => new { pr.ProductId, pr.SectionId });
        }
    }
}