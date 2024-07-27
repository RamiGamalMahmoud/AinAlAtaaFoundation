using AinAlAtaaFoundation.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement.Create
{
    internal static class CommandHandlerCreate
    {
        public record Command(DistrictRepresentativeDataModel DataModel) : IRequest<DistrictRepresentative>;

        internal class Handler(Repository repository) : IRequestHandler<Command, DistrictRepresentative>
        {
            public async Task<DistrictRepresentative> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _repository.Create(request.DataModel);
            }

            private readonly Repository _repository = repository;
        }
    }
}
