using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Editor
{
    internal class CommandHandlerShowCreate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowCreate<Family>>
    {
        public Task Handle(Shared.Commands.Generic.ShowCreate<Family> request, CancellationToken cancellationToken)
        {
            EditoViewModelBase viewModel = new ViewModelCreate(
                _serviceProvider.GetRequiredService<IMediator>(),
                _serviceProvider.GetRequiredService<IMessenger>());
            Editor.View view = new Editor.View(viewModel);
            view.ShowDialog();
            return Task.CompletedTask;
        }

        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }

    internal class CommandHandlerCreate(Repository repository) : IRequestHandler<Shared.Commands.Generic.CommandCreate<Family, FamilyDataModel>, Family>
    {
        public async Task<Family> Handle(Shared.Commands.Generic.CommandCreate<Family, FamilyDataModel> request, CancellationToken cancellationToken)
        {
            return await _repository.Create(request.DataModel);
        }

        private readonly Repository _repository = repository;
    }

    internal class CommandHandlerShowUpdate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowUpdate<Family>>
    {
        public Task Handle(Shared.Commands.Generic.ShowUpdate<Family> request, CancellationToken cancellationToken)
        {
            EditoViewModelBase viewModel = new ViewModelUpdate(
                _serviceProvider.GetRequiredService<IMediator>(),
                _serviceProvider.GetRequiredService<IMessenger>(),
                request.Model);
            Editor.View view = new Editor.View(viewModel);
            view.ShowDialog();
            return Task.CompletedTask;
        }

        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }

    internal class CommandHandlerUpdate(Repository repository) : IRequestHandler<Shared.Commands.Generic.CommandUpdate<FamilyDataModel>, bool>
    {
        public async Task<bool> Handle(Shared.Commands.Generic.CommandUpdate<FamilyDataModel> request, CancellationToken cancellationToken)
        {
            return await _repository.Update(request.DataModel);
        }

        private readonly Repository _repository = repository;
    }
}
