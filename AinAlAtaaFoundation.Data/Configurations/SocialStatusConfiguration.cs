using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AinAlAtaaFoundation.Data.Configurations
{
    internal class SocialStatusConfiguration : IEntityTypeConfiguration<SocialStatus>
    {
        public void Configure(EntityTypeBuilder<SocialStatus> builder)
        {
            //builder.ToTable("social_statuses");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnType("VARCHAR(20)").IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasData(new[]
            {
                new { Id = 1, Name = "أيتام"},
                new { Id = 2, Name = "متعفف"},
                new { Id = 3, Name = "أرامل"},
                new { Id = 4, Name = "مطلقات"},
                new { Id = 5, Name = "الزوج مفقود"},
                new { Id = 6, Name = "أخرى"},
            });
        }
    }
}
