using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
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
                    .Where(x => x.Date == date)
                    .ToListAsync();
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
                    .Where(x => x.FamilyId == familyId)
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

        private readonly AppDbContextFactory _dbContextFactory = dbContextFactory;
    }
}
