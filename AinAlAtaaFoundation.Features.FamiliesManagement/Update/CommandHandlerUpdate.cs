using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Update
{
    internal static class CommandHandlerUpdate
    {
        public record Command(FamilyDataModel DataModel) : IRequest<bool>;

        internal class Handler(Repository repository) : IRequestHandler<Command, bool>
        {
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _repository.Update(request.DataModel);
            }

            private readonly Repository _repository = repository;
        }
    }
}
