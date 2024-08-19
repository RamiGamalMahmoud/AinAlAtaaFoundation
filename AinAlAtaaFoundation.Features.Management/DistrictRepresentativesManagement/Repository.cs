using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using HandyControl.Tools.Extension;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement
{
    public class Repository(AppDbContextFactory dbContextFactory) :
        RepositoryBase<DistrictRepresentative, DistrictRepresentativeDataModel>(dbContextFactory)
    {
        public override async Task<DistrictRepresentative> Create(DistrictRepresentativeDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                District district = await dbContext.Districts.FindAsync(dataModel.District.Id);

                DistrictRepresentative existedName = await dbContext
                    .DistrictRepresentatives
                    .Where(x => x.Name == dataModel.Name)
                    .FirstOrDefaultAsync();


                IEnumerable<DistrictRepresentative> stored = await dbContext
                    .DistrictRepresentatives
                    .Where(x => x.DistrictId == dataModel.District.Id && x.IsActive)
                    .ToListAsync();

                if (stored.Any())
                {
                    foreach (var x in stored)
                    {
                        x.IsActive = false;
                        dbContext.UpdateRange(stored);

                    }
                }

                Address address = new Address() { Street = dataModel.Street, DistrictId = dataModel.District.Id };
                DistrictRepresentative districtRepresentative = new DistrictRepresentative
                {
                    Name = dataModel.Name,
                    DistrictId = dataModel.District.Id,
                    Address = address,
                    IsActive = true
                };


                districtRepresentative.Phones.AddRange(dataModel.Phones);


                dbContext.DistrictRepresentatives.Add(districtRepresentative);
                await dbContext.SaveChangesAsync();
                districtRepresentative.Address.District = dataModel.District;

                districtRepresentative.District = dataModel.District;

                _entities.Add(districtRepresentative);
                return districtRepresentative;
            }
        }

        public override async Task<IEnumerable<DistrictRepresentative>> GetAll(bool reload = false)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isLoaded || reload)
                {
                    IEnumerable<DistrictRepresentative> districtRepresentatives = await dbContext
                        .DistrictRepresentatives
                        .Include(x => x.District)
                        .Include(x => x.Address)
                        .ThenInclude(x => x.District)
                        .Include(x => x.Phones)
                        .Where(x => x.IsActive)
                        .ToListAsync();
                    SetEntities(districtRepresentatives);
                }
            }
            return _entities;
        }

        public override async Task<bool> Update(DistrictRepresentativeDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                DistrictRepresentative stored = await dbContext
                        .DistrictRepresentatives
                        .Include(x => x.Phones)
                        .Include(x => x.Address)
                        .Where(x => x.Id == dataModel.Model.Id)
                        .FirstOrDefaultAsync();

                stored.Name = dataModel.Name;
                stored.DistrictId = dataModel.District.Id;
                stored.Address.DistrictId = dataModel.District.Id;
                stored.Address.Street = dataModel.Street;

                IEnumerable<int> phonesIds = dataModel.Phones.Select(x => x.Id).Where(x => x > 0);
                IEnumerable<Phone> newPhones = dataModel.Phones.Where(x => x.Id == 0);
                IEnumerable<Phone> oldPhones = dbContext.Set<Phone>().Where(x => phonesIds.Contains(x.Id));

                stored.Phones.Clear();
                stored.Phones.AddRange(newPhones);
                stored.Phones.AddRange(oldPhones);

                dbContext.DistrictRepresentatives.Update(stored);
                await dbContext.SaveChangesAsync();
                dataModel.UpdateModel();
                return true;

            }
        }

        internal async Task<IEnumerable<DistrictRepresentative>> GetByDistrict(District district)
        {
            if (district is null) return [];
            if (_entities is not null && _entities.Any()) return _entities.Where(x => x.Id == district.Id);

            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .DistrictRepresentatives
                    .Include(x => x.District)
                    .Include(x => x.Address)
                    .ThenInclude(x => x.District)
                    .Include(x => x.Phones)
                    .Where(x => x.District.Id == district.Id)
                    .ToListAsync();

            }
        }
    }
}
