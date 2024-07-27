using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members.Update
{
    internal class CommandHandlerShowUpdate(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.FamilyMembers.CommandShowUpdate>
    {
        public Task Handle(FamilyMembers.CommandShowUpdate request, CancellationToken cancellationToken)
        {
            ViewModelUpdate viewModel = new ViewModelUpdate(
                _serviceProvider.GetRequiredService<IMediator>(),
                _serviceProvider.GetRequiredService<IMessenger>(),
                request.FamilyMember);

            Editor.View view = new Editor.View(viewModel);
            view.ShowDialog();

            return Task.CompletedTask;
        }
    
        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }
}
