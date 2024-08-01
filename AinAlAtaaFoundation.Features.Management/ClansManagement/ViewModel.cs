using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.ClansManagement
{
    internal partial class ViewModel(IMediator mediator) : ObservableObject
    {
        public async Task LoadDataAsync()
        {
            Clans = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Clan>());
        }

        [RelayCommand]
        private void ShowCreate()
        {
            _mediator.Send(new Shared.Commands.Generic.ShowCreate<Clan>());
        }

        [RelayCommand]
        private void ShowUpdate(Clan clan)
        {
            _mediator.Send(new Shared.Commands.Generic.ShowUpdate<Clan>(clan));
        }

        public IEnumerable<Clan> Clans { get => _clans; private set => SetProperty(ref _clans, value); }
        private IEnumerable<Clan> _clans;
        private readonly IMediator _mediator = mediator;
    }
}
