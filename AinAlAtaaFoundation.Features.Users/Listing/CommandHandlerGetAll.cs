using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Users.Listing
{
    internal class CommandHandlerGetAll(Repository repository) : IRequestHandler<Shared.Commands.Generic.GetAllCommand<User>, IEnumerable<User>>
    {
        async Task<IEnumerable<User>> IRequestHandler<Generic.GetAllCommand<User>, IEnumerable<User>>.Handle(Generic.GetAllCommand<User> request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request.Reload);
        }

        private readonly Repository _repository = repository;
    }
}
