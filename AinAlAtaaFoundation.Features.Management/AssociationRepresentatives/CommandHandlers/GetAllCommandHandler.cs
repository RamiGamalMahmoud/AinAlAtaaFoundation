using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.AssociationRepresentatives.CommandHandlers
{
    internal class GetAllCommandHandler(Repository repository) : IRequestHandler<Generic.GetAllCommand<AssociationRepresentative>, IEnumerable<AssociationRepresentative>>
    {
        public async Task<IEnumerable<AssociationRepresentative>> Handle(Generic.GetAllCommand<AssociationRepresentative> request, CancellationToken cancellationToken)
        {
            return await repository.GetAll(request.Reload);
        }
    }
}
