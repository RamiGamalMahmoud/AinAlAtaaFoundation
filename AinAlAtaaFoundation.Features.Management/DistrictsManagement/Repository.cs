using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictsManagement
{
    internal class Repository(AppDbContextFactory dbContextFactory) : RepositoryBase<District, DistrictDataModel>(dbContextFactory)
    {
        public override async Task<District> Create(DistrictDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                District district = new District() { Name = dataModel.Name };

                dbContext.Districts.Add(district);

                try
                {
                    await dbContext.SaveChangesAsync();
                    _entities.Add(district);
                    return district;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public override async Task<IEnumerable<District>> GetAll(bool reload = false)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if(!_isLoaded || reload)
                {
                    IEnumerable<District> districts = await dbContext
                        .Districts
                        .OrderBy(x => x.Name)
                        .ToListAsync();
                    SetEntities(districts);
                }

                return _entities;
            }
        }

        public override async Task<bool> Update(DistrictDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                District district = await dbContext.Districts.FindAsync(dataModel.Model.Id);
                district.Name = dataModel.Name;

                dbContext.Districts.Update(district);

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
