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
    internal class ShowEditCommandHandler(IServiceProvider services) : IRequestHandler<Generic.ShowUpdate<AssociationRepresentative>>
    {
        public Task Handle(Generic.ShowUpdate<AssociationRepresentative> request, CancellationToken cancellationToken)
        {
            ViewModelUpdate viewModel = new ViewModelUpdate(services.GetRequiredService<IMediator>(), services.GetRequiredService<IMessenger>(), request.Model);
            Editor.View view = new Editor.View(viewModel);
            view.ShowDialog();
            return Task.CompletedTask;
        }
    }
}
