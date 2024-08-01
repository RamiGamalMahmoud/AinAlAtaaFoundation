using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.FeaturedPointsManagement.Editor
{
    internal class ViewModelUpdate : ViewModelEditorBase
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, FeaturedPoint featuredPoint) : base(mediator, messenger, featuredPoint)
        {
        }

        protected override async Task Save()
        {
            if (!(await _mediator.Send(new CommandHandlers.Update.Command(DataModel))))
            {
                _messenger.Send(new Notifications.FailerNotification("فشل في عملية الحفظ , بيانات مكررة!"));
                return;
            }
            _messenger.Send(new Messages.EntityUpdated<FeaturedPoint>(DataModel.Model));
            _messenger.Send(new Notifications.SuccessNotification("تمت عملية الحفظ بنجاح"));
        }
    }
}
