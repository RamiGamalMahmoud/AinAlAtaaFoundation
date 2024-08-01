using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictsManagement.Editor
{
    internal class ViewModelCreate : ViewModelEditorBase
    {
        public ViewModelCreate(IMediator mediator, IMessenger messenger, District district) : base(mediator, messenger, district)
        {
        }

        protected override async Task Save()
        {
            District district = await _mediator.Send(new Shared.Commands.Generic.CommandCreate<District, DistrictDataModel>(DataModel));
            if (district is null)
            {
                _messenger.Send(new Notifications.FailerNotification("فشل في الحفظ , بيانات مكررة"));
                return;
            }

            _messenger.Send(new Messages.EntityCreated<District>(district));
            _messenger.Send(new Notifications.SuccessNotification("تمت الإضافة"));
        }
    }
}
