using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.FeaturedPointsManagement.Editor
{
    internal static class CommandHandlers
    {
        // show create
        internal class ShowCreate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowCreate<FeaturedPoint>>
        {
            public Task Handle(Shared.Commands.Generic.ShowCreate<FeaturedPoint> request, CancellationToken cancellationToken)
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
            internal record Command(FeaturedPointDataModel DataModel) : IRequest<FeaturedPoint>;
            internal class Handler(Repository repository) : IRequestHandler<Command, FeaturedPoint>
            {
                public async Task<FeaturedPoint> Handle(Command request, CancellationToken cancellationToken)
                {
                    return await _repository.Create(request.DataModel);
                }

                private readonly Repository _repository = repository;
            }
        }

        // show update
        internal class ShowUpdate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowUpdate<FeaturedPoint>>
        {
            public Task Handle(Shared.Commands.Generic.ShowUpdate<FeaturedPoint> request, CancellationToken cancellationToken)
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
            internal record Command(FeaturedPointDataModel DataModel) : IRequest<bool>;
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
