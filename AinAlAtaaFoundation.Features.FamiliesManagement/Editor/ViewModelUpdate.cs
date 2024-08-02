using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Editor
{
    internal class ViewModelUpdate : EditoViewModelBase
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, Family model) : base(mediator, messenger, model)
        {
        }

        protected override async Task Save()
        {
            if (await _mediator.Send(new Shared.Commands.Generic.CommandUpdate<FamilyDataModel>(DataModel)))
            {
                _messenger.Send(new Shared.Notifications.SuccessNotification("تمت عملية التعديل بنجاح"));
                _messenger.Send(new Shared.Messages.EntityUpdated<Family>(DataModel.Model));
            }
            else
            {
                _messenger.Send(new Shared.Notifications.FailerNotification("فشل في عملية التعديل, بيانات مكررة"));
            }
        }
    }
}
