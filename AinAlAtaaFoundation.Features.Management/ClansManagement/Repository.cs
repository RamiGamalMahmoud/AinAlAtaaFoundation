using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.ClansManagement
{
    internal class Repository(AppDbContextFactory dbContextFactory) : RepositoryBase<Clan, ClanDataModel>(dbContextFactory)
    {
        public override async Task<Clan> Create(ClanDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Clan clan = new Clan() { Name = dataModel.Name };

                dbContext.Add(clan);

                try
                {
                    await dbContext.SaveChangesAsync();
                    _entities.Add(clan);
                    return clan;
                }
                catch (System.Exception)
                {
                    return null;
                }
            }
        }

        public override async Task<IEnumerable<Clan>> GetAll(bool reload = false)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isLoaded || reload)
                {
                    IEnumerable<Clan> clans = await dbContext.Clans.OrderBy(x => x.Name).ToListAsync();
                    SetEntities(clans);
                }
                return _entities;
            }
        }

        public override async Task<bool> Update(ClanDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                Clan stored = await dbContext
                    .Clans
                    .Where(x => x.Id == dataModel.Model.Id)
                    .FirstOrDefaultAsync();

                stored.Name = dataModel.Name;

                try
                {
                    await dbContext.SaveChangesAsync();
                    dataModel.UpdateModel(dataModel.Model);
                    return true;
                }
                catch (System.Exception)
                {
                    return false;
                }

            }
        }
    }
}
