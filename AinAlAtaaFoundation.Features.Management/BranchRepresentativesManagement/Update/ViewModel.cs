using AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement.Editor;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement.Update
{
    internal class ViewModel(IMediator mediator, IMessenger messenger, BranchRepresentative branchRepresentative = null) :
        EditorViewModelBase(mediator, messenger, branchRepresentative)
    {
        protected override async Task Save()
        {
            using (DoBusyWorkFactory.CreateBusyWork(DoBusyWorkObject))
            {
                await _mediator.Send(new CommandHandlerUpdate.Command(DataModel));
            }
        }
    }
}
