using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Users.Editor
{
    internal class ViewModelCreate : ViewModel
    {
        public ViewModelCreate(IMediator mediator, IMessenger messenger, User user) : base(mediator, messenger, user)
        {
            ShowPassword = true;

        }

        protected override async Task Save()
        {
            User user = await _mediator.Send(new Shared.Commands.Generic.CommandCreate<User, UserDataModel>(DataModel));
            if (user is not null)
            {
                _messenger.Send(new Shared.Notifications.SuccessNotification("تم الحفظ"));
                _messenger.Send(new Shared.Messages.EntityCreated<User>(user));

                DataModel.ClearInputs();

            }
            else _messenger.Send(new Shared.Notifications.FailerNotification("فشل في عملية التعديل , بيانات مكررة"));
        }
    }
}
