using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Features.FamiliesManagement;
using AinAlAtaaFoundation.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AinAlAtaaFoundation.Tests.Families
{
    [Collection("AppDbFactoryCollection")]
    public class DB_Tests
    {
        public DB_Tests(TestDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _repository = new Features.FamiliesManagement.Repository(_dbContextFactory);
        }

        [Fact]
        public async Task CreateFamilyTest()
        {
            Clan clan;
            Branch branch;
            BranchRepresentative branchRepresentative;
            District district;
            DistrictRepresentative districtRepresentative;
            Gender gender;
            FamilyType familyType;
            SocialStatus socialStatus;

            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                familyType = await dbContext.FamilyTypes.FirstOrDefaultAsync();
                gender = await dbContext.Genders.FirstOrDefaultAsync();
                socialStatus = await dbContext.SocialStatuses.FirstOrDefaultAsync();

                branchRepresentative = await dbContext.BranchRepresentatives.FirstOrDefaultAsync();
                clan = await dbContext.Clans.Where(x => x.Id == branchRepresentative.ClanId).FirstOrDefaultAsync();
                branch = await dbContext.Branches.Where(x => x.ClanId == branchRepresentative.ClanId).FirstOrDefaultAsync();

                districtRepresentative = await dbContext.DistrictRepresentatives.FirstOrDefaultAsync();
                district = await dbContext.Districts.Where(x => x.Id == districtRepresentative.DistrictId).FirstOrDefaultAsync();
            }

            FamilyDataModel dataModel = new FamilyDataModel(null)
            {
                Name = "applicant name",
                RationCard = "12015",
                RationCardOwnerName = "ration card 12015 owner",
                YearOfBirth = 2000,
                Gender = gender,
                Clan = clan,
                Branch = branch,
                BranchRepresentative = branchRepresentative,
                District = district,
                DistrictRepresentative = districtRepresentative,
                FamilyType = familyType,
                SocialStatus = socialStatus,
            };

            dataModel.Phones.Add(new Phone() { Number = "123456789"});
            dataModel.Phones.Add(new Phone() { Number = "23456789" });
            dataModel.Phones.Add(new Phone() { Number = "3456789" });
            dataModel.Phones.Add(new Phone() { Number = "3456789" });

            Family family = await _repository.Create(dataModel);
            family.Phones.Count.Should().Be(4);
            family.Should().NotBeNull();
        }

        readonly IAppDbContextFactory _dbContextFactory;
        private readonly Features.FamiliesManagement.Repository _repository;
    }
}
