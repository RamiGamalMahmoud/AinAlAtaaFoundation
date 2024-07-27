using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Create
{
    internal class CommandHandlerShowCreate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Families.CommandShowCreate>
    {
        public Task Handle(Families.CommandShowCreate request, CancellationToken cancellationToken)
        {
            ViewModelCreate viewModel = new ViewModelCreate(
                _serviceProvider.GetRequiredService<IMediator>(),
                _serviceProvider.GetRequiredService<IMessenger>());
            Editor.View view = new Editor.View(viewModel);
            view.ShowDialog();
            return Task.CompletedTask;
        }

        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }
}
