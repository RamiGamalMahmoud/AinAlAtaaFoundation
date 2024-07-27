using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.OrphanTypes
{
    internal class GetAllCommandHandler(AppDbContextFactory dbContextFactory) : IRequestHandler<Shared.Commands.Generic.GetAllCommand<OrphanType>, IEnumerable<OrphanType>>
    {
        public async Task<IEnumerable<OrphanType>> Handle(Generic.GetAllCommand<OrphanType> request, CancellationToken cancellationToken)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                return await dbContext.OrphanTypes.ToListAsync();
            }
        }
    
        private readonly AppDbContextFactory _dbContextFactory = dbContextFactory;
    }
}
