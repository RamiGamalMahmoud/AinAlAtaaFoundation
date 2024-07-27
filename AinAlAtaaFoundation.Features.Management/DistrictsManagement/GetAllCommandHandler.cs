using AinAlAtaaFoundation.Models;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictsManagement
{
    internal class GetAllCommandHandler(Repository repository) : IRequestHandler<Shared.Commands.Generic.GetAllCommand<District>, IEnumerable<District>>
    {
        public async Task<IEnumerable<District>> Handle(Shared.Commands.Generic.GetAllCommand<District> request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll();
        }

        private readonly Repository _repository = repository;
    }
}
