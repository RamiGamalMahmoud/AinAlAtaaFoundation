using AinAlAtaaFoundation.Models;
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
        internal class ShowCreate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowCreate<District>>
        {
            public Task Handle(Shared.Commands.Generic.ShowCreate<District> request, CancellationToken cancellationToken)
            {
                IMessenger messenger = _serviceProvider.GetRequiredService<IMessenger>();
                ViewModelEditorBase viewModel = new ViewModelCreate(
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
            //internal record Command(DistrictDataModel DataModel) : IRequest<District>;
            internal class Handler(Repository repository) : IRequestHandler<Shared.Commands.Generic.CommandCreate<District, DistrictDataModel>, District>
            {
                public async Task<District> Handle(Shared.Commands.Generic.CommandCreate<District, DistrictDataModel> request, CancellationToken cancellationToken)
                {
                    return await _repository.Create(request.DataModel);
                }

                private readonly Repository _repository = repository;
            }
        }

        // show update
        internal class ShowUpdate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowUpdate<District>>
        {
            public Task Handle(Shared.Commands.Generic.ShowUpdate<District> request, CancellationToken cancellationToken)
            {
                IMessenger messenger = _serviceProvider.GetRequiredService<IMessenger>();
                ViewModelEditorBase viewModel = new ViewModelUpdate(
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

        // handle update

        internal static class Update
        {
            //internal record Command(DistrictDataModel DataModel) : IRequest<bool>;
            internal class Handler(Repository repository) : IRequestHandler<Shared.Commands.Generic.CommandUpdate<DistrictDataModel>, bool>
            {
                public async Task<bool> Handle(Shared.Commands.Generic.CommandUpdate<DistrictDataModel> request, CancellationToken cancellationToken)
                {
                    return await _repository.Update(request.DataModel);
                }

                private readonly Repository _repository = repository;
            }
        }
    }
}
