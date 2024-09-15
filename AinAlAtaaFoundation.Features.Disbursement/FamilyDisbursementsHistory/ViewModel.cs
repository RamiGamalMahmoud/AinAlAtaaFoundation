using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.FamilyDisbursementsHistory
{
    internal partial class ViewModel(IMediator mediator, IMessenger messenger, Family family) : ObservableObject
    {
        private static int _itesmCount = 50;

        public async Task LoadDataAsync()
        {
            _allDisbursements = new ObservableCollection<Disbursement>(await _mediator.Send(new Shared.Commands.Disbursements.GetByFamilyId(Family.Id)));

            Pages = (int)Math.Ceiling(_allDisbursements.Count / (double)_itesmCount);
            OnPropertyChanged(nameof(Pages));
            Disbursements = new ObservableCollection<Disbursement>( _allDisbursements.Skip(0).Take(_itesmCount));
        }

        [RelayCommand]
        private Task NextPage(int index)
        {
            Disbursements = new ObservableCollection<Disbursement>( _allDisbursements.Skip(index * _itesmCount).Take(_itesmCount));
            return Task.CompletedTask;
        }

        [RelayCommand]
        private async Task Remove(Disbursement disbursement)
        {
            await _mediator.Send(new Shared.Commands.Disbursements.CommandRemove(disbursement));
            Disbursements.Remove(disbursement);
            messenger.Send(new Shared.Notifications.SuccessNotification("تم الحذف بنجاح"));
        }

        public int Pages { get; set; }

        public Family Family { get; private set; } = family;

        public ObservableCollection<Disbursement> Disbursements
        {
            get => _disbursements;
            private set => SetProperty(ref _disbursements, value);
        }
        private ObservableCollection<Disbursement> _disbursements;
        private ObservableCollection<Disbursement> _allDisbursements;
        private readonly IMediator _mediator = mediator;
    }
}
