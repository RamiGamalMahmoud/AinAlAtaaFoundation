using AinAlAtaaFoundation.Features.FamiliesManagement.Editor;
using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Create
{
    internal class ViewModelCreate : EditoViewModelBase
    {
        public ViewModelCreate(IMediator mediator, IMessenger messenger) : base(mediator, messenger, null)
        {
        }

        protected override async Task Save()
        {
            Family family = await _mediator.Send(new CommandHandlerCreate.Command(DataModel));
            if (family is not null)
            {
                _messenger.Send(new Shared.Notifications.SuccessNotification("تمت الاضافة"));
            }

            else
            {
                _messenger.Send(new Shared.Notifications.FailerNotification("فشل في عملية الاضافة, بيانات مكررة"));
            }
        }
    }
}
