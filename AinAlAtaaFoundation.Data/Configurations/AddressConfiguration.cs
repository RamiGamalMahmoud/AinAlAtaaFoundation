using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AinAlAtaaFoundation.Data.Configurations
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("addresses");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Street)
                .IsRequired(false)
                .HasColumnType("VARCHAR(50)");

            builder.HasOne(x => x.District)
                .WithMany()
                .HasForeignKey(nameof(District) + "Id")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(x => x.FeaturedPoint)
                .WithMany()
                .HasForeignKey(nameof(FeaturedPoint) + "Id")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}
