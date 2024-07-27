using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AinAlAtaaFoundation.Data.Configurations
{
    internal class PhoneConfiguration : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.ToTable("phones");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Number).HasColumnType("VARCHAR(15)").IsRequired();
        }
    }
}
