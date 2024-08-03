using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement.Editor
{
    internal class ViewModelUpdate : EditorViewModelBase
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, DistrictRepresentative districtRepresentative) :
            base(mediator, messenger, districtRepresentative)
        {
        }

        public override bool CanSave()
        {
            return base.CanSave() && HasChangesObject.HasChanges;
        }

        protected override async Task Save()
        {
            if(await _mediator.Send(new Shared.Commands.Generic.CommandUpdate<DistrictRepresentativeDataModel>(DataModel)))
            {
                _messenger.Send(new Shared.Messages.EntityUpdated<DistrictRepresentative>(DataModel.Model));
                _messenger.Send(new Shared.Notifications.SuccessNotification("تم الحفظ بنجاح"));
            }
            else
            {
                _messenger.Send(new Shared.Notifications.FailerNotification("فشل في عملية التعديل, بيانات مكررة"));
            }
        }
    }
}
