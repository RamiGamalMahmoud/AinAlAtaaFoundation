using AinAlAtaaFoundation.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement.Create
{
    internal class CommandHandlerCreate
    {
        public record Command(BranchRepresentativeDataModel DataModel) : IRequest<BranchRepresentative>;

        internal class Handler(Repository repository) : IRequestHandler<Command, BranchRepresentative>
        {
            public async Task<BranchRepresentative> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _repository.Create(request.DataModel);
            }

            private readonly Repository _repository = repository;
        }
    }
}
