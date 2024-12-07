using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AinAlAtaaFoundation.Data.Configurations
{
    internal class FamilyConfiguration : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.RationCard)
                .HasColumnType("VARCHAR(20)")
                .IsRequired();

            builder.Property(x => x.RationCardOwnerName)
                .HasColumnType("VARCHAR(50)")
                .IsRequired();

            builder.Property(x => x.HusbandName)
                .HasColumnType("VARCHAR(50)")
                .IsRequired(false);

            builder.Property(x => x.IsSponsored)
                .HasDefaultValue(false);

            builder.HasIndex(x => x.RationCard).IsUnique();

            builder.Property(x => x.Notes)
                .HasColumnType("TEXT")
                .IsRequired(false);

            builder.HasOne(x => x.Clan)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.ClanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Branch)
                .WithMany()
                .HasForeignKey(x => x.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DistrictRepresentative)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.DistrictRepresentativeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.BranchRepresentative)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.BranchRepresentativeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Address)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.SocialStatus)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.SocialStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.FamilyType)
                .WithMany()
                .IsRequired()
                .HasForeignKey(x => x.FamilyTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Phones)
                .WithMany();

            builder.HasOne(x => x.OrphanType)
                .WithMany()
                .HasForeignKey(x => x.OrphanTypeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Applicant)
                .WithOne()
                .HasForeignKey<Family>(x => x.ApplicantId)
                .IsRequired(false);

            builder.Property(x => x.HasFlag)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.ImagePath)
                .HasColumnType("TEXT")
                .IsRequired(false);

            builder.Ignore(x => x.Image);

        }
    }
}
