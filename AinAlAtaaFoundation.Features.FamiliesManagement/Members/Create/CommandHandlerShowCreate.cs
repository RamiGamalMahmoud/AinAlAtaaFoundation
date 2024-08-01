using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members.Create
{
    internal class CommandHandlerShowCreate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowCreate<FamilyMember>>
    {
        public Task Handle(Shared.Commands.Generic.ShowCreate<FamilyMember> request, CancellationToken cancellationToken)
        {
            ViewModel viewModel = new ViewModel(
                _serviceProvider.GetRequiredService<IMediator>(),
                _serviceProvider.GetRequiredService<IMessenger>()
                );

            Editor.View view = new Editor.View(viewModel);
            view.ShowDialog();

            return Task.CompletedTask;
        }

        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }
}
