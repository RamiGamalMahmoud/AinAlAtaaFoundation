using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.FeaturedPointsManagement.Editor
{
    internal class ViewModelCreate : ViewModelEditorBase
    {
        public ViewModelCreate(IMediator mediator, IMessenger messenger, FeaturedPoint featuredPoint) : base(mediator, messenger, featuredPoint)
        {
        }

        protected override async Task Save()
        {
            FeaturedPoint featuredPoint = await _mediator.Send(new CommandHandlers.Create.Command(DataModel));
            if (featuredPoint is null)
            {
                _messenger.Send(new Notifications.FailerNotification("فشل في عملية الحفظ , بيانات مكررة!"));
                return;
            }
            else
            {
                _messenger.Send(new Messages.EntityCreated<FeaturedPoint>(featuredPoint));
                _messenger.Send(new Notifications.SuccessNotification("تمت عملية الحفظ بنجاح"));
            }
        }
    }
}
