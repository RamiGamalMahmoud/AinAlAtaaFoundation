using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.AssociationRepresentatives.Editor
{
    internal class ViewModelCreate : ViewModel
    {
        public ViewModelCreate(IMediator mediator, IMessenger messenger) : base(mediator, messenger, null)
        {
        }

        protected override async Task Save()
        {
            AssociationRepresentative representative = await _mediator.Send(new Generic.CommandCreate<AssociationRepresentative, AssociationRepresentativeDataModel>(DataModel));
            if(representative is null)
            {
                _messenger.Send(new Shared.Notifications.FailerNotification("خطأ"));
            }
            else
            {
                _messenger.Send(new Shared.Messages.EntityCreated<AssociationRepresentative>(DataModel.Model));
                _messenger.Send(new Shared.Notifications.SuccessNotification("تم الحفظ بنجاح"));
                DataModel.Name = "";
            }
        }
    }
}
