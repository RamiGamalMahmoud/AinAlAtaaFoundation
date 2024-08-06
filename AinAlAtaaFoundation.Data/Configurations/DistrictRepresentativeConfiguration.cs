using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AinAlAtaaFoundation.Data.Configurations
{
    internal class DistrictRepresentativeConfiguration : IEntityTypeConfiguration<DistrictRepresentative>
    {
        public void Configure(EntityTypeBuilder<DistrictRepresentative> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasColumnType("VARCHAR(255)")
                .IsRequired();

            builder.HasOne(x => x.District)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(x => x.Address)
                .WithOne()
                .HasForeignKey<DistrictRepresentative>(x => x.AddressId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasMany(x => x.Phones)
                .WithMany();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}
