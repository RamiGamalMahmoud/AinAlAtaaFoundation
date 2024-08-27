using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.DisbursementManagement
{
    internal class Repository(AppDbContextFactory dbContextFactory)
    {
        public async Task<Disbursement> Create(Disbursement disbursement)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                disbursement.Date = DateTime.Now;
                disbursement.FamilyId = disbursement.Family.Id;
                Family family = disbursement.Family;
                disbursement.Family = null;

                dbContext.Disbursements.Add(disbursement);
                await dbContext.SaveChangesAsync();
                disbursement.Family = family;
                return disbursement;
            }
        }

        public async Task<IEnumerable<Disbursement>> GetByDate(DateTime date)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Disbursements
                    .Include(x => x.Family)
                        .ThenInclude(x => x.Applicant)
                    .OrderByDescending(x => x.Date)
                    .Where(x => x.Date == date)
                    .ToListAsync();
            }
        }

        public async Task<Disbursement> GetLastFamilyDisbursementById(int familyId)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Disbursements
                    .Include(x => x.Family)
                        .ThenInclude(x => x.Applicant)
                    .OrderByDescending(x => x.Date)
                    .Where(x => x.FamilyId == familyId)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<Disbursement> GetLastFamilyDisbursementByRationCard(string rationCard)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Disbursements
                    .Include(x => x.Family)
                        .ThenInclude(x => x.Applicant)
                    .OrderByDescending(x => x.Date)
                    .Where(x => x.Family.RationCard == rationCard)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<IEnumerable<Disbursement>> GetByFamily(int familyId)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Disbursements
                    .Include(x => x.Family)
                        .ThenInclude(x => x.Applicant)
                    .OrderByDescending(x => x.Date)
                    .Where(x => x.FamilyId == familyId)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<Disbursement>> GetByRationCard(string rationCard)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext
                    .Disbursements
                    .Include(x => x.Family)
                        .ThenInclude(x => x.Applicant)
                    .OrderByDescending(x => x.Date)
                    .Where(x => x.Family.RationCard == rationCard)
                    .ToListAsync();
            }
        }

        public async Task<int> GetLastTicketNumber(DateTime date)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                try
                {
                    return await dbContext
                        .Disbursements
                        .Where(x => x.Date.Date == date.Date)
                        .MaxAsync(x => x.TicketNumber);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public async Task<IEnumerable<Disbursement>> GetDisbursementsAsync(IFilterParameters parameters)
        {
            var isPropertiesIsNull = parameters
                .GetType()
                .GetProperties()
                .Select(p => p.GetValue(parameters))
                .All(v => v is null);

            if (isPropertiesIsNull) return [];

            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Disbursements
                    .Include(disbursement => disbursement.Family)
                        .ThenInclude(x => x.Applicant)
                    .OrderByDescending(x => x.Date)

                    .Where(x => parameters.Clan == null || x.Family.Clan == parameters.Clan)
                    .Where(x => parameters.Branch == null || x.Family.Branch == null || x.Family.Branch == parameters.Branch)
                    .Where(x => parameters.BranchRepresentative == null || x.Family.BranchRepresentative == null || x.Family.BranchRepresentative == parameters.BranchRepresentative)

                    .Where(x => parameters.District == null || x.Family.Address.District == parameters.District)
                    .Where(x => parameters.DistrictRepresentative == null || x.Family.DistrictRepresentative == parameters.DistrictRepresentative)

                    .Where(x => parameters.FamilyType == null || x.Family.FamilyType == parameters.FamilyType)
                    .Where(x => parameters.SocialStatus == null || x.Family.SocialStatus == parameters.SocialStatus)
                    .Where(x => parameters.OrphanType == null || x.Family.OrphanType == parameters.OrphanType)

                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<Disbursement>> GetDisbursementsHistoryByDate(DateTime date)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.Disbursements
                    .Include(x => x.Family)
                        .ThenInclude(x => x.Applicant)
                    .OrderByDescending(x => x.Date)
                    .Where(x => x.Date.Date == date.Date).ToListAsync();
            }
        }

        private readonly AppDbContextFactory _dbContextFactory = dbContextFactory;
    }
}
