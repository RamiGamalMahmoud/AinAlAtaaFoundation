using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.SocialStatuses
{
    internal class Repository(IAppDbContextFactory dbContextFactory) : RepositoryBase<SocialStatus, SocialStatusDataModel>(dbContextFactory)
    {
        public override async Task<SocialStatus> Create(SocialStatusDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                SocialStatus socialStatus = new SocialStatus() { Name = dataModel.Name };

                dbContext.SocialStatuses.Add(socialStatus);

                await dbContext.SaveChangesAsync();
                dataModel.UpdateModel(socialStatus);
                _entities?.Add(socialStatus);
                return socialStatus;
            }
        }

        public override async Task<IEnumerable<SocialStatus>> GetAll(bool reload = false)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isLoaded || reload)
                {
                    IEnumerable<SocialStatus> socialStatuses = await dbContext.SocialStatuses.ToListAsync();
                    SetEntities(socialStatuses);
                }

                return _entities;

            }
        }

        public override Task<bool> Update(SocialStatusDataModel dataModel)
        {
            throw new NotImplementedException();
        }
    }
}
