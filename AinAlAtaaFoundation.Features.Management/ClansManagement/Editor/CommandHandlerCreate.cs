using AinAlAtaaFoundation.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.ClansManagement.Editor
{
    internal static class CommandHandlerCreate
    {
        public record Command(ClanDataModel DataModel) : IRequest<Clan>;

        public class Handler(Repository repository) : IRequestHandler<Command, Clan>
        {
            async Task<Clan> IRequestHandler<Command, Clan>.Handle(Command request, CancellationToken cancellationToken)
            {
                return await _repository.Create(request.DataModel);
            }

            private readonly Repository _repository = repository;
        }
    }
}
