using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement
{
    internal partial class ViewModel : ObservableObject
    {
        public ViewModel(IMediator mediator, IMessenger messenger)
        {
            _mediator = mediator;
            messenger.Register<Messages.EntityCreated<DistrictRepresentative>>(this, async (r, m) =>
            {
                DistrictRepresentatives = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<DistrictRepresentative>(true));
            });
        }

        public async Task LoadDataAsync()
        {
            DistrictRepresentatives = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<DistrictRepresentative>());
        }

        public IEnumerable<DistrictRepresentative> DistrictRepresentatives
        {
            get => _districtRepresentatives; set => SetProperty(ref _districtRepresentatives, value);
        }
        private IEnumerable<DistrictRepresentative> _districtRepresentatives;

        [RelayCommand]
        private async Task ShowCreate()
        {
            await _mediator.Send(new Shared.Commands.Generic.ShowCreate<DistrictRepresentative>());
        }

        [RelayCommand]
        private async Task ShowUpdate(DistrictRepresentative districtRepresentative)
        {
            await _mediator.Send(new Shared.Commands.Generic.ShowUpdate<DistrictRepresentative>(districtRepresentative));
        }

        private readonly IMediator _mediator;
        private readonly IMessenger _messenger;
    }
}
