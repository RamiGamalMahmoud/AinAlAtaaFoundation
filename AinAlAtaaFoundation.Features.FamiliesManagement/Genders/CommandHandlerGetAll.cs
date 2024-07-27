using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Genders
{
    public class CommandHandlerGetAll(AppDbContextFactory dbContextFactory) : IRequestHandler<Shared.Commands.Generic.GetAllCommand<Gender>, IEnumerable<Gender>>
    {
        public async Task<IEnumerable<Gender>> Handle(Generic.GetAllCommand<Gender> request, CancellationToken cancellationToken)
        {
            using (AppDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                _genders ??= await dbContext.Genders.ToListAsync();
                return _genders;
            }
        }

        private static IEnumerable<Gender> _genders;
        private readonly AppDbContextFactory _dbContextFactory = dbContextFactory;
    }
}
