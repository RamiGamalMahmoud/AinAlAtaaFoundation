using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.SocialStatuses.CommandHandlers
{
    internal class GetAllCommandHandler(Repository repository) : IRequestHandler<Generic.GetAllCommand<SocialStatus>, IEnumerable<SocialStatus>>
    {
        public async Task<IEnumerable<SocialStatus>> Handle(Generic.GetAllCommand<SocialStatus> request, CancellationToken cancellationToken)
        {
            return await repository.GetAll(request.Reload);
        }
    }
}
