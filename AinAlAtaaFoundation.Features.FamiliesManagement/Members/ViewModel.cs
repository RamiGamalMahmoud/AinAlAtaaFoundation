﻿using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members
{
    internal partial class ViewModel : ObservableObject
    {
        public ViewModel(IMediator mediator, IMessenger messenger)
        {
            _mediator = mediator;

            messenger.Register<Shared.Messages.EntityUpdated<Family>>(this, async (r, m) => await LoadDataAsync(true));
            messenger.Register<Shared.Messages.EntityRestored<Family>>(this, async (r, m) => await LoadDataAsync(true));
            messenger.Register<Shared.Messages.EntityRemoved<Family>>(this, async (r, m) => await LoadDataAsync(true));

        }

        public async Task LoadDataAsync(bool reload = false)
        {
            using (DoBusyWorkFactory.CreateBusyWork(DoBusyWorkObject))
            {
                _allFamilyMembers = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<FamilyMember>(reload));
                Clans = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Clan>());
                FamilyMembers = _allFamilyMembers;
                Families = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Family>());
            }
        }

        public DoBusyWorkObject DoBusyWorkObject { get; } = new DoBusyWorkObject();

        partial void OnSearchValueChanged(string oldValue, string newValue)
        {
            SelectedClan = null;
            FamilyMembers = _allFamilyMembers.Where(
                x => x.Name.Contains(newValue) ||
                x.Family.Clan.Name.Contains(newValue) ||
                x.Family.RationCard.Contains(newValue));
        }

        async partial void OnSelectedClanChanged(Clan oldValue, Clan newValue)
        {
            await FilterFamilies();
            FilterMembers();
        }

        partial void OnSelectedFamilyChanged(Family oldValue, Family newValue)
        {
            if (newValue is not null)
                FamilyMembers = _allFamilyMembers.Where(x => x.Family.Id == newValue.Id);
            else FamilyMembers = _allFamilyMembers;
        }

        private async Task FilterFamilies()
        {
            if (SelectedClan is null)
            {
                SelectedFamily = null;   
                Families = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Family>());
                return;
            }
            Families = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Family>()))
                .Where(x => SelectedClan is not null && x.Clan.Id == SelectedClan.Id);
        }

        private void FilterMembers()
        {
            if (SelectedClan is null)
            {
                FamilyMembers = _allFamilyMembers;
                return;
            }
            FamilyMembers = _allFamilyMembers.Where(x => SelectedClan is not null && x.Family.Clan.Id == SelectedClan.Id);
        }

        [RelayCommand]
        private void ShowCreate()
        {
            _mediator.Send(new Shared.Commands.Generic.ShowCreate<FamilyMember>());
        }

        [RelayCommand]
        private void ShowUpdate(FamilyMember familyMember)
        {
            _mediator.Send(new Shared.Commands.Generic.ShowUpdate<FamilyMember>(familyMember));
        }

        [ObservableProperty]
        private string _searchValue;

        [ObservableProperty]
        private IEnumerable<Clan> _clans;

        [ObservableProperty]
        private Clan _selectedClan;

        [ObservableProperty]
        private IEnumerable<Family> _families;

        [ObservableProperty]
        private Family _selectedFamily;

        public IEnumerable<FamilyMember> FamilyMembers
        {
            get => _familyMembers;
            private set => SetProperty(ref _familyMembers, value);
        }
        private IEnumerable<FamilyMember> _familyMembers;
        private IEnumerable<FamilyMember> _allFamilyMembers;
        private readonly IMediator _mediator;
    }
}
