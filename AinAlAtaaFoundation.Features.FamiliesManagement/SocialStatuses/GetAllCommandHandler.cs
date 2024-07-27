using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.SocialStatuses
{
    internal class GetAllCommandHandler(AppDbContextFactory dbContextFactory) : IRequestHandler<Shared.Commands.Generic.GetAllCommand<SocialStatus>, IEnumerable<SocialStatus>>
    {
        public async Task<IEnumerable<SocialStatus>> Handle(Generic.GetAllCommand<SocialStatus> request, CancellationToken cancellationToken)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.SocialStatuses.ToListAsync();
            }
        }

        private readonly AppDbContextFactory _dbContextFactory = dbContextFactory;
    }
}
