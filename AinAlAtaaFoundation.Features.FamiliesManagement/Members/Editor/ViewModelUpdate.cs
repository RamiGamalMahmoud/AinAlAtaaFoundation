using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members.Editor
{
    internal class ViewModelUpdate : EditorViewModelBase
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, FamilyMember familyMember) : base(mediator, messenger, familyMember)
        {
            Title = "تعديل";
            Clan = familyMember.Family.Clan;
        }

        public override bool CanSave() => base.CanSave() && HasChangesObject.HasChanges;

        protected override async Task Save()
        {
            await _mediator.Send(new Shared.Commands.Generic.CommandUpdate<FamilyMemberDataModel>(DataModel));
        }
    }
}
