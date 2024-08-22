using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.FeaturedPointsManagement
{
    internal class CommandHandlerGetAll(Repository repository) : IRequestHandler<Shared.Commands.Generic.GetAllCommand<FeaturedPoint>, IEnumerable<FeaturedPoint>>
    {
        public async Task<IEnumerable<FeaturedPoint>> Handle(Generic.GetAllCommand<FeaturedPoint> request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request.Reload);
        }

        private readonly Repository _repository = repository;
    }

    internal class CommandGetByDistrictHandler(Repository repository) : IRequestHandler<Shared.Commands.FeaturedPoints.CommandGetByDistrict, IEnumerable<FeaturedPoint>>
    {
        private readonly Repository _repository = repository;

        public async Task<IEnumerable<FeaturedPoint>> Handle(FeaturedPoints.CommandGetByDistrict request, CancellationToken cancellationToken)
        {
            return await _repository.GetByDistrict(request.District);
        }
    }
}
