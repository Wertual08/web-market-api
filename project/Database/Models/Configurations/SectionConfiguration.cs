using Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.Configurations {
    public class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> bldr)
        {
            bldr.HasOne(x => x.Record)
                .WithMany()
                .HasForeignKey(x => x.RecordId);
            bldr.HasMany(x => x.Sections)
                .WithOne()
                .HasForeignKey(x => x.SectionId);
        }
    }
}