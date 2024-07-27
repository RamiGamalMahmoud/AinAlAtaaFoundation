using AinAlAtaaFoundation.Features.FamiliesManagement.Editor;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Create
{
    internal class ViewModelCreate : EditoViewModelBase
    {
        public ViewModelCreate(IMediator mediator, IMessenger messenger) : base(mediator, messenger, null)
        {
        }

        protected override async Task Save()
        {
            await _mediator.Send(new CommandHandlerCreate.Command(DataModel));
        }
    }
}
