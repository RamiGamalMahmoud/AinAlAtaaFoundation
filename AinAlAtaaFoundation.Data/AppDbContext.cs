using AinAlAtaaFoundation.Data.Configurations;
using AinAlAtaaFoundation.Models;
using Microsoft.EntityFrameworkCore;

namespace AinAlAtaaFoundation.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchRepresentative> BranchRepresentatives { get; set; }
        public DbSet<DistrictRepresentative> DistrictRepresentatives { get; set; }
        public DbSet<Family> Families { get; set; }

        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<FeaturedPoint> FeaturedPoints { get; set; }
        public DbSet<SocialStatus> SocialStatuses { get; set; }
        public DbSet<FamilyType> FamilyTypes { get; set; }
        public DbSet<OrphanType> OrphanTypes { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Disbursement> Disbursements { get; set; }
        public DbSet<DatabaseInfo> DatabaseInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IModelsConfigurationMarker).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
