using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement.Editor
{
    internal class ViewModelCreate(IMediator mediator, IMessenger messenger, DistrictRepresentative districtRepresentative = null) :
        EditorViewModelBase(mediator, messenger, districtRepresentative)
    {
        protected override async Task Save()
        {
            DistrictRepresentative districtRepresentative1 = await _mediator.Send(new Shared.Commands.Generic.CommandCreate<DistrictRepresentative, DistrictRepresentativeDataModel>(DataModel));

            if(districtRepresentative1 is null)
            {
                _messenger.Send(new Notifications.FailerNotification("فشل في الحفظ , بيانات مكررة"));
                return;
            }

            _messenger.Send(new Messages.EntityCreated<DistrictRepresentative>(DataModel.Model));
            _messenger.Send(new Notifications.SuccessNotification("تمت الإضافة"));
        }
    }
}
