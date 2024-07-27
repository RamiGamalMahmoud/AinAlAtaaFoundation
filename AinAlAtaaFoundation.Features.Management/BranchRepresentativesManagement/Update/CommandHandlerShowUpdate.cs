using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement.Update
{
    internal class CommandHandlerShowUpdate(IServiceProvider serviceProvider) : IRequestHandler<BranchRepresentatives.ShowUpdate, bool>
    {
        public Task<bool> Handle(BranchRepresentatives.ShowUpdate request, CancellationToken cancellationToken)
        {
            ViewModel viewModel = new ViewModel(
                _serviceProvider.GetRequiredService<IMediator>(),
                _serviceProvider.GetRequiredService<IMessenger>(),
                request.BranchRepresentative);

            new Editor.View(viewModel).ShowDialog();

            return Task.FromResult(true);
        }

        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }
}
