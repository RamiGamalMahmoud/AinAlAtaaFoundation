using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members.Update
{
    internal static class CommandHandlerUpdate
    {
        internal record Command(FamilyMemberDataModel DataModel) : IRequest<bool>;

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
