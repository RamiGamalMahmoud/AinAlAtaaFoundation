using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictsManagement
{
    internal partial class ViewModel(IMediator mediator, IMessenger messenger) : ObservableObject
    {
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
            _mediator.Send(new Shared.Commands.Generic.ShowCreate<District>());
        }

        [RelayCommand]
        private void ShowUpdate(District district)
        {
            _mediator.Send(new Shared.Commands.Generic.ShowUpdate<District>(district));
        }

        [RelayCommand]
        private async Task Remove(District district)
        {
            bool isRemoved = await _mediator.Send(new Shared.Commands.Generic.RemoveCommand<District>(district));
            if (isRemoved)
            {
                messenger.Send(new Shared.Notifications.SuccessNotification("تم حذف العنصر بنجاح"));
            }
            else messenger.Send(new Shared.Notifications.FailerNotification("هذا العنصر مرتبط ببيانات اخرى و لا يمكن حذفه"));
        }

        public DoBusyWorkObject DoBusyWorkObject { get; } = new DoBusyWorkObject();

        public IEnumerable<District> Districts { get => _districts; private set => SetProperty(ref _districts, value); }

        private IEnumerable<District> _districts;
        private readonly IMediator _mediator = mediator;
    }
}
