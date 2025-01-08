using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.AssociationRepresentatives.CommandHandlers
{
    internal class CreateCommandHandler(Repository repository) : IRequestHandler<Generic.CommandCreate<AssociationRepresentative, AssociationRepresentativeDataModel>, AssociationRepresentative>
    {
        public async Task<AssociationRepresentative> Handle(Generic.CommandCreate<AssociationRepresentative, AssociationRepresentativeDataModel> request, CancellationToken cancellationToken)
        {
            return await repository.Create(request.DataModel);
        }
    }
}
