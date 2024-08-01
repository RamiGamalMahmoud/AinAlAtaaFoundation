using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement.Editor
{
    internal class ViewModelUpdate(IMediator mediator, IMessenger messenger, BranchRepresentative branchRepresentative = null) :
        EditorViewModelBase(mediator, messenger, branchRepresentative)
    {
        protected override async Task Save()
        {
            using (DoBusyWorkFactory.CreateBusyWork(DoBusyWorkObject))
            {
                if (await _mediator.Send(new Shared.Commands.Generic.CommandUpdate<BranchRepresentativeDataModel>(DataModel)))
                {
                    _messenger.Send(new Notifications.SuccessNotification("تم التعديل بنجاح"));
                    _messenger.Send(new Messages.EntityUpdated<BranchRepresentative>(DataModel.Model));
                }
                else
                {
                    _messenger.Send(new Notifications.FailerNotification("فشل في عملية التعديل, بيانات مكررة"));
                }
            }
        }
    }
}
