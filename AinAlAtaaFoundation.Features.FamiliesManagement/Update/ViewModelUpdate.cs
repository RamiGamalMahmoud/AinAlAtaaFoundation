using AinAlAtaaFoundation.Features.FamiliesManagement.Editor;
using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Update
{
    internal class ViewModelUpdate : EditoViewModelBase
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, Family model) : base(mediator, messenger, model)
        {
        }

        protected override async Task Save()
        {
            if(await _mediator.Send(new CommandHandlerUpdate.Command(DataModel)))
            {
                _messenger.Send(new Shared.Notifications.SuccessNotification("تمت عملية التعديل بنجاح"));
            }
            else
            {
                _messenger.Send(new Shared.Notifications.FailerNotification("فشل في عملية التعديل, بيانات مكررة"));
            }
        }
    }
}
