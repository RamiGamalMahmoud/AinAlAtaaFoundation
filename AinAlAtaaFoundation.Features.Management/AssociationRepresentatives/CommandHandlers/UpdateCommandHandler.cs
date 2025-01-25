using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.AssociationRepresentatives.CommandHandlers
{
    internal class UpdateCommandHandler(Repository repository) : IRequestHandler<Generic.CommandUpdate<AssociationRepresentativeDataModel>, bool>
    {
        public async Task<bool> Handle(Generic.CommandUpdate<AssociationRepresentativeDataModel> request, CancellationToken cancellationToken)
        {
            return await _repository.Update(request.DataModel);
        }

        private readonly Repository _repository = repository;
    }
}
