using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictsManagement
{
    internal partial class ViewModel : ObservableObject
    {
        public ViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [RelayCommand]
        public async Task LoadDataAsync()
        {
            using (DoBusyWorkFactory.CreateBusyWork(DoBusyWorkObject))
            {
                Districts = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<District>());
            }
        }

        [RelayCommand]
        private void ShowCreate()
        {
            _mediator.Send(new Shared.Commands.Districts.CommandShowCreate());
        }

        [RelayCommand]
        private void ShowUpdate(District district)
        {
            _mediator.Send(new Shared.Commands.Districts.CommandShowUpdate(district));
        }

        public DoBusyWorkObject DoBusyWorkObject { get; } = new DoBusyWorkObject();

        public IEnumerable<District> Districts { get => _districts; private set => SetProperty(ref _districts, value); }

        private IEnumerable<District> _districts;
        private readonly IMediator _mediator;
    }
}
