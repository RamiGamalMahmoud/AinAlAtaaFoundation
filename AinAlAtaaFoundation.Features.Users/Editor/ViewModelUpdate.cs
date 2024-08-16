using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Users.Editor
{
    internal class ViewModelUpdate : ViewModel
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, User user) : base(mediator, messenger, user)
        {
            IsUpdate = true;
        }

        protected async override Task Save()
        {
            try
            {
                if (await _mediator.Send(new Shared.Commands.Generic.CommandUpdate<UserDataModel>(DataModel)))
                {
                    _messenger.Send(new Shared.Notifications.SuccessNotification("تم الحفظ"));
                    _messenger.Send(new Shared.Messages.EntityUpdated<User>(DataModel.Model));
                }
                else _messenger.Send(new Shared.Notifications.FailerNotification("فشل في عملية التعديل , بيانات مكررة"));
            }
            catch (Shared.Exceptions.AdminsCountException)
            {
                _messenger.Send(new Shared.Notifications.FailerNotification("يجب أن يكون هناك مسؤل واحد على الأقل"));
                DataModel.IsAdmin = true;
            }
        }
    }
}
