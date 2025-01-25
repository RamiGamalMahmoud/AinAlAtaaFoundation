using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.AssociationRepresentatives.Listing
{
    internal partial class ViewModel(IMediator mediator) : ObservableObject
    {
        public async Task LoadDataAsync()
        {
            AssociationRepresentatives = await mediator.Send(new Shared.Commands.Generic.GetAllCommand<AssociationRepresentative>());
        }

        [RelayCommand]
        private async Task ShowCreate()
        {
            await mediator.Send(new Shared.Commands.Generic.ShowCreate<AssociationRepresentative>());
        }

        [RelayCommand]
        private async Task ShowUpdate(AssociationRepresentative associationRepresentative)
        {
            await mediator.Send(new Shared.Commands.Generic.ShowUpdate<AssociationRepresentative>(associationRepresentative));
        }

        [ObservableProperty]
        private IEnumerable<AssociationRepresentative> _associationRepresentatives;
    }
}
