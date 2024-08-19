using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchesManagement
{
    internal class GetAllCommandHandler(Repository repository) : IRequestHandler<Generic.GetAllCommand<Branch>, IEnumerable<Branch>>
    {
        private readonly Repository _repository = repository;

        public async Task<IEnumerable<Branch>> Handle(Generic.GetAllCommand<Branch> request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request.Reload);
        }
    }

    internal class CommandHandlerGetByClan(Repository repository) : IRequestHandler<Shared.Commands.Branches.CommandGetByClan, IEnumerable<Branch>>
    {
        public async Task<IEnumerable<Branch>> Handle(Branches.CommandGetByClan request, CancellationToken cancellationToken)
        {
            return await _repository.GetByClan(request.Clan);
        }

        private readonly Repository _repository = repository;
    }
}
