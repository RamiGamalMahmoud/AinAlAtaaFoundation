using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.ClansManagement.Editor
{
    internal static class CommandHandlerUpdate
    {
        public record Command(ClanDataModel DataModel) : IRequest<bool>;

        public class Handler(Repository repository) : IRequestHandler<Command, bool>
        {
            async Task<bool> IRequestHandler<Command, bool>.Handle(Command request, CancellationToken cancellationToken)
            {
                return await _repository.Update(request.DataModel);
            }
        
            private readonly Repository _repository = repository;
        }
    }
}
