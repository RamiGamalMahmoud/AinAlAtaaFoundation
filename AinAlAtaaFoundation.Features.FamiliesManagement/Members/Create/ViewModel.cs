using AinAlAtaaFoundation.Features.FamiliesManagement.Members.Editor;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members.Create
{
    internal partial class ViewModel : EditorViewModelBase
    {
        public ViewModel(IMediator mediator, IMessenger messenger) : base(mediator, messenger, null)
        {
        }

        protected override async Task Save()
        {
            await _mediator.Send(new CommandHandlerCreate.Command(DataModel));
        }


    }
}
