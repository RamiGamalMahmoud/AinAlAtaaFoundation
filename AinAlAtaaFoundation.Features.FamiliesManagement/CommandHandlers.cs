using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement
{
    internal class CommandHandlerGet(Repository repository) : IRequestHandler<Shared.Commands.Families.GetFamilyCommand, Family>
    {
        public async Task<Family> Handle(Families.GetFamilyCommand request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Id);
        }

        private readonly Repository _repository = repository;
    }

    internal class CommandHandlerGetByRationCard(Repository repository) : IRequestHandler<Shared.Commands.Families.GetByRationCard, Family>
    {
        public async Task<Family> Handle(Families.GetByRationCard request, CancellationToken cancellationToken)
        {
            return await _repository.GetByRationCard(request.RationCard);
        }
        private readonly Repository _repository = repository;
    }

    internal class CommandHandlerGetByRationCardOwner(Repository repository) : IRequestHandler<Shared.Commands.Families.GetByRationCardOwner, IEnumerable<Family>>
    {
        public async Task<IEnumerable<Family>> Handle(Families.GetByRationCardOwner request, CancellationToken cancellationToken)
        {
            return await _repository.GetByRationCardOwner(request.RationCardOwnerName);
        }
        private readonly Repository _repository = repository;
    }
}
