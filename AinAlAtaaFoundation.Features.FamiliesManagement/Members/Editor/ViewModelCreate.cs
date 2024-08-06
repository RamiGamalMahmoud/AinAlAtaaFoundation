using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members.Editor
{
    internal partial class ViewModelCreate : EditorViewModelBase
    {
        public ViewModelCreate(IMediator mediator, IMessenger messenger) : base(mediator, messenger, null)
        {
            Title = "إضافة";
        }

        protected override async Task Save()
        {
            FamilyMember familyMember = await _mediator.Send(new Shared.Commands.Generic.CommandCreate<FamilyMember, FamilyMemberDataModel>(DataModel));
            if(familyMember is not null)
            {
                _messenger.Send(new Shared.Messages.EntityCreated<FamilyMember>(DataModel.Model));
                _messenger.Send(new Shared.Notifications.SuccessNotification("تم الحفظ بنجاح"));
            }
            else
            {
                _messenger.Send(new Shared.Notifications.FailerNotification("فشل في عملية الحفظ"));
            }
        }


    }
}
