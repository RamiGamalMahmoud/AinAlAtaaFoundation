using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement.Editor
{
    internal class ShowCommandHandler(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowCreate<DistrictRepresentative>>
    {
        public Task Handle(Shared.Commands.Generic.ShowCreate<DistrictRepresentative> request, CancellationToken cancellationToken)
        {
            IMessenger messenger = _serviceProvider.GetRequiredService<IMessenger>();
            ViewModelCreate viewModel = new ViewModelCreate(
                _serviceProvider.GetRequiredService<IMediator>(),
                messenger);

            new View(viewModel, messenger).ShowDialog();
            return Task.CompletedTask;
        }

        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }

    internal class CommandHandlerCreate(Repository repository) : IRequestHandler<Shared.Commands.Generic.CommandCreate<DistrictRepresentative, DistrictRepresentativeDataModel>, DistrictRepresentative>
    {
        public async Task<DistrictRepresentative> Handle(Shared.Commands.Generic.CommandCreate<DistrictRepresentative, DistrictRepresentativeDataModel> request, CancellationToken cancellationToken)
        {
            return await _repository.Create(request.DataModel);
        }

        private readonly Repository _repository = repository;
    }

    internal class CommandHandlerShowUpdate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowUpdate<DistrictRepresentative>>
    {
        public Task Handle(Shared.Commands.Generic.ShowUpdate<DistrictRepresentative> request, CancellationToken cancellationToken)
        {
            IMessenger messenger = _serviceProvider.GetRequiredService<IMessenger>();
            ViewModelUpdate viewModel = new ViewModelUpdate(
                _serviceProvider.GetRequiredService<IMediator>(),
                messenger,
                request.Model);

            new View(viewModel, messenger).ShowDialog();
            return Task.CompletedTask;
        }
        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }

    internal class CommandHandlerUpdate(Repository repository) : IRequestHandler<Shared.Commands.Generic.CommandUpdate<DistrictRepresentativeDataModel>, bool>
    {
        public async Task<bool> Handle(Shared.Commands.Generic.CommandUpdate<DistrictRepresentativeDataModel> request, CancellationToken cancellationToken)
        {
            return await _repository.Update(request.DataModel);
        }

        private readonly Repository _repository = repository;
    }
}
