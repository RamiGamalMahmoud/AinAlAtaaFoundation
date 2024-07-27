using AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement.Editor;
using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement.Update
{
    internal class ViewModel : EditorViewModelBase
    {
        public ViewModel(IMediator mediator, IMessenger messenger, DistrictRepresentative districtRepresentative) : 
            base(mediator, messenger, districtRepresentative)
        {
        }

        public override bool CanSave()
        {
            return base.CanSave() && HasChangesObject.HasChanges;
        }

        protected override async Task Save()
        {
            await _mediator.Send(new Update.CommandHandlerUpdate.Command(DataModel));
        }
    }
}
