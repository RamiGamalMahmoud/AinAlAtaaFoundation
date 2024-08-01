using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement.Editor
{
    internal class CommandHandlerShowCreate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowCreate<BranchRepresentative>>
    {
        public Task Handle(Shared.Commands.Generic.ShowCreate<BranchRepresentative> request, CancellationToken cancellationToken)
        {
            IMessenger messenger = _serviceProvider.GetRequiredService<IMessenger>();
            ViewModelCreate viewModel = new ViewModelCreate(
                _serviceProvider.GetRequiredService<IMediator>(),
                messenger);

            new Editor.View(viewModel, messenger).ShowDialog();
            return Task.CompletedTask;
        }

        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }

    internal class CommandHandlerCreate(Repository repository) :
    IRequestHandler<Shared.Commands.Generic.CommandCreate<BranchRepresentative, BranchRepresentativeDataModel>, BranchRepresentative>
    {
        public async Task<BranchRepresentative> Handle(Generic.CommandCreate<BranchRepresentative, BranchRepresentativeDataModel> request, CancellationToken cancellationToken)
        {
            return await _repository.Create(request.DataModel);
        }

        private readonly Repository _repository = repository;
    }

    internal class CommandHandlerShowUpdate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowUpdate<BranchRepresentative>>
    {
        public Task Handle(Shared.Commands.Generic.ShowUpdate<BranchRepresentative> request, CancellationToken cancellationToken)
        {
            IMessenger messenger = _serviceProvider.GetRequiredService<IMessenger>();
            ViewModelUpdate viewModel = new ViewModelUpdate(
                _serviceProvider.GetRequiredService<IMediator>(),
                messenger,
                request.Model);

            new Editor.View(viewModel, messenger).ShowDialog();

            return Task.CompletedTask;
        }

        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }

    internal class CommandHandlerUpdate(Repository repository) : IRequestHandler<Shared.Commands.Generic.CommandUpdate<BranchRepresentativeDataModel>, bool>
    {
        public async Task<bool> Handle(Generic.CommandUpdate<BranchRepresentativeDataModel> request, CancellationToken cancellationToken)
        {
            return await _repository.Update(request.DataModel);

        }

        private readonly Repository _repository = repository;
    }
}
