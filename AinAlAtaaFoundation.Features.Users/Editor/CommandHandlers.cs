using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Users.Editor
{
    internal class CommandHandlerShowCreate(IServiceProvider serviceProvider, Repository repository) :
        IRequestHandler<Shared.Commands.Generic.ShowCreate<User>>,
        IRequestHandler<Shared.Commands.Generic.ShowUpdate<User>>,
        IRequestHandler<Shared.Commands.Generic.CommandCreate<User, UserDataModel>, User>,
        IRequestHandler<Shared.Commands.Generic.CommandUpdate<UserDataModel>, bool>
    {
        public Task Handle(Generic.ShowCreate<User> request, CancellationToken cancellationToken)
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

        public Task Handle(Generic.ShowUpdate<User> request, CancellationToken cancellationToken)
        {
            IMessenger messenger = _serviceProvider.GetRequiredService<IMessenger>();
            ViewModel viewModel = new ViewModel(
                _serviceProvider.GetRequiredService<IMediator>(),
                messenger,
                request.Model
                );

            View view = new View(viewModel, messenger);
            view.ShowDialog();
            return Task.CompletedTask;
        }

        public async Task<User> Handle(Generic.CommandCreate<User, UserDataModel> request, CancellationToken cancellationToken)
        {
            return await _repository.Create(request.DataModel);
        }

        public async Task<bool> Handle(Generic.CommandUpdate<UserDataModel> request, CancellationToken cancellationToken)
        {
            return await _repository.Update(request.DamaModel);
        }

        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly Repository _repository = repository;
    }
}
