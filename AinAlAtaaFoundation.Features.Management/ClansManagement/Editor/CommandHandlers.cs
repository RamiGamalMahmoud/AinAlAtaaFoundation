using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.ClansManagement.Editor
{
    internal class CommandHandlerSowCreate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowCreate<Clan>>
    {
        public Task Handle(Shared.Commands.Generic.ShowCreate<Clan> request, CancellationToken cancellationToken)
        {
            IMessenger messenger = _serviceProvider.GetRequiredService<IMessenger>();
            EditorViewModelBase viewModel = new ViewModelCreate(
                _serviceProvider.GetRequiredService<IMediator>(),
                messenger,
                null
                );

            View view = new View(viewModel, messenger);
            view.ShowDialog();
            return Task.CompletedTask;
        }

        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }

    internal class CommandHandlerCreate(Repository repository) : IRequestHandler<Shared.Commands.Generic.CommandCreate<Clan, ClanDataModel>, Clan>
    {
        async Task<Clan> IRequestHandler<Shared.Commands.Generic.CommandCreate<Clan, ClanDataModel>, Clan>.Handle(Shared.Commands.Generic.CommandCreate<Clan, ClanDataModel> request, CancellationToken cancellationToken)
        {
            return await _repository.Create(request.DataModel);
        }

        private readonly Repository _repository = repository;
    }


    internal class CommandHandlerShowUpdate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowUpdate<Clan>>
    {
        public Task Handle(Shared.Commands.Generic.ShowUpdate<Clan> request, CancellationToken cancellationToken)
        {
            IMessenger messenger = _serviceProvider.GetRequiredService<IMessenger>();
            EditorViewModelBase viewModel = new ViewModelUpdate(
                _serviceProvider.GetRequiredService<IMediator>(),
                messenger,
                request.Model
                );

            View view = new View(viewModel, messenger);
            view.ShowDialog();
            return Task.CompletedTask;
        }
        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }

    internal static class CommandHandlerUpdate
    {
        public class Handler(Repository repository) : IRequestHandler<Shared.Commands.Generic.CommandUpdate<ClanDataModel>, bool>
        {
            async Task<bool> IRequestHandler<Shared.Commands.Generic.CommandUpdate<ClanDataModel>, bool>.Handle(Shared.Commands.Generic.CommandUpdate<ClanDataModel> request, CancellationToken cancellationToken)
            {
                return await _repository.Update(request.DataModel);
            }

            private readonly Repository _repository = repository;
        }
    }
}
