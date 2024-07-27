using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.ClansManagement
{
    internal class GetAllCommandHandler(Repository repository) : IRequestHandler<Generic.GetAllCommand<Clan>, IEnumerable<Clan>>
    {
        public async Task<IEnumerable<Clan>> Handle(Generic.GetAllCommand<Clan> request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll();
        }

        private readonly Repository _repository = repository;
    }
}
