using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

            messenger.Register<Shared.Messages.EntityCreated<Branch>>(this, async (r, m) =>
            {
                await LoadBranches();
            });

            messenger.Register<Shared.Messages.EntityCreated<BranchRepresentative>>(this, async (r, m) =>
            {
                await LoadBranchRepresentatives();
            });

            messenger.Register<Shared.Messages.EntityCreated<DistrictRepresentative>>(this, async (r, m) =>
            {
                await LoadDistrictRepresentatives();
            });

            messenger.Register<Shared.Messages.EntityCreated<FeaturedPoint>>(this, async (r, m) =>
            {
                await LoadFeaturedPoints();
            });
        }

        private async void DataModel_PropertyChanged1(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            if (e.PropertyName == nameof(DataModel.Clan))
            {
                await LoadBranches();
                await LoadBranchRepresentatives();
            }
            else if (e.PropertyName == nameof(DataModel.District))
            {
                await LoadDistrictRepresentatives();
                await LoadFeaturedPoints();
            }
            else if (e.PropertyName == nameof(DataModel.Branch))
            {
                await LoadBranchRepresentatives();
            }

            else if (e.PropertyName == nameof(DataModel.RationCard))
            {
                await OnRationCardChanged(DataModel.RationCard);
            }

            else if (e.PropertyName == nameof(DataModel.Name))
            {
                await OnNameChanged(DataModel.Name);
            }

            else if (e.PropertyName == nameof(DataModel.RationCardOwnerName))
            {
                await OnRationCardOwnerChanged(DataModel.RationCardOwnerName);
            }

            HasChangesObject.SetHaschanges();
        }

        private async Task OnRationCardOwnerChanged(string rationCardOwnerName)
        {
            if (string.IsNullOrEmpty(rationCardOwnerName))
            {
                FoundFamiliesForRationCardOwner = [];
            }
            else FoundFamiliesForRationCardOwner = await _mediator.Send(new Shared.Commands.Families.GetByRationCardOwner(rationCardOwnerName));
        }

        private async Task LoadBranches()
        {
            if (DataModel.Clan is not null)
                Branches = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Branch>()))
                    .Where(x => x.ClanId == DataModel.Clan.Id);
        }

        private async Task LoadBranchRepresentatives()
        {
            //if (DataModel.Branch is null || DataModel.Clan is null) BranchRepresentatives = [];
            if (DataModel.Branch is not null)
            {
                BranchRepresentatives = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<BranchRepresentative>())
                    .ContinueWith(x =>
                    x.Result
                    .Where(x => x.Branch is not null && x.Branch.Id == DataModel.Branch.Id));
            }
            else if (DataModel.Clan is not null)
                BranchRepresentatives = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<BranchRepresentative>()))
                    .Where(x => x.Clan.Id == DataModel.Clan.Id);
        }

        private async Task LoadDistrictRepresentatives()
        {
            if (DataModel.District is not null)
                DistrictRepresentatives = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<DistrictRepresentative>()))
                    .Where(x => x.DistrictId == DataModel.District.Id);
        }

        private async Task LoadFeaturedPoints()
        {
            if (DataModel.District is not null)
                FeaturedPoints = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<FeaturedPoint>()))
                    .Where(x => x.DistrictId == DataModel.District.Id);
        }

        public override async Task LoadDataAsync()
        {
            using (DoBusyWorkFactory.CreateBusyWork(DoBusyWorkObject))
            {
                FamilyTypes = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<FamilyType>());
                SocialStatuses = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<SocialStatus>());
                Districts = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<District>());
                Clans = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Clan>());

                OrphanTypes = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<OrphanType>());

                Genders = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Gender>());

                await LoadBranches();
                await LoadBranchRepresentatives();
                await LoadDistrictRepresentatives();
                await LoadFeaturedPoints();
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
        private async Task AddSocialStatus()
        {
            await _mediator.Send(new Shared.Commands.Generic.ShowCreate<SocialStatus>());
        }

        [RelayCommand]
        private async Task AddBranchRepresentative()
        {
            await _mediator.Send(new Shared.Commands.Generic.ShowCreate<BranchRepresentative>());
        }

        [RelayCommand]
        private async Task AddDistrictRepresentative()
        {
            await _mediator.Send(new Shared.Commands.Generic.ShowCreate<DistrictRepresentative>());
        }

        [RelayCommand]
        private void AddClan()
        {
            _mediator.Send(new Shared.Commands.Generic.ShowCreate<Clan>());
        }

        [RelayCommand]
        private void AddBranch()
        {
            _mediator.Send(new Shared.Commands.Generic.ShowCreate<Branch>());
        }

        [RelayCommand]
        private void AddDistrict()
        {
            _mediator.Send(new Shared.Commands.Generic.ShowCreate<District>());
        }

        [RelayCommand]
        private void AddFeaturedPoint()
        {
            _mediator.Send(new Shared.Commands.Generic.ShowCreate<FeaturedPoint>());
        }

        private bool CanAddPhoneNumber() => !string.IsNullOrEmpty(PhoneNumber);

        public override bool CanSave() => !HasMessageObject.HasMessage && !HasErrors && DataModel.IsValid;

        private async Task OnRationCardChanged(string rationCard)
        {
            if (string.IsNullOrEmpty(rationCard))
            {
                FoundFamilyForRationCard = null;
                HasMessageObject.Clear();
            }

            else
            {
                FoundFamilyForRationCard = await _mediator.Send(new Shared.Commands.Families.GetByRationCard(rationCard));
                if (FoundFamilyForRationCard is null)
                {
                    HasMessageObject.Clear();
                }
                else
                {
                    HasMessageObject.Message = "رقم بطاقة تموينية مكرر";
                }
            }
        }

        private async Task OnNameChanged(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                FoundedNames = [];
            }
            else
            {
                FoundedNames = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Family>())
                    .ContinueWith(x => x
                    .Result
                    .Where(x => x.Applicant.Name == name));
            }
        }

        public HasMessageObject HasMessageObject { get; } = new HasMessageObject();

        [ObservableProperty]
        private Family _foundFamilyForRationCard;

        [ObservableProperty]
        private IEnumerable<Family> _foundedNames;

        [ObservableProperty]
        private IEnumerable<Family> _foundFamiliesForRationCardOwner;

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
