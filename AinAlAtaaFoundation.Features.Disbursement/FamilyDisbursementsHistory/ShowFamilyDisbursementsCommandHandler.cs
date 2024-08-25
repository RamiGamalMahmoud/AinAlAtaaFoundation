using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.FamilyDisbursementsHistory
{
    internal class ShowFamilyDisbursementsCommandHandler(IServiceProvider serviceProvider) : IRequestHandler<Shared.Commands.Disbursements.ShowFamilyDisbursementsHistory>
    {
        public Task Handle(Disbursements.ShowFamilyDisbursementsHistory request, CancellationToken cancellationToken)
        {

            ViewModel viewModel = new ViewModel(
                serviceProvider.GetRequiredService<IMediator>(),
                serviceProvider.GetRequiredService<IMessenger>(),
                request.Family);
            View view = new View(viewModel);
            view.Show();
            return Task.CompletedTask;
        }
    }
}
