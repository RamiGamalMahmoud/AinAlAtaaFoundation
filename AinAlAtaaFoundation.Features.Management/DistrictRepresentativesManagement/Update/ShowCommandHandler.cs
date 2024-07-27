using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement.Update
{
    internal class ShowCommandHandler(IServiceProvider serviceProvider) : IRequestHandler<DistrictRepresentatives.ShowUpdate, bool>
    {

        public Task<bool> Handle(DistrictRepresentatives.ShowUpdate request, CancellationToken cancellationToken)
        {
            ViewModel viewModel = new ViewModel(
                _serviceProvider.GetRequiredService<IMediator>(),
                _serviceProvider.GetRequiredService<IMessenger>(),
                request.DistrictRepresentative);

            new Editor.View(viewModel).ShowDialog();
            return Task.FromResult(true);
        }
        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }
}
