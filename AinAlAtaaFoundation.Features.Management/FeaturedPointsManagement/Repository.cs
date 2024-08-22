using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.FeaturedPointsManagement
{
    internal class Repository(AppDbContextFactory dbContextFactory) : RepositoryBase<FeaturedPoint, FeaturedPointDataModel>(dbContextFactory)
    {
        public override async Task<FeaturedPoint> Create(FeaturedPointDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                FeaturedPoint featuredPoint = new FeaturedPoint()
                {
                    Name = dataModel.Name,
                    DistrictId = dataModel.District.Id
                };

                try
                {
                    dbContext.FeaturedPoints.Add(featuredPoint);
                    await dbContext.SaveChangesAsync();
                    dataModel.UpdateModel(featuredPoint);
                    _entities.Add(featuredPoint);
                    return featuredPoint;
                }
                catch (System.Exception)
                {
                    return null;
                }
            }
        }

        public override async Task<IEnumerable<FeaturedPoint>> GetAll(bool reload = false)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isLoaded || reload)
                {
                    IEnumerable<FeaturedPoint> featuredPoints = await dbContext
                        .FeaturedPoints
                        .Include(x => x.District)
                        .ToListAsync();

                    SetEntities(featuredPoints);
                }
                return _entities;
            }
        }

        public async Task<IEnumerable<FeaturedPoint>> GetByDistrict(District district)
        {
            if(_entities.Any())
            {
                return _entities.Where(x => x.District == district);
            }
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .FeaturedPoints
                    .Include(x => x.District)
                    .Where(x => x.District == district)
                    .ToListAsync();
            }
        }

        public override async Task<bool> Update(FeaturedPointDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                FeaturedPoint featuredPoint = await dbContext.FeaturedPoints.FindAsync(dataModel.Model.Id);

                featuredPoint.Name = dataModel.Name;
                featuredPoint.DistrictId = dataModel.District.Id;

                try
                {
                    dbContext.FeaturedPoints.Update(featuredPoint);
                    await dbContext.SaveChangesAsync();
                    dataModel.UpdateModel(featuredPoint);
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
