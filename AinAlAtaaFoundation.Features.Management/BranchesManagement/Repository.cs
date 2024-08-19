using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchesManagement
{
    internal class Repository(AppDbContextFactory dbContextFactory) : RepositoryBase<Branch, BranchDataModel>(dbContextFactory)
    {
        public override async Task<Branch> Create(BranchDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Branch branch = new Branch
                {
                    Name = dataModel.Name,
                    ClanId = dataModel.Clan.Id
                };

                dbContext.Branches.Add(branch);

                try
                {
                    await dbContext.SaveChangesAsync();
                    dataModel.UpdateModel(branch);
                    _entities.Add(branch);
                    return branch;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public async Task<IEnumerable<Branch>> GetByClan(Clan clan)
        {
            if (clan is null) return [];
            if (_entities is not null && _entities.Any()) return _entities.Where(x => x.Clan.Id == clan.Id);

            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                        .Branches
                        .Include(x => x.Clan)
                        .OrderBy(x => x.Clan.Name)
                        .OrderBy(x => x.Name)
                        .Where(x => x.Clan.Id == clan.Id)
                        .ToListAsync();
            }
        }

        public override async Task<IEnumerable<Branch>> GetAll(bool reload = false)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isLoaded || reload)
                {
                    IEnumerable<Branch> branches = await dbContext
                        .Branches
                        .Include(x => x.Clan)
                        .OrderBy(x => x.Clan.Name)
                        .OrderBy(x => x.Name)
                        .ToListAsync();
                    SetEntities(branches);
                }
                return _entities;
            }
        }

        public override async Task<bool> Update(BranchDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Branch stored = await dbContext
                    .Branches
                    .FindAsync(dataModel.Model.Id);
                stored.Name = dataModel.Name;
                stored.ClanId = dataModel.Clan.Id;

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
    }
}
