using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using AinAlAtaaFoundation.Shared.Abstraction;
using AinAlAtaaFoundation.Shared.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Listing
{
    internal partial class ViewModel : ObservableObject
    {
        public ViewModel(IMediator mediator, IMessenger messenger, TopFilterViewModel topFilterViewModel, IAppState appState)
        {
            _mediator = mediator;
            _messenger = messenger;
            _messenger.Register<ViewModel, Shared.Messages.EntityCreated<FamilyMember>>(this, async (reciver, message) => await LoadDataAsync(true));
            _messenger.Register<ViewModel, Shared.Messages.EntityUpdated<FamilyMember>>(this, async (reciver, message) => await LoadDataAsync(true));
            _messenger.Register<ViewModel, Shared.Messages.EntityRestored<Family>>(this, async (reciver, message) => await LoadDataAsync(true));

            _messenger.Register<Shared.Messages.EntityUpdated<Family>>(this, async (r, m) =>
            {
                await ShowPrintFamily(m.Entity);
            });

            _messenger.Register<Shared.Messages.EntityCreated<Family>>(this, async (r, m) =>
            {
                await ShowPrintFamily(m.Entity);
            });

            TopFilterViewModel = topFilterViewModel;
            _appState = appState;
        }

        public async Task LoadDataAsync(bool reload = false)
        {
            using (DoBusyWorkFactory.CreateBusyWork(DoBusyWorkObject))
            {
                _allFamilies = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Family>(reload));
                Families = new ObservableCollection<Family>(_allFamilies);

                await TopFilterViewModel.LoadDataAsync();
            }
        }

        public TopFilterViewModel TopFilterViewModel { get; }
        [RelayCommand]
        private async Task ShowCreate()
        {
            await _mediator.Send(new Shared.Commands.Generic.ShowCreate<Family>());
        }

        [RelayCommand]
        private void ShowUpdateMember(FamilyMember familyMember)
        {
            _mediator.Send(new Shared.Commands.Generic.ShowUpdate<FamilyMember>(familyMember));
        }

        private static Dictionary<string, string> GetFamilyParameters(Family family)
        {
            string barcodeImageString = Shared.GenerateBarCode.ToBarCodeString(family.Id);
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("Id", family.Id.ToString());
            parameters.Add("img", barcodeImageString);
            if(!string.IsNullOrEmpty(family.ImagePath)) parameters.Add("FamilyImage", $"file:///{family.ImagePath}");
            parameters.Add("FamilyType", family.FamilyType.Name);
            parameters.Add("SocialStatus", family.SocialStatus.Name);
            parameters.Add("OrphanType", family.OrphanType?.Name);
            parameters.Add("Clan", family.Clan.Name);
            parameters.Add("Branch", family.Branch?.Name);
            parameters.Add("BranchRepresentative", family.BranchRepresentative.Name);
            parameters.Add("District", family.Address.District.Name);
            parameters.Add("FeaturedPoint", family.Address.FeaturedPoint?.Name);
            parameters.Add("Street", family.Address.Street);
            parameters.Add("DistrictRepresentative", family.DistrictRepresentative.Name);
            parameters.Add("RationCard", family.RationCard);
            parameters.Add("RationCardOwnerName", family.RationCardOwnerName);
            parameters.Add("Notes", family.Notes);
            parameters.Add("ApplicantName", family.Applicant.Name);
            parameters.Add("IsSponsoredParameter", family.IsSponsored.ToString());
            parameters.Add("DateCreated", family.DateCreated?.ToString());
            parameters.Add("HusbandName", family.HusbandName);

            return parameters;
        }

        public DoBusyWorkObject DoBusyWorkObject { get; } = new DoBusyWorkObject();

        [RelayCommand]
        private void ViewAll()
        {
            Families = _allFamilies;
        }

        [RelayCommand(CanExecute = nameof(CanPerformFamilyAction))]
        private async Task Remove(Family family)
        {
            await _mediator.Send(new Shared.Commands.Generic.RemoveCommand<Family>(family));
            _messenger.Send(new Shared.Notifications.SuccessNotification("تم حذف العائلة"));
            _messenger.Send(new Shared.Messages.EntityRemoved<Family>(family));
        }

        [RelayCommand]
        private void Filter()
        {
            IsVewAll = false;
            Families = _allFamilies?
                .Where(x => string.IsNullOrEmpty(TopFilterViewModel.FamilyId) || x.Id.ToString().Contains(TopFilterViewModel.FamilyId))
                .Where(x => string.IsNullOrEmpty(TopFilterViewModel.RationCard) || x.RationCard.Contains(TopFilterViewModel.RationCard))
                .Where(x => string.IsNullOrEmpty(TopFilterViewModel.RationCardOwner) || x.RationCardOwnerName.Contains(TopFilterViewModel.RationCardOwner))
                .Where(x => string.IsNullOrEmpty(TopFilterViewModel.ApplicantName) || x.Applicant.Name.Contains(TopFilterViewModel.ApplicantName))
                .Where(x => TopFilterViewModel.Clan is null || x.Clan is null || x.Clan.Id == TopFilterViewModel.Clan.Id)
                .Where(x => TopFilterViewModel.Branch is null || x.Branch is not null && x.Branch.Id == TopFilterViewModel.Branch.Id)
                .Where(x => TopFilterViewModel.BranchRepresentative is null || x.BranchRepresentative is null || x.BranchRepresentative.Id == TopFilterViewModel.BranchRepresentative.Id)
                .Where(x => TopFilterViewModel.SocialStatus is null || x.SocialStatus is null || x.SocialStatus.Id == TopFilterViewModel.SocialStatus.Id)
                .Where(x => TopFilterViewModel.FamilyType is null || x.FamilyType is null || x.FamilyType.Id == TopFilterViewModel.FamilyType.Id)
                .Where(x => TopFilterViewModel.OrphanType is null || x.OrphanType is null || x.OrphanType == TopFilterViewModel.OrphanType)
                .Where(x => TopFilterViewModel.District is null || x.Address.District.Id == TopFilterViewModel.District.Id)
                .Where(x => TopFilterViewModel.DistrictRepresentative is null || x.DistrictRepresentative.Id == TopFilterViewModel.DistrictRepresentative.Id)
                .Where(x => TopFilterViewModel.FeaturedPoint is null || x.Address.FeaturedPoint is null || x.Address.FeaturedPoint == TopFilterViewModel.FeaturedPoint)
                .Where(x => TopFilterViewModel.SponsoringStatus is null || x.IsSponsored == TopFilterViewModel.SponsoringStatus.IsSponsored)
                .Where(x => TopFilterViewModel.AssociationRepresentative is null || x.AssociationRepresentative is not null && x.AssociationRepresentative.Id == TopFilterViewModel.AssociationRepresentative.Id);
        }

        public int FamiliesCount => Families?.Count() ?? 0;

        [RelayCommand]
        private async Task ShowDeletedFamilies()
        {
            await _mediator.Send(new Shared.Commands.Generic.ShowDeletedEntities<Family>());
        }

        [ObservableProperty]
        private bool _isVewAll = true;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FamiliesCount))]
        private IEnumerable<Family> _families;

        private IEnumerable<Family> _allFamilies;

        private readonly IMediator _mediator;
        private readonly IMessenger _messenger;
        private readonly IAppState _appState;
    }
}
