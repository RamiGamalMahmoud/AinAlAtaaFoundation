using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictsManagement
{
    internal class RemoveCommandHandler(Repository repository) : IRequestHandler<Shared.Commands.Generic.RemoveCommand<District>, bool>
    {
        public async Task<bool> Handle(Generic.RemoveCommand<District> request, CancellationToken cancellationToken)
        {
            return await repository.Remove(request.Model);
        }
    }
}
