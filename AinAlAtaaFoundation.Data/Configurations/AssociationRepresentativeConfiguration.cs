using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AinAlAtaaFoundation.Data.Configurations
{
    internal class AssociationRepresentativeConfiguration : IEntityTypeConfiguration<AssociationRepresentative>
    {
        public void Configure(EntityTypeBuilder<AssociationRepresentative> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("VARCHAR(20)");
        }
    }
}
