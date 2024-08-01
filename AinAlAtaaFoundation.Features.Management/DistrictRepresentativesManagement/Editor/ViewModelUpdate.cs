using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement.Editor
{
    internal class ViewModelUpdate : EditorViewModelBase
    {
        public ViewModelUpdate(IMediator mediator, IMessenger messenger, DistrictRepresentative districtRepresentative) :
            base(mediator, messenger, districtRepresentative)
        {
        }

        public override bool CanSave()
        {
            return base.CanSave() && HasChangesObject.HasChanges;
        }

        protected override async Task Save()
        {
            await _mediator.Send(new Shared.Commands.Generic.CommandUpdate<DistrictRepresentativeDataModel>(DataModel));
        }
    }
}
