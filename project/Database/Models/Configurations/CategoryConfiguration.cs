using Api.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models.Configurations {
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> bldr)
        {
            bldr.HasOne(x => x.Record)
                .WithMany()
                .HasForeignKey(x => x.RecordId);
        }
    }
}