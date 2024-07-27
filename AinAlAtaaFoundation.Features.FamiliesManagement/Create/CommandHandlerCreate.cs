using AinAlAtaaFoundation.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Create
{
    internal static class CommandHandlerCreate 
    {
        internal record Command(FamilyDataModel DataModel) : IRequest<Family>;

        internal class Handler(Repository repository) : IRequestHandler<Command, Family>
        {
            public async Task<Family> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _repository.Create(request.DataModel);
            }

            private readonly Repository _repository = repository;
        }
    }
}
