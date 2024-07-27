using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement.Update
{
    internal static class CommandHandlerUpdate
    {
        public record Command(BranchRepresentativeDataModel DataModel) : IRequest;

        internal class Handler(Repository repository) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                await _repository.Update(request.DataModel);
            }

            private readonly Repository _repository = repository;
        }
    }
}
