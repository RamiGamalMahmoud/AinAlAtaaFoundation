using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AinAlAtaaFoundation.Data.Configurations
{
    internal class FeaturedPointConfiguration : IEntityTypeConfiguration<FeaturedPoint>
    {
        public void Configure(EntityTypeBuilder<FeaturedPoint> builder)
        {
            builder.ToTable("featured_points");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.District)
                .WithMany()
                .HasForeignKey(nameof(District) + "Id")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
