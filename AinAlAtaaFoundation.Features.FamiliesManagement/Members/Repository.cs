using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Data.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members
{
    internal class Repository(AppDbContextFactory dbContextFactory) : RepositoryBase<FamilyMember, FamilyMemberDataModel>(dbContextFactory)
    {
        public override async Task<FamilyMember> Create(FamilyMemberDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                FamilyMember familyMember = new FamilyMember()
                {
                    Name = dataModel.Name,
                    BirthDate = dataModel.BirthDate,
                    FamilyId = dataModel.Family.Id,
                    GenderId = dataModel.Gender.Id,
                    MotherId = dataModel.Mother?.Id,
                    IsOrphan = dataModel.IsOrphan
                };

                dbContext.FamilyMembers.Add(familyMember);
                await dbContext.SaveChangesAsync();
                dataModel.UpdateModel(familyMember);
                _entities.Add(familyMember);
                return familyMember;
            }
        }

        public override async Task<IEnumerable<FamilyMember>> GetAll(bool reload = false)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                if (!_isLoaded || reload)
                {
                    IEnumerable<FamilyMember> familyMembers = await dbContext
                        .FamilyMembers
                        .Include(x => x.Gender)
                        .Include(x => x.Family)
                            .ThenInclude(x => x.Clan)
                        .Include(x => x.Family)
                            .ThenInclude(x => x.OrphanType)
                        .Include(x => x.Family)
                            .ThenInclude(x => x.SocialStatus)
                        .Include(x => x.Mother)
                        .ToListAsync();
                    SetEntities(familyMembers);
                }
                return _entities;
            }
        }

        public override async Task<bool> Update(FamilyMemberDataModel dataModel)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                FamilyMember familyMember = await dbContext.FamilyMembers
                    .Where(x => x.Id == dataModel.Model.Id)
                    .FirstOrDefaultAsync();

                familyMember.Name = dataModel.Name;
                familyMember.BirthDate = dataModel.BirthDate;
                familyMember.FamilyId = dataModel.Family.Id;
                familyMember.GenderId = dataModel.Gender.Id;
                familyMember.MotherId = dataModel.Mother?.Id;
                familyMember.IsOrphan = dataModel.IsOrphan;

                dbContext.FamilyMembers.Update(familyMember);
                await dbContext.SaveChangesAsync();
                dataModel.UpdateModel(dataModel.Model);
                return true;
            }
        }
    }
}
