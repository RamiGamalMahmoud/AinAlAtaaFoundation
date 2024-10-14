using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.SocialStatuses.Editor
{
    internal class CreateViewModel(IMediator mediator, IMessenger messenger) : ViewModel(mediator, messenger, null)
    {
        protected override async Task Save()
        {
            await _mediator.Send(new Shared.Commands.Generic.CommandCreate<SocialStatus, SocialStatusDataModel>(DataModel));
            _messenger.Send(new Messages.EntityCreated<SocialStatus>(DataModel.Model));
        }
    }
}
