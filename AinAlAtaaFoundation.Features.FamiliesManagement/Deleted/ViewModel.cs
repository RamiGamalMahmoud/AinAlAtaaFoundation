using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Deleted
{
    internal partial class ViewModel(IMediator mediator, IMessenger messenger) : ObservableObject
    {
        public async Task LoadDataAsync()
        {
            Families = await mediator.Send(new Shared.Commands.Generic.GetDeletedCommand<Family>());
        }

        [RelayCommand]
        private async Task RestoreFamily(Family family)
        {
            await mediator.Send(new Shared.Commands.Generic.RestoreDeletedEntity<Family>(family));
            _families.Remove(family);
            messenger.Send(new Shared.Notifications.SuccessNotification("تمت استعادة العائلة"));
            messenger.Send(new Shared.Messages.EntityRestored<Family>());
        }

        public IEnumerable<Family> Families
        {
            get => _families;
            set => SetProperty(ref _families, new ObservableCollection<Family>(value));
        }
        ObservableCollection<Family> _families;
    }
}
