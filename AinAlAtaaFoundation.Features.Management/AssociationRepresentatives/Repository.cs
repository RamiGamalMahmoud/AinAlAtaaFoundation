using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.AssociationRepresentatives
{
    internal class Repository(AppDbContextFactory dbContextFactory) : RepositoryBase<AssociationRepresentative, AssociationRepresentativeDataModel>(dbContextFactory)
    {
        public override async Task<AssociationRepresentative> Create(AssociationRepresentativeDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                AssociationRepresentative representative = new AssociationRepresentative
                {
                    Name = dataModel.Name
                };
                dbContext.AssociationRepresentatives.Add(representative);
                try
                {
                    await dbContext.SaveChangesAsync();
                    dataModel.UpdateModel(representative);
                    _entities.Add(representative);
                    return representative;

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public override async Task<IEnumerable<AssociationRepresentative>> GetAll(bool reload = false)
        {
            if (!_isLoaded || reload)
            {
                using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
                {
                    IEnumerable<AssociationRepresentative> representatives = await dbContext.AssociationRepresentatives.ToListAsync();
                    SetEntities(representatives);
                }
            }
            return _entities;
        }

        public override Task<bool> Update(AssociationRepresentativeDataModel dataModel)
        {
            throw new NotImplementedException();
        }
    }
}
