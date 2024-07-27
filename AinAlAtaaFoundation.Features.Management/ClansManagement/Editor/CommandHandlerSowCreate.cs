using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.ClansManagement.Editor
{
    internal class CommandHandlerSowCreate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Clans.CommandShowCreate>
    {
        public Task Handle(Clans.CommandShowCreate request, CancellationToken cancellationToken)
        {
            IMessenger messenger = _serviceProvider.GetRequiredService<IMessenger>();
            EditorViewModelBase viewModel = new EditorViewModelBase(
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
}
