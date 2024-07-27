using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Update
{
    internal class CommandHandlerShowUpdate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Families.CommandShowUpdate>
    {
        public Task Handle(Families.CommandShowUpdate request, CancellationToken cancellationToken)
        {
            ViewModelUpdate viewModel = new ViewModelUpdate(
                _serviceProvider.GetRequiredService<IMediator>(),
                _serviceProvider.GetRequiredService<IMessenger>(),
                request.Family);
            Editor.View view = new Editor.View(viewModel);
            view.ShowDialog();
            return Task.CompletedTask;
        }

        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }
}
