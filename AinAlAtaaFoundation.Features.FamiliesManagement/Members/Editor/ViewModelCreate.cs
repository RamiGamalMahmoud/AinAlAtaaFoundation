using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members.Editor
{
    internal partial class ViewModelCreate : EditorViewModelBase
    {
        public ViewModelCreate(IMediator mediator, IMessenger messenger) : base(mediator, messenger, null)
        {
            Title = "إضافة";
        }

        protected override async Task Save()
        {
            await _mediator.Send(new Shared.Commands.Generic.CommandCreate<FamilyMember, FamilyMemberDataModel>(DataModel));
        }


    }
}
