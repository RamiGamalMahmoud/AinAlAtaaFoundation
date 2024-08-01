using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchesManagement.Editor
{
    internal class ViewModelUpdate : ViewModel
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, Branch branch) : base(mediator, messenger, branch)
        {
        }

        protected override async Task Save()
        {
            if (!(await _mediator.Send(new Shared.Commands.Generic.CommandUpdate<BranchDataModel>(DataModel))))
            {
                _messenger.Send(new Notifications.FailerNotification("خطأ في عملية الحفظ"));
                return;
            }
            _messenger.Send(new Notifications.SuccessNotification("تم الحفظ بنجاح"));
            _messenger.Send(new Messages.EntityUpdated<Branch>(DataModel.Model));
        }
    }
}
