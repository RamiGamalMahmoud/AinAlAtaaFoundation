using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Listing
{
    internal class GetAllCommandHandler(Repository repository) : IRequestHandler<Shared.Commands.Generic.GetAllCommand<Family>, IEnumerable<Family>>
    {
        public async Task<IEnumerable<Family>> Handle(Generic.GetAllCommand<Family> request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request.Reload);
        }

        private readonly Repository _repository = repository;
    }
}
