using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
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

    internal class RemoveFamilyCommandHandler(Repository repository) : IRequestHandler<Shared.Commands.Generic.RemoveCommand<Family>, bool>
    {
        public async Task<bool> Handle(Generic.RemoveCommand<Family> request, CancellationToken cancellationToken)
        {
            return await repository.Remove(request.Model);
        }
    }

    internal class GetDeletedFamiliesCommandHandler(Repository repository) : IRequestHandler<Shared.Commands.Generic.GetDeletedCommand<Family>, IEnumerable<Family>>
    {
        public async Task<IEnumerable<Family>> Handle(Generic.GetDeletedCommand<Family> request, CancellationToken cancellationToken)
        {
            return await repository.GetAllDeletedAsync();
        }
    }

    internal class RestoreFamily(Repository repository) : IRequestHandler<Shared.Commands.Generic.RestoreDeletedEntity<Family>>
    {
        public async Task Handle(Generic.RestoreDeletedEntity<Family> request, CancellationToken cancellationToken)
        {
            await repository.Restore(request.Model);
        }
    }

    internal class ShowDeletedFamiliesCommandHandler(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowDeletedEntities<Family>>
    {
        public Task Handle(Generic.ShowDeletedEntities<Family> request, CancellationToken cancellationToken)
        {
            Deleted.View view = serviceProvider.GetRequiredService<Deleted.View>();
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}
