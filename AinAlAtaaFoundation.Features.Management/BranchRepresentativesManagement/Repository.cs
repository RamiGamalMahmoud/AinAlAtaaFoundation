using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using HandyControl.Tools.Extension;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement
{
    internal class Repository(AppDbContextFactory dbContextFactory) : RepositoryBase<BranchRepresentative, BranchRepresentativeDataModel>(dbContextFactory)
    {
        public override async Task<BranchRepresentative> Create(BranchRepresentativeDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                BranchRepresentative branchRepresentative = new BranchRepresentative
                {
                    Name = dataModel.Name,
                    IsActive = true,
                    CreatedAt = DateTime.Now
                };

                if (dataModel.HasBranch)
                {
                    IEnumerable<BranchRepresentative> stored = await dbContext
                        .BranchRepresentatives
                        .Where(x => x.BranchId == dataModel.Branch.Id).ToListAsync();

                    foreach (BranchRepresentative item in stored)
                    {
                        item.IsActive = false;
                    }

                    dbContext.BranchRepresentatives.UpdateRange(stored);
                    branchRepresentative.BranchId = dataModel.Branch.Id;
                    branchRepresentative.ClanId = dataModel.Clan.Id;
                }
                else
                {
                    branchRepresentative.ClanId = dataModel.Clan.Id;
                }

                branchRepresentative.Phones.AddRange(dataModel.Phones);

                dbContext.BranchRepresentatives.Add(branchRepresentative);
                await dbContext.SaveChangesAsync();
                branchRepresentative.Branch = dataModel.Branch;
                _entities.Add(branchRepresentative);

                return branchRepresentative;
            }
        }

        public override async Task<IEnumerable<BranchRepresentative>> GetAll(bool reload = false)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isLoaded || reload)
                {
                    IEnumerable<BranchRepresentative> branchRepresentatives = await dbContext
                        .BranchRepresentatives
                        .Include(x => x.Clan)
                        .Include(x => x.Branch)
                            .ThenInclude(x => x.Clan)
                        .Include(x => x.Phones)
                        .OrderBy(x => x.Name)
                        .Where(x => x.IsActive)
                        .ToListAsync();
                    SetEntities(branchRepresentatives);
                }
            }
            return _entities;
        }

        public override async Task<bool> Update(BranchRepresentativeDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                BranchRepresentative stored = await dbContext
                    .BranchRepresentatives
                    .Include(x => x.Phones)
                    .Where(x => x.Id == dataModel.Model.Id)
                    .FirstOrDefaultAsync();

                stored.Name = dataModel.Name;
                if (dataModel.Branch is not null) stored.BranchId = dataModel.Branch.Id;


                IEnumerable<int> phonesIds = dataModel.Phones.Select(x => x.Id).Where(x => x > 0);
                IEnumerable<Phone> newPhones = dataModel.Phones.Where(x => x.Id == 0);
                IEnumerable<Phone> oldPhones = dbContext.Set<Phone>().Where(x => phonesIds.Contains(x.Id));

                stored.Phones.Clear();
                stored.Phones.AddRange(newPhones);
                stored.Phones.AddRange(oldPhones);

                dbContext.BranchRepresentatives.Update(stored);
                await dbContext.SaveChangesAsync();
                dataModel.UpdateModel();
                return true;
            }
        }
    }
}
