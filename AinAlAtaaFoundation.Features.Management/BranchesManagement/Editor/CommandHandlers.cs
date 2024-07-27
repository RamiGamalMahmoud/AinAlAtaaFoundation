using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchesManagement.Editor
{
    internal static class CommandHandlers
    {
        internal class Show(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Branches.CommandShowCreate>, 
            IRequestHandler<Shared.Commands.Branches.CommandShowUpdate>
        {
            public Task Handle(Branches.CommandShowCreate request, CancellationToken cancellationToken)
            {
                IMessenger messenger = _serviceProvider.GetRequiredService<IMessenger>();

                ViewModel viewModel = new ViewModel(
                    _serviceProvider.GetRequiredService<IMediator>(),
                    messenger,
                    null
                    );

                View view = new View(viewModel, messenger);
                view.ShowDialog();
                return Task.CompletedTask;
            }

            public Task Handle(Branches.CommandShowUpdate request, CancellationToken cancellationToken)
            {
                IMessenger messenger = _serviceProvider.GetRequiredService<IMessenger>();

                ViewModel viewModel = new ViewModel(
                    _serviceProvider.GetRequiredService<IMediator>(),
                    messenger,
                    request.Branch
                    );

                View view = new View(viewModel, messenger);
                view.ShowDialog();
                return Task.CompletedTask;
            }

            private readonly IServiceProvider _serviceProvider = serviceProvider;
        }

        internal record CommandCreate(BranchDataModel DataModel) : IRequest<Branch>;

        internal class HandleCreate(Repository repository) : IRequestHandler<CommandCreate, Branch>
        {
            public async Task<Branch> Handle(CommandCreate request, CancellationToken cancellationToken)
            {
                return await _repository.Create(request.DataModel);
            }

            private readonly Repository _repository = repository;
        }

        internal record CommandUpdate(BranchDataModel DataModel) : IRequest<bool>;
        internal class HandleUpdate(Repository repository) : IRequestHandler<CommandUpdate, bool>
        {
            public async Task<bool> Handle(CommandUpdate request, CancellationToken cancellationToken)
            {
                return await _repository.Update(request.DataModel);
            }
            private readonly Repository _repository = repository;
        }
    }
}
