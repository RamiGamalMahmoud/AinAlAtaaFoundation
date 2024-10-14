using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.SocialStatuses.CommandHandlers
{
    internal class CreateCommandHandler(Repository repository) : IRequestHandler<Generic.CommandCreate<SocialStatus, SocialStatusDataModel>, SocialStatus>
    {
        public async Task<SocialStatus> Handle(Generic.CommandCreate<SocialStatus, SocialStatusDataModel> request, CancellationToken cancellationToken)
        {
            return await repository.Create(request.DataModel);
        }
    }
}
