using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.AssociationRepresentatives.Editor
{
    internal class ViewModelUpdate : ViewModel
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, AssociationRepresentative model) : base(mediator, messenger, model)
        {
        }

        protected override async Task Save()
        {
            if(await _mediator.Send(new Generic.CommandUpdate<AssociationRepresentativeDataModel>(DataModel)))
            {
                _messenger.Send(new Shared.Messages.EntityUpdated<AssociationRepresentative>(DataModel.Model));
                _messenger.Send(new Shared.Notifications.SuccessNotification("تم الحفظ بنجاح"));
            }
            else
            {
                _messenger.Send(new Shared.Notifications.FailerNotification("خطاء"));
            }
        }
    }
}
