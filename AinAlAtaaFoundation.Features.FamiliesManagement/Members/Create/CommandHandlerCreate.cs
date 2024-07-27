using AinAlAtaaFoundation.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members.Create
{
    internal static class CommandHandlerCreate
    {
        internal record Command(FamilyMemberDataModel DataModel) : IRequest<FamilyMember>;

        internal class Handler(Repository repository) : IRequestHandler<Command, FamilyMember>
        {
            public async Task<FamilyMember> Handle(Command request, CancellationToken cancellationToken)
            {
                return await _repository.Create(request.DataModel);
            }
            private readonly Repository _repository = repository;
        }

    }
}
