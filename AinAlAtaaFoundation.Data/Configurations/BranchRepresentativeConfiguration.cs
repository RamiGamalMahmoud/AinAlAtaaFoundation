using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AinAlAtaaFoundation.Data.Configurations
{
    internal class BranchRepresentativeConfiguration : IEntityTypeConfiguration<BranchRepresentative>
    {
        public void Configure(EntityTypeBuilder<BranchRepresentative> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("VARCHAR(20)");

            builder
                .HasOne(x => x.Branch)
                .WithMany(x => x.BranchRepresentatives)
                .HasForeignKey(x => x.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Clan)
                .WithMany()
                .HasForeignKey(x => x.ClanId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(x => x.Phones)
                .WithMany();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

        }
    }
}
