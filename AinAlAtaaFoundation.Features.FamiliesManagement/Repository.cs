using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using HandyControl.Tools.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement
{
    internal class Repository(IAppDbContextFactory dbContextFactory) : RepositoryBase<Family, FamilyDataModel>(dbContextFactory)
    {
        public override async Task<Family> Create(FamilyDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                IDbContextTransaction transation = dbContext.Database.BeginTransaction();
                try
                {
                    FamilyMember applicant = new FamilyMember
                    {
                        Name = dataModel.Name,
                        YearOfBirth = dataModel.YearOfBirth,
                        IsApplicant = true,
                        GenderId = dataModel.Gender.Id
                    };

                    Address address = new Address
                    {
                        DistrictId = dataModel.District.Id,
                        Street = dataModel.Street,
                        FeaturedPointId = dataModel.FeaturedPoint?.Id
                    };

                    dbContext.Addresses.Add(address);
                    await dbContext.SaveChangesAsync();

                    Family family = new Family
                    {
                        BranchId = dataModel.Branch?.Id,
                        AddressId = address.Id,
                        BranchRepresentativeId = dataModel.BranchRepresentative.Id,
                        ClanId = dataModel.Clan.Id,
                        DistrictRepresentativeId = dataModel.DistrictRepresentative.Id,
                        FamilyTypeId = dataModel.FamilyType.Id,
                        Notes = dataModel.Notes,
                        OrphanTypeId = dataModel.OrphanType?.Id,
                        RationCard = dataModel.RationCard,
                        RationCardOwnerName = dataModel.RationCardOwnerName,
                        SocialStatusId = dataModel.SocialStatus.Id,
                        IsSponsored = dataModel.IsSponsered
                    };

                    foreach (Phone phone in dataModel.Phones)
                    {
                        family.Phones.Add(phone);
                    }

                    dbContext.Families.Add(family);
                    await dbContext.SaveChangesAsync();


                    applicant.FamilyId = family.Id;
                    dbContext.FamilyMembers.Add(applicant);
                    await dbContext.SaveChangesAsync();

                    family.ApplicantId = applicant.Id;
                    await dbContext.SaveChangesAsync();

                    await transation.CommitAsync();
                    dataModel.UpdateModel(family);
                    _entities.Add(family);
                    return family;
                }
                catch (Exception)
                {
                    await transation.RollbackAsync();
                    return null;
                }
            }
        }

        public override async Task<bool> Update(FamilyDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Address address = await dbContext.Addresses.Where(x => x.Id == dataModel.Address.Id).FirstOrDefaultAsync();
                FamilyMember applicant = await dbContext.FamilyMembers.Where(x => x.Id == dataModel.Applicant.Id).FirstOrDefaultAsync();

                address.Street = dataModel.Street;
                address.DistrictId = dataModel.District.Id;
                address.FeaturedPointId = dataModel.FeaturedPoint?.Id;

                applicant.Name = dataModel.Name;
                applicant.YearOfBirth = dataModel.YearOfBirth;
                applicant.GenderId = dataModel.Gender.Id;

                Family stored = await dbContext
                    .Families
                    .Include(x => x.Phones)
                    .Include(x => x.FamilyMembers)
                    .Where(x => x.Id == dataModel.Model.Id)
                    .FirstOrDefaultAsync();

                stored.RationCard = dataModel.RationCard;
                stored.RationCardOwnerName = dataModel.RationCardOwnerName;
                stored.BranchId = dataModel.Branch?.Id;
                stored.ClanId = dataModel.Clan.Id;
                stored.BranchRepresentativeId = dataModel.BranchRepresentative.Id;
                stored.DistrictRepresentativeId = dataModel.DistrictRepresentative.Id;
                stored.SocialStatusId = dataModel.SocialStatus.Id;
                stored.OrphanTypeId = dataModel.OrphanType?.Id;
                stored.FamilyTypeId = dataModel.FamilyType.Id;
                stored.Notes = dataModel.Notes;
                stored.IsSponsored = dataModel.IsSponsered;

                IEnumerable<int> phonesIds = dataModel.Phones.Select(x => x.Id).Where(x => x > 0);
                IEnumerable<Phone> newPhones = dataModel.Phones.Where(x => x.Id == 0);
                IEnumerable<Phone> oldPhones = dbContext.Set<Phone>().Where(x => phonesIds.Contains(x.Id));

                stored.Phones.Clear();
                stored.Phones.AddRange(newPhones);
                stored.Phones.AddRange(oldPhones);

                dbContext.Addresses.Update(address);
                dbContext.FamilyMembers.Update(applicant);
                dbContext.Families.Update(stored);

                try
                {
                    await dbContext.SaveChangesAsync();
                    dataModel.UpdateModel(dataModel.Model);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }

        public override async Task<IEnumerable<Family>> GetAll(bool reload = false)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isLoaded || reload)
                {
                    IEnumerable<Family> families = await dbContext
                        .Families
                        .Include(x => x.Address)
                            .ThenInclude(x => x.FeaturedPoint)
                        .Include(x => x.Applicant)
                            .ThenInclude(x => x.Gender)
                        .Include(x => x.Branch)
                        .Include(x => x.BranchRepresentative)
                        .Include(x => x.Clan)
                        .Include(x => x.DistrictRepresentative)
                            .ThenInclude(x => x.District)
                        .Include(x => x.FamilyMembers)
                        .Include(x => x.FamilyType)
                        .Include(x => x.OrphanType)
                        .Include(x => x.Phones)
                        .Include(x => x.SocialStatus)
                        .Where(x => !x.IsDelted)
                        .ToListAsync();
                    SetEntities(families);
                }
                return _entities;
            }
        }

        public async Task<IEnumerable<Family>> GetAllDeletedAsync()
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Families
                    .Include(x => x.Address)
                        .ThenInclude(x => x.FeaturedPoint)
                    .Include(x => x.Applicant)
                        .ThenInclude(x => x.Gender)
                    .Include(x => x.Branch)
                    .Include(x => x.BranchRepresentative)
                    .Include(x => x.Clan)
                    .Include(x => x.DistrictRepresentative)
                        .ThenInclude(x => x.District)
                    .Include(x => x.FamilyMembers)
                    .Include(x => x.FamilyType)
                    .Include(x => x.OrphanType)
                    .Include(x => x.Phones)
                    .Include(x => x.SocialStatus)
                    .Where(x => x.IsDelted)
                    .ToListAsync();
            }
        }

        public async Task<Family> GetByRationCard(string RationCard)
        {
            Family family;
            family = _entities
                .Where(x => x.RationCard == RationCard)
                .FirstOrDefault();

            if (family is null)
            {
                using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
                {
                    family = await dbContext.Families
                        .Include(x => x.Address)
                        .Include(x => x.Applicant)
                            .ThenInclude(x => x.Gender)
                        .Include(x => x.Branch)
                        .Include(x => x.BranchRepresentative)
                        .Include(x => x.Clan)
                        .Include(x => x.DistrictRepresentative)
                            .ThenInclude(x => x.District)
                        .Include(x => x.FamilyMembers)
                        .Include(x => x.FamilyType)
                        .Include(x => x.OrphanType)
                        .Include(x => x.Phones)
                        .Include(x => x.SocialStatus)
                        .Where(x => x.RationCard == RationCard)
                        .FirstOrDefaultAsync();
                }
            }
            return family;
        }

        public async Task<IEnumerable<Family>> GetByRationCardOwner(string RationCardOwnerName)
        {
            IEnumerable<Family> families;
            families = _entities.Where(x => x.RationCardOwnerName.Contains(RationCardOwnerName)).ToList();

            if (families is null || !families.Any())
            {
                using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
                {
                    families = await dbContext.Families
                        .Include(x => x.Address)
                        .Include(x => x.Applicant)
                            .ThenInclude(x => x.Gender)
                        .Include(x => x.Branch)
                        .Include(x => x.BranchRepresentative)
                        .Include(x => x.Clan)
                        .Include(x => x.DistrictRepresentative)
                            .ThenInclude(x => x.District)
                        .Include(x => x.FamilyMembers)
                        .Include(x => x.FamilyType)
                        .Include(x => x.OrphanType)
                        .Include(x => x.Phones)
                        .Include(x => x.SocialStatus)
                        .Where(x => x.RationCardOwnerName.Contains(RationCardOwnerName))
                        .ToListAsync();
                }
            }
            return families;
        }

        /// <summary>
        /// Get Family by its id, search firts in the stored families else search in the database
        /// </summary>
        /// <param name="id">Family id</param>
        /// <returns>Models.Family</returns>
        public async Task<Family> Get(int id)
        {
            if (_entities.Any())
            {
                return _entities
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
            }

            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Families
                    .Include(x => x.Address)
                    .Include(x => x.Applicant)
                        .ThenInclude(x => x.Gender)
                    .Include(x => x.Branch)
                    .Include(x => x.BranchRepresentative)
                    .Include(x => x.Clan)
                    .Include(x => x.DistrictRepresentative)
                        .ThenInclude(x => x.District)
                    .Include(x => x.FamilyMembers)
                    .Include(x => x.FamilyType)
                    .Include(x => x.OrphanType)
                    .Include(x => x.Phones)
                    .Include(x => x.SocialStatus)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<bool> Remove(Family family)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {

                    Family familyToBeDeleted = await dbContext.Families.Where(x => x.Id == family.Id).SingleOrDefaultAsync();
                    IEnumerable<FamilyMember> familyMembersToBeDeleted = await dbContext.FamilyMembers.Where(x => x.Family.Id == family.Id).ToListAsync();

                    if (familyToBeDeleted is not null)
                    {
                        familyToBeDeleted.IsDelted = true;
                        dbContext.Families.Update(familyToBeDeleted);
                    }

                    if (familyMembersToBeDeleted.Any())
                    {
                        foreach (FamilyMember familyMember in familyMembersToBeDeleted)
                        {
                            familyMember.IsDelted = true;
                        }
                    }

                    dbContext.FamilyMembers.UpdateRange(familyMembersToBeDeleted);

                    await dbContext.SaveChangesAsync();
                    transaction.Commit();
                    _entities.Remove(family);
                    return true;
                }
            }
        }
    }
}
