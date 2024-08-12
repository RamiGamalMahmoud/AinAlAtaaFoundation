using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Listing
{
    internal partial class ViewModel : ObservableObject
    {
        public ViewModel(IMediator mediator, IMessenger messenger)
        {
            _mediator = mediator;

            messenger.Register<ViewModel, Shared.Messages.EntityCreated<FamilyMember>>(this, async (reciver, message) => await LoadDataAsync(true));
            messenger.Register<ViewModel, Shared.Messages.EntityUpdated<FamilyMember>>(this, async (reciver, message) => await LoadDataAsync(true));
        }

        public async Task LoadDataAsync(bool reload = false)
        {
            _allFamilies = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Family>(reload));
            Families = _allFamilies;

            Clans = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Clan>());
            FamilyTypes = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<FamilyType>());
            SocialStatuses = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<SocialStatus>());

            Districts = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<District>());
        }

        [RelayCommand]
        private async Task ShowCreate()
        {
            await _mediator.Send(new Shared.Commands.Generic.ShowCreate<Family>());
        }

        [RelayCommand]
        private void ShowUpdate(Family family)
        {
            _mediator.Send(new Shared.Commands.Generic.ShowUpdate<Family>(family));
        }

        [RelayCommand]
        private void ShowPrint(Family family)
        {
            Dictionary<string, object> dataSources = new Dictionary<string, object>
            {
                { "Phones", family.Phones },
                { "Members", family.FamilyMembers }
            };
            _mediator.Send(new Shared.Commands.Generic.PrintCommand("Family.rdlc", GetParameters(family), dataSources));
        }

        [RelayCommand]
        private void PrintBarcode(Family family)
        {
            string barcodeImageString = Shared.GenerateBarCode.ToBarCodeString(family.Id);
            Dictionary<string, List<string>> parameters = new Dictionary<string, List<string>>();
            parameters.Add("Barcode", [barcodeImageString]);
            parameters.Add("RationCard", [family.RationCard]);
            _mediator.Send(new Shared.Commands.Generic.PrintCommand("FamilyBarcode.rdlc", parameters));
        }

        private static Dictionary<string, List<string>> GetParameters(Family family)
        {
            string barcodeImageString = Shared.GenerateBarCode.ToBarCodeString(family.Id);
            Dictionary<string, List<string>> parameters = new Dictionary<string, List<string>>();

            parameters.Add("Id", [family.Id.ToString()]);
            parameters.Add("img", [barcodeImageString]);
            parameters.Add("", []);

            parameters.Add("FamilyType", [family.FamilyType.Name]);
            parameters.Add("SocialStatus", [family.SocialStatus.Name]);
            parameters.Add("OrphanType", [family.OrphanType?.Name]);
            parameters.Add("Clan", [family.Clan.Name]);
            parameters.Add("Branch", [family.Branch?.Name]);
            parameters.Add("BranchRepresentative", [family.BranchRepresentative.Name]);
            parameters.Add("District", [family.Address.District.Name]);
            parameters.Add("FeaturedPoint", [family.Address.FeaturedPoint?.Name]);
            parameters.Add("Street", [family.Address.Street]);
            parameters.Add("DistrictRepresentative", [family.DistrictRepresentative.Name]);
            parameters.Add("RationCard", [family.RationCard]);
            parameters.Add("RationCardOwnerName", [family.RationCardOwnerName]);
            parameters.Add("Notes", [family.Notes]);
            parameters.Add("ApplicantName", [family.Applicant.Name]);

            return parameters;
        }

        [RelayCommand]
        private void ViewAll()
        {
            Clan = null;
            Branch = null;
            BranchRepresentative = null;
            FamilyType = null;
            SocialStatus = null;
            Families = _allFamilies;
        }

        [RelayCommand]
        private void Filter()
        {
            IsVewAll = false;
            Families = _allFamilies
                .Where(x => Clan is null || x.Clan is null || x.Clan.Id == Clan.Id)
                .Where(x => Branch is null || x.Branch is not null && x.Branch.Id == Branch.Id)
                .Where(x => BranchRepresentative is null || x.BranchRepresentative is null || x.BranchRepresentative.Id == BranchRepresentative.Id)
                .Where(x => SocialStatus is null || x.SocialStatus is null || x.SocialStatus.Id == SocialStatus.Id)
                .Where(x => FamilyType is null || x.FamilyType is null ||  x.FamilyType.Id == FamilyType.Id)
                .Where(x => District is null || x.Address.District.Id == District.Id )
                .Where(x => DistrictRepresentative is null || x.DistrictRepresentative.Id == DistrictRepresentative.Id);
        }

        [ObservableProperty]
        private bool _isVewAll = true;

        public IEnumerable<Family> Families
        {
            get => _families;
            private set => SetProperty(ref _families, value);
        }
        private IEnumerable<Family> _families;
        private IEnumerable<Family> _allFamilies;

        [ObservableProperty]
        private IEnumerable<Clan> _clans;

        [ObservableProperty]
        private IEnumerable<Branch> _branches;

        [ObservableProperty]
        private IEnumerable<BranchRepresentative> _branchRepresentatives;

        [ObservableProperty]
        private IEnumerable<FamilyType> _familyTypes;

        [ObservableProperty]
        private IEnumerable<SocialStatus> _socialStatuses;

        [ObservableProperty]
        private Clan _clan;
        async partial void OnClanChanged(Clan oldValue, Clan newValue)
        {
            Branches = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Branch>()))
                .Where(x => newValue is not null && x.Clan.Id == newValue.Id);

            BranchRepresentatives = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<BranchRepresentative>()))
                .Where(x => newValue is not null && x.Clan.Id == newValue.Id);
        }

        [ObservableProperty]
        private Branch _branch;
        async partial void OnBranchChanged(Branch oldValue, Branch newValue)
        {
            if (newValue is null)
            {
                BranchRepresentatives = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<BranchRepresentative>()))
                    .Where(x => x.Clan.Id == Clan.Id);
            }
            else
            {
                BranchRepresentatives = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<BranchRepresentative>()))
                    .Where(x => x.Branch is not null && x.Branch.Id == newValue.Id);
            }
        }

        [ObservableProperty]
        private BranchRepresentative _branchRepresentative;

        [ObservableProperty]
        private IEnumerable<District> _districts;

        [ObservableProperty]
        private District _district;
        async partial void OnDistrictChanged(District oldValue, District newValue)
        {
            DistrictRepresentatives = await _mediator
                .Send(new Shared.Commands.Generic.GetAllCommand<DistrictRepresentative>())
                .ContinueWith(x => x
                .Result
                .Where(x => newValue is not null && x.District.Id == newValue.Id));
        }

        [ObservableProperty]
        private IEnumerable<DistrictRepresentative> _districtRepresentatives;
        [ObservableProperty]
        private DistrictRepresentative _districtRepresentative;

        [ObservableProperty]
        private FamilyType _familyType;

        [ObservableProperty]
        private SocialStatus _socialStatus;

        private readonly IMediator _mediator;
    }
}
