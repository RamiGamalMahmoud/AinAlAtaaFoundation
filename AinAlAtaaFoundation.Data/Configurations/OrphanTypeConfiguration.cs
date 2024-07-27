using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AinAlAtaaFoundation.Data.Configurations
{
    internal class OrphanTypeConfiguration : IEntityTypeConfiguration<OrphanType>
    {
        public void Configure(EntityTypeBuilder<OrphanType> builder)
        {
            //builder.ToTable("orphan_types");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnType("VARCHAR(20)").IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasData(new[] 
            {
                new { Id = 1, Name = "يتيم الأب"},
                new { Id = 2, Name = "يتيم الأم"},
                new { Id = 3, Name = "يتيم الأب و الأم"},
            });
        }
    }
}
