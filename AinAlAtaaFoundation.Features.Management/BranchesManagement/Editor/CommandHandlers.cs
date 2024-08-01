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
        internal class Show(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowCreate<Branch>>, 
            IRequestHandler<Shared.Commands.Generic.ShowUpdate<Branch>>
        {
            public Task Handle(Shared.Commands.Generic.ShowCreate<Branch> request, CancellationToken cancellationToken)
            {
                IMessenger messenger = _serviceProvider.GetRequiredService<IMessenger>();

                ViewModel viewModel = new ViewModelCreate(
                    _serviceProvider.GetRequiredService<IMediator>(),
                    messenger,
                    null
                    );

                View view = new View(viewModel, messenger);
                view.ShowDialog();
                return Task.CompletedTask;
            }

            public Task Handle(Shared.Commands.Generic.ShowUpdate<Branch> request, CancellationToken cancellationToken)
            {
                IMessenger messenger = _serviceProvider.GetRequiredService<IMessenger>();

                ViewModel viewModel = new ViewModelUpdate(
                    _serviceProvider.GetRequiredService<IMediator>(),
                    messenger,
                    request.Model
                    );

                View view = new View(viewModel, messenger);
                view.ShowDialog();
                return Task.CompletedTask;
            }

            private readonly IServiceProvider _serviceProvider = serviceProvider;
        }

        internal class HandleCreate(Repository repository) :
            IRequestHandler<Shared.Commands.Generic.CommandCreate<Branch, BranchDataModel>, Branch>
        {
            public async Task<Branch> Handle(Generic.CommandCreate<Branch, BranchDataModel> request, CancellationToken cancellationToken)
            {
                return await _repository.Create(request.DataModel);
            }

            private readonly Repository _repository = repository;
        }

        internal class HandleUpdate(Repository repository) : IRequestHandler<Shared.Commands.Generic.CommandUpdate<BranchDataModel>, bool>
        {
            public async Task<bool> Handle(Generic.CommandUpdate<BranchDataModel> request, CancellationToken cancellationToken)
            {
                return await _repository.Update(request.DataModel);
            }

            private readonly Repository _repository = repository;
        }
    }
}
