using AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement.Editor;
using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement.Create
{
    internal class ViewModel(IMediator mediator, IMessenger messenger, DistrictRepresentative districtRepresentative = null) :
        EditorViewModelBase(mediator, messenger, districtRepresentative)
    {
        protected override async Task Save()
        {
            await _mediator.Send(new CommandHandlerCreate.Command(DataModel));

            DataModel.Name = "";
            DataModel.District = null;
            DataModel.Street = "";
            DataModel.Phones.Clear();

            _messenger.Send(new Messages.EntityCreated<DistrictRepresentative>(DataModel.Model));
        }
    }
}
