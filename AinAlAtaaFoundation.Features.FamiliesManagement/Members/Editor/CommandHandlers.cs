using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members.Editor
{
    internal class CommandHandlerShowCreate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowCreate<FamilyMember>>
    {
        public Task Handle(Shared.Commands.Generic.ShowCreate<FamilyMember> request, CancellationToken cancellationToken)
        {
            ViewModelCreate viewModel = new ViewModelCreate(
                _serviceProvider.GetRequiredService<IMediator>(),
                _serviceProvider.GetRequiredService<IMessenger>()
                );

            View view = new View(viewModel);
            view.ShowDialog();

            return Task.CompletedTask;
        }

        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }

    internal class CommandHandlerCreate(Repository repository) : IRequestHandler<Shared.Commands.Generic.CommandCreate<FamilyMember, FamilyMemberDataModel>, FamilyMember>
    {
        public async Task<FamilyMember> Handle(Shared.Commands.Generic.CommandCreate<FamilyMember, FamilyMemberDataModel> request, CancellationToken cancellationToken)
        {
            return await _repository.Create(request.DataModel);
        }
        private readonly Repository _repository = repository;

    }

    internal class CommandHandlerShowUpdate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Generic.ShowUpdate<FamilyMember>>
    {
        public Task Handle(Shared.Commands.Generic.ShowUpdate<FamilyMember> request, CancellationToken cancellationToken)
        {
            ViewModelUpdate viewModel = new ViewModelUpdate(
                _serviceProvider.GetRequiredService<IMediator>(),
                _serviceProvider.GetRequiredService<IMessenger>(),
                request.Model);

            View view = new View(viewModel);
            view.ShowDialog();

            return Task.CompletedTask;
        }

        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }

    internal class CommandHandlerUpdate(Repository repository) : IRequestHandler<Shared.Commands.Generic.CommandUpdate<FamilyMemberDataModel>, bool>
    {
        public async Task<bool> Handle(Shared.Commands.Generic.CommandUpdate<FamilyMemberDataModel> request, CancellationToken cancellationToken)
        {
            return await _repository.Update(request.DataModel);
        }

        private readonly Repository _repository = repository;
    }

}
