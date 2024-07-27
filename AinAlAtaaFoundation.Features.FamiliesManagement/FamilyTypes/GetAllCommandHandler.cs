using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.FamilyTypes
{
    internal class GetAllCommandHandler(AppDbContextFactory dbContextFactory) : IRequestHandler<Shared.Commands.Generic.GetAllCommand<FamilyType>, IEnumerable<FamilyType>>
    {
        public async Task<IEnumerable<FamilyType>> Handle(Generic.GetAllCommand<FamilyType> request, CancellationToken cancellationToken)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.FamilyTypes.ToListAsync();
            }
        }

        private readonly AppDbContextFactory _dbContextFactory = dbContextFactory;
    }
}
