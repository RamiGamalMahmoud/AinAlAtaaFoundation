using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement.Create
{
    internal class CommandHandlerShowCreate(IServiceProvider serviceProvider) : IRequestHandler<BranchRepresentatives.ShowCreate, BranchRepresentative>
    {
        public Task<BranchRepresentative> Handle(BranchRepresentatives.ShowCreate request, CancellationToken cancellationToken)
        {
            Create.ViewModel viewModel = new ViewModel(_serviceProvider.GetRequiredService<IMediator>(), _serviceProvider.GetRequiredService<IMessenger>());
            new Editor.View(viewModel).ShowDialog();
            return Task.FromResult(new BranchRepresentative());
        }

        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }
}
