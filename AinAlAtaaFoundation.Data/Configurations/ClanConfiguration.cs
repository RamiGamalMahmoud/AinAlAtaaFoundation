using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AinAlAtaaFoundation.Data.Configurations
{
    internal class ClanConfiguration : IEntityTypeConfiguration<Clan>
    {
        public void Configure(EntityTypeBuilder<Clan> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("VARCHAR(20)");
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
