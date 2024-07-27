using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Listing
{
    internal partial class ViewModel(IMediator mediator) : ObservableObject
    {
        public async Task LoadDataAsync()
        {
            Families = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Family>());
        }

        [RelayCommand]
        private async Task ShowCreate()
        {
            await _mediator.Send(new Shared.Commands.Families.CommandShowCreate());
        }

        [RelayCommand]
        private void ShowUpdate(Family family)
        {
            _mediator.Send(new Shared.Commands.Families.CommandShowUpdate(family));
        }

        [RelayCommand]
        private void ShowPrint(Family family)
        {
            _mediator.Send(new Print.CommandHandlerShowPrint.Command(family));
        }

        public IEnumerable<Family> Families
        {
            get => _families;
            private set => SetProperty(ref _families, value);
        }
        private IEnumerable<Family> _families;

        private readonly IMediator _mediator = mediator;
    }
}
