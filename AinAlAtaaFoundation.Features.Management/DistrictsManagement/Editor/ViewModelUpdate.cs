using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictsManagement.Editor
{
    internal class ViewModelUpdate : ViewModelEditorBase
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, District district) : base(mediator, messenger, district)
        {
        }

        protected override async Task Save()
        {
            if (!(await _mediator.Send(new Shared.Commands.Generic.CommandUpdate<DistrictDataModel>(DataModel))))
            {
                _messenger.Send(new Notifications.FailerNotification("فشل في الحفظ , بيانات مكررة"));
                return;
            }
            _messenger.Send(new Messages.EntityUpdated<District>(DataModel.Model));
            _messenger.Send(new Notifications.SuccessNotification("تم حقظ التعديل"));
        }
    }
}
