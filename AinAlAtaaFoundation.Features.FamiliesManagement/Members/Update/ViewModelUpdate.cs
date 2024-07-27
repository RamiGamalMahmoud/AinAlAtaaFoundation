using AinAlAtaaFoundation.Features.FamiliesManagement.Members.Editor;
using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members.Update
{
    internal class ViewModelUpdate(IMediator mediator, IMessenger messenger, FamilyMember familyMember) : 
        EditorViewModelBase(mediator, messenger, familyMember)
    {
        public override bool CanSave() => base.CanSave() && HasChangesObject.HasChanges;

        protected override async Task Save()
        {
            await _mediator.Send(new CommandHandlerUpdate.Command(DataModel));
        }
    }
}
