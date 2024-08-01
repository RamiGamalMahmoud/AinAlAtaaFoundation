using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchesManagement.Editor
{
    internal class ViewModelCreate : ViewModel
    {
        public ViewModelCreate(IMediator mediator, IMessenger messenger, Branch branch) : base(mediator, messenger, branch)
        {
        }

        protected override async Task Save()
        {
            Branch branch = await _mediator.Send(new Shared.Commands.Generic.CommandCreate<Branch, BranchDataModel>(DataModel));
            if (branch is null)
            {
                _messenger.Send(new Notifications.FailerNotification("خطأ في عملية الحفظ"));
                return;
            }
            _messenger.Send(new Messages.EntityCreated<Branch>(branch));
            _messenger.Send(new Notifications.SuccessNotification("تم الحفظ بنجاح"));
        }
    }
}
