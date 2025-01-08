using AinAlAtaaFoundation.Features.Management.AssociationRepresentatives.Editor;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.AssociationRepresentatives.CommandHandlers
{
    internal class ShowCreateCommandHandler(IServiceProvider services) : IRequestHandler<Generic.ShowCreate<AssociationRepresentative>>
    {
        public Task Handle(Generic.ShowCreate<AssociationRepresentative> request, CancellationToken cancellationToken)
        {
            ViewModelCreate viewModel = new ViewModelCreate(services.GetRequiredService<IMediator>(), services.GetRequiredService<IMessenger>());
            Editor.View view = new Editor.View(viewModel);
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}
