using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.History
{
    internal partial class ViewModel(IMediator mediator, IMessenger messenger) : ObservableObject
    {
        public async Task LoadDataAsync()
        {
            Clans = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Clan>());
            FamilyTypes = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<FamilyType>());
            SocialStatuses = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<SocialStatus>());

            Districts = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<District>());
        }

        private readonly IMediator _mediator = mediator;
        private readonly IMessenger _messenger = messenger;
    }
}
