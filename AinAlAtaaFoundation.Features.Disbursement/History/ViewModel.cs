using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using AinAlAtaaFoundation.Shared.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.History
{
    internal partial class ViewModel(IMediator mediator, IMessenger messenger, TopFilterViewModel topFilterViewModel) : ObservableObject
    {
        public TopFilterViewModel TopFilterViewModel { get; } = topFilterViewModel;
        public async Task LoadDataAsync()
        {
            TopFilterViewModel.Clans = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Clan>());
            TopFilterViewModel.FamilyTypes = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<FamilyType>());
            TopFilterViewModel.SocialStatuses = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<SocialStatus>());

            TopFilterViewModel.Districts = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<District>());

            SearchDate = DateTime.Now;
        }

        [RelayCommand]
        private async Task Filter()
        {
            IEnumerable<Disbursement> disbursements = await _mediator.Send(new Shared.Commands.Disbursements.GetHistory(
                TopFilterViewModel.Clan,
                TopFilterViewModel.Branch,
                TopFilterViewModel.BranchRepresentative,
                TopFilterViewModel.District,
                TopFilterViewModel.DistrictRepresentative,
                TopFilterViewModel.FamilyType,
                TopFilterViewModel.SocialStatus,
                TopFilterViewModel.OrphanType));
            Disbursements = new System.Collections.ObjectModel.ObservableCollection<Disbursement>(disbursements);
        }

        [RelayCommand]
        private async Task Remove(Disbursement disbursement)
        {
            await _mediator.Send(new Shared.Commands.Disbursements.CommandRemove(disbursement));
            Disbursements.Remove(disbursement);
            _messenger.Send(new Shared.Notifications.SuccessNotification("تم الحذف بنجاح"));
        }

        [RelayCommand]
        private void ChangeDate(int value)
        {
            SearchDate = SearchDate.AddDays(value);
        }

        [RelayCommand]
        private void Refresh() => SearchDate = DateTime.Now;

        async partial void OnSearchDateChanged(DateTime oldValue, DateTime newValue)
        {
            TopFilterViewModel.ClearFiltersCommand.Execute(null);
            Disbursements = new ObservableCollection<Disbursement>(await _mediator.Send(new Shared.Commands.Disbursements.GetDisbursementsHistoryByDateCommand(newValue)));
        }

        [ObservableProperty]
        private ObservableCollection<Disbursement> _disbursements;

        [ObservableProperty]
        private DateTime _searchDate;

        private readonly IMediator _mediator = mediator;
        private readonly IMessenger _messenger = messenger;
    }
}
