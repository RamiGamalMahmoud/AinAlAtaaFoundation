using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement
{
    internal class GetAllCommandHandler(Repository repository) : IRequestHandler<Generic.GetAllCommand<BranchRepresentative>, IEnumerable<BranchRepresentative>>
    {
        public async Task<IEnumerable<BranchRepresentative>> Handle(Generic.GetAllCommand<BranchRepresentative> request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(request.Reload);
        }

        private readonly Repository _repository = repository;
    }

    internal class CommandHandlerGetByClan(Repository repository) : IRequestHandler<BranchRepresentatives.CommandGetByClan, IEnumerable<BranchRepresentative>>
    {
        public async Task<IEnumerable<BranchRepresentative>> Handle(BranchRepresentatives.CommandGetByClan request, CancellationToken cancellationToken)
        {
            return await _repository.GetByClan(request.Clan);
        }

        private readonly Repository _repository = repository;
    }

    internal class CommandHandlerGetByBranch(Repository repository) : IRequestHandler<BranchRepresentatives.CommandGetByBranch, IEnumerable<BranchRepresentative>>
    {
        public async Task<IEnumerable<BranchRepresentative>> Handle(BranchRepresentatives.CommandGetByBranch request, CancellationToken cancellationToken)
        {
            return await _repository.GetByBranch(request.Branch);
        }

        private readonly Repository _repository = repository;
    }
}
