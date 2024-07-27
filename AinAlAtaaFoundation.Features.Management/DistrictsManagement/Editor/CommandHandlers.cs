using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictsManagement.Editor
{
    internal static class CommandHandlers
    {
        // show create
        internal class ShowCreate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Districts.CommandShowCreate>
        {
            public Task Handle(Districts.CommandShowCreate request, CancellationToken cancellationToken)
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

            private readonly IServiceProvider _serviceProvider = serviceProvider;
        }

        // handle create

        internal static class Create
        {
            internal record Command(DistrictDataModel DataModel) : IRequest<District>;
            internal class Handler(Repository repository) : IRequestHandler<Command, District>
            {
                public async Task<District> Handle(Command request, CancellationToken cancellationToken)
                {
                    return await _repository.Create(request.DataModel);
                }

                private readonly Repository _repository = repository;
            }
        }

        // show update
        internal class ShowUpdate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Districts.CommandShowUpdate>
        {
            public Task Handle(Districts.CommandShowUpdate request, CancellationToken cancellationToken)
            {
                IMessenger messenger = _serviceProvider.GetRequiredService<IMessenger>();
                ViewModel viewModel = new ViewModel(
                    _serviceProvider.GetRequiredService<IMediator>(),
                    messenger,
                    request.District
                    );

                View view = new View(viewModel, messenger);
                view.ShowDialog();
                return Task.CompletedTask;
            }
            private readonly IServiceProvider _serviceProvider = serviceProvider;
        }

        // handle update

        internal static class Update
        {
            internal record Command(DistrictDataModel DataModel) : IRequest<bool>;
            internal class Handler(Repository repository) : IRequestHandler<Command, bool>
            {
                public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
                {
                    return await _repository.Update(request.DataModel);
                }

                private readonly Repository _repository = repository;
            }
        }
    }
}
