using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AinAlAtaaFoundation.Data.Configurations
{
    internal class DatabaseInfoConfiguration : IEntityTypeConfiguration<DatabaseInfo>
    {
        public void Configure(EntityTypeBuilder<DatabaseInfo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Discreption)
                .IsRequired(false)
                .HasDefaultValue(null);

            builder.HasData(new DatabaseInfo() { Id = 1, DateUpdated = new DateTime(2024, 8, 30, 11, 50, 00), Version = 1, Discreption = "Initial Database Schema"});
        }
    }
}
