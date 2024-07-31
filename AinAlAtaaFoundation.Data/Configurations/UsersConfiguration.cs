﻿using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AinAlAtaaFoundation.Data.Configurations
{
    internal class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.UserName)
                .IsRequired()
                .HasColumnType("VARCHAR(10)")
                .HasMaxLength(10);

            builder
                .HasIndex(x => x.UserName)
                .IsUnique();

            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder
                .Property<string>("Password")
                .HasColumnType("VARCHAR(20)")
                .HasMaxLength(20);

            builder
                .Property(x => x.IsAdmin)
                .HasDefaultValue(false);

            builder.OwnsOne(x => x.DeletableModel)
                .Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.OwnsOne(x => x.DeletableModel)
                .Property(x => x.DeletedAt)
                .HasDefaultValue(null);

            builder.HasData(new { Id = 1, UserName = "admin", Password = "admin", IsAdmin = true, IsActive = true});
        }
    }
}
