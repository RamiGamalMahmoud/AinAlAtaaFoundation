using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AinAlAtaaFoundation.Data.Configurations
{
    internal class FamilyMemberConfiguration : IEntityTypeConfiguration<FamilyMember>
    {
        public void Configure(EntityTypeBuilder<FamilyMember> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR(50)")
                .IsRequired();

            builder.Property(x => x.Surname)
                .HasColumnType("VARCHAR(50)")
                .IsRequired();

            builder.Property(x => x.BirthDate)
                .IsRequired();

            builder.HasOne(x => x.Family)
                .WithMany(x => x.FamilyMembers)
                .HasForeignKey(x => x.FamilyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Gender)
                .WithMany()
                .HasForeignKey(x => x.GenderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Mother)
                .WithMany()
                .HasForeignKey(x => x.MotherId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
