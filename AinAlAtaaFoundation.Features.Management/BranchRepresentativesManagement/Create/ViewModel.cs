using AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement.Editor;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement.Create
{
    internal class ViewModel(IMediator mediator, IMessenger messenger, BranchRepresentative branchRepresentative = null) :
        EditorViewModelBase(mediator, messenger, branchRepresentative)
    {
        protected override async Task Save()
        {
            BranchRepresentative created = await _mediator.Send(new CommandHandlerCreate.Command(DataModel));
            _messenger.Send(new Messages.EntityCreated<BranchRepresentative>(created));
        }
    }
}
