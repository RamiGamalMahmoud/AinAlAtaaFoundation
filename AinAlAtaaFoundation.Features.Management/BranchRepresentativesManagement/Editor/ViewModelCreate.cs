using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement.Editor
{
    internal class ViewModelCreate(IMediator mediator, IMessenger messenger, BranchRepresentative branchRepresentative = null) :
        EditorViewModelBase(mediator, messenger, branchRepresentative)
    {
        protected override async Task Save()
        {
            BranchRepresentative created = await _mediator.Send(new Shared.Commands.Generic.CommandCreate<BranchRepresentative, BranchRepresentativeDataModel>(DataModel));
            _messenger.Send(new Messages.EntityCreated<BranchRepresentative>(created));
        }
    }
}
