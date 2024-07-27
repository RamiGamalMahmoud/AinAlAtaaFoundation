using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement
{
    internal class GetAllCommandHandler(Repository repository) :
        IRequestHandler<Generic.GetAllCommand<DistrictRepresentative>, IEnumerable<DistrictRepresentative>>
    {
        public async Task<IEnumerable<DistrictRepresentative>> Handle(Generic.GetAllCommand<DistrictRepresentative> request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request.Reload);
        }

        private readonly Repository _repository = repository;
    }
}
