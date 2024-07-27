using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement.Create
{
    internal class ShowCommandHandler(IServiceProvider serviceProvider) : IRequestHandler<DistrictRepresentatives.ShowCreate, DistrictRepresentative>
    {
        public Task<DistrictRepresentative> Handle(DistrictRepresentatives.ShowCreate request, CancellationToken cancellationToken)
        {
            ViewModel viewModel = new ViewModel(
                _serviceProvider.GetRequiredService<IMediator>(),
                _serviceProvider.GetRequiredService<IMessenger>());

            new Editor.View(viewModel).ShowDialog();
            return Task.FromResult(new DistrictRepresentative());
        }
        private readonly IServiceProvider _serviceProvider = serviceProvider;
    }
}
