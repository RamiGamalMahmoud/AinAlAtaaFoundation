using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.ClansManagement.Editor
{
    internal class ViewModelUpdate : EditorViewModelBase
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, Clan clan) : base(mediator, messenger, clan)
        {
        }

        protected override async Task Save()
        {
            if (await _mediator.Send(new Shared.Commands.Generic.CommandUpdate<ClanDataModel>(DataModel)))
            {
                _messenger.Send(new Shared.Messages.EntityUpdated<Clan>(DataModel.Model));
                _messenger.Send(new Shared.Notifications.SuccessNotification("تم الحفظ بنجاح"));
            }
            else
            {
                _messenger.Send(new Shared.Notifications.FailerNotification("خطأ"));
            }
        }
    }
}
