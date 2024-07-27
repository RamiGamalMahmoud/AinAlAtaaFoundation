using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AinAlAtaaFoundation.Data.Configurations
{
    internal class DisbursementConfiguration : IEntityTypeConfiguration<Disbursement>
    {
        public void Configure(EntityTypeBuilder<Disbursement> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Family)
                .WithMany()
                .HasForeignKey(x => x.FamilyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
