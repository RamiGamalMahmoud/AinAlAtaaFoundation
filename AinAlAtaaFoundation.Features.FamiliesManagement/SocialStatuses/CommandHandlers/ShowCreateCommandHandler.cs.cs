using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.SocialStatuses.CommandHandlers
{
    internal class ShowCreateCommandHandler(IServiceProvider services) : IRequestHandler<Shared.Commands.Generic.ShowCreate<SocialStatus>>
    {
        public Task Handle(Generic.ShowCreate<SocialStatus> request, CancellationToken cancellationToken)
        {
            IMediator mediator = services.GetRequiredService<IMediator>();
            IMessenger messenger = services.GetRequiredService<IMessenger>();

            Editor.ViewModel viewModel = new Editor.CreateViewModel(mediator, messenger);
            Editor.View view = new Editor.View(viewModel, messenger);
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}
