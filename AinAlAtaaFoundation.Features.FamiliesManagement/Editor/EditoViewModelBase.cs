using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Editor
{
    internal abstract partial class EditoViewModelBase : ViewModelBase<FamilyDataModel>
    {
        protected EditoViewModelBase(IMediator mediator, IMessenger messenger, Family model) : base(mediator, messenger)
        {
            DataModel = new FamilyDataModel(model);
            HasChangesObject = new Shared.Helpers.HasChangesObject(SaveCommand.NotifyCanExecuteChanged);
            DataModel.PropertyChanged += DataModel_PropertyChanged1;
        }

        private async void DataModel_PropertyChanged1(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            if (e.PropertyName == nameof(DataModel.Clan))
            {
                Branches = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Branch>()))
                    .Where(x => x.ClanId == DataModel.Clan.Id);

                var branchRepresentatives = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<BranchRepresentative>(true)));

                BranchRepresentatives = branchRepresentatives.Where(x => x.Clan.Id == DataModel.Clan.Id);
            }
            else if (e.PropertyName == nameof(DataModel.District))
            {
                DistrictRepresentatives = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<DistrictRepresentative>()))
                    .Where(x => x.DistrictId == DataModel.District.Id);

                FeaturedPoints = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<FeaturedPoint>()))
                    .Where(x => x.DistrictId == DataModel.District.Id);
            }
            else if (e.PropertyName == nameof(DataModel.Branch))
            {

            }
            HasChangesObject.SetHaschanges();
        }

        public override async Task LoadDataAsync()
        {
            using (DoBusyWorkFactory.CreateBusyWork(DoBusyWorkObject))
            {
                FamilyTypes = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<FamilyType>());
                SocialStatuses = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<SocialStatus>());
                Districts = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<District>());
                Clans = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Clan>());

                FeaturedPoints = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<FeaturedPoint>());
                OrphanTypes = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<OrphanType>());

                Genders = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Gender>());
            }
        }

        [RelayCommand(CanExecute = nameof(CanAddPhoneNumber))]
        private void AddPhoneNumber()
        {
            DataModel.Phones.Add(new Phone() { Number = PhoneNumber });
            PhoneNumber = "";
        }

        [RelayCommand]
        private void RemovePhone(Phone phone)
        {
            DataModel.Phones.Remove(phone);
        }

        [RelayCommand]
        private async Task AddBranchRepresentative()
        {
            await _mediator.Send(new Shared.Commands.BranchRepresentatives.ShowCreate());
        }

        [RelayCommand]
        private async Task AddDistrictRepresentative()
        {
            await _mediator.Send(new Shared.Commands.DistrictRepresentatives.ShowCreate());
        }

        [RelayCommand]
        private void AddClan()
        {
            _mediator.Send(new Shared.Commands.Clans.CommandShowCreate());
        }

        [RelayCommand]
        private void AddBranch()
        {
            _mediator.Send(new Shared.Commands.Branches.CommandShowCreate());
        }

        [RelayCommand]
        private void AddDistrict()
        {
            _mediator.Send(new Shared.Commands.Districts.CommandShowCreate());
        }

        [RelayCommand]
        private void AddFeaturedPoint()
        {
            _mediator.Send(new Shared.Commands.FeaturedPoints.CommandShowCreate());
        }

        private bool CanAddPhoneNumber() => !string.IsNullOrEmpty(PhoneNumber);

        public override bool CanSave() => DataModel.IsValid;

        [ObservableProperty]
        IEnumerable<Gender> _genders;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddPhoneNumberCommand))]
        private string _phoneNumber;

        [ObservableProperty]
        private IEnumerable<FamilyType> _familyTypes;

        [ObservableProperty]
        private IEnumerable<District> _districts;

        [ObservableProperty]
        private IEnumerable<Clan> _clans;

        [ObservableProperty]
        private IEnumerable<Branch> _branches;

        [ObservableProperty]
        private IEnumerable<BranchRepresentative> _branchRepresentatives;

        [ObservableProperty]
        private IEnumerable<DistrictRepresentative> _districtRepresentatives;

        [ObservableProperty]
        private IEnumerable<FeaturedPoint> _featuredPoints;

        [ObservableProperty]
        private IEnumerable<SocialStatus> _socialStatuses;

        [ObservableProperty]
        private IEnumerable<OrphanType> _orphanTypes;
    }
}
