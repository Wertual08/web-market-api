using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.Configurations {
    public class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> bldr)
        {
            bldr.HasMany(x => x.Sections)
                .WithOne()
                .HasForeignKey("SectionId");
        }
    }
}