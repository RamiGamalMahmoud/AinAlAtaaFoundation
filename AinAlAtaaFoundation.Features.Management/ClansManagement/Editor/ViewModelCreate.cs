using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.ClansManagement.Editor
{
    internal class ViewModelCreate : EditorViewModelBase
    {
        public ViewModelCreate(IMediator mediator, IMessenger messenger, Clan clan) : base(mediator, messenger, clan)
        {
        }

        protected override async Task Save()
        {
            Clan clan = await _mediator.Send(new Shared.Commands.Generic.CommandCreate<Clan, ClanDataModel>(DataModel));

            if (clan is null)
            {
                _messenger.Send(new Shared.Notifications.FailerNotification("خطأ"));
            }
            else
            {
                _messenger.Send(new Shared.Messages.EntityCreated<Clan>(DataModel.Model));
                _messenger.Send(new Shared.Notifications.SuccessNotification("تم الحفظ بنجاح"));
                DataModel.Name = "";
            }
        }
    }
}
