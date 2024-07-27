using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AinAlAtaaFoundation.Data.Configurations
{
    internal class FamilyTypeConfiguration : IEntityTypeConfiguration<FamilyType>
    {
        public void Configure(EntityTypeBuilder<FamilyType> builder)
        {
            //builder.ToTable("family_types");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnType("VARCHAR(20)").IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasData(new[]
            {
                new { Id = 1, Name = "عام"},
                new { Id = 2, Name = "خاص"},
            });
        }
    }
}
