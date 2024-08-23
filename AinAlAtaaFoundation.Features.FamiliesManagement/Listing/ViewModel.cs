using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using AinAlAtaaFoundation.Shared.Components;
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
        public ViewModel(IMediator mediator, IMessenger messenger, TopFilterViewModel topFilterViewModel)
        {
            _mediator = mediator;

            messenger.Register<ViewModel, Shared.Messages.EntityCreated<FamilyMember>>(this, async (reciver, message) => await LoadDataAsync(true));
            messenger.Register<ViewModel, Shared.Messages.EntityUpdated<FamilyMember>>(this, async (reciver, message) => await LoadDataAsync(true));
            TopFilterViewModel = topFilterViewModel;
            TopFilterViewModel.PropertyChanged += TopFilterViewModel_PropertyChanged;
        }

        private void TopFilterViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(TopFilterViewModel.FamilyId))
            {
                if(TopFilterViewModel.FamilyId == 0)
                {
                    Families = _allFamilies;
                }
                else Families = _allFamilies.Where(x => x.Id == TopFilterViewModel.FamilyId);
            }
            else if(e.PropertyName == nameof(TopFilterViewModel.RationCard))
            {
                Families = _allFamilies.Where(x => x.RationCard.Contains(TopFilterViewModel.RationCard));
            }
        }

        public async Task LoadDataAsync(bool reload = false)
        {
            using (DoBusyWorkFactory.CreateBusyWork(DoBusyWorkObject))
            {
                _allFamilies = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Family>(reload));
                Families = _allFamilies;

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
        private void ShowUpdate(Family family)
        {
            _mediator.Send(new Shared.Commands.Generic.ShowUpdate<Family>(family));
        }

        [RelayCommand]
        private void ShowPrint(Family family)
        {
            var members = family.FamilyMembers.Select(member =>
            {
                return new
                {
                    member.Name,
                    member.YearOfBirth,
                    member.Age,
                    IsDeserves = member.IsDeserves ? "نعم" : "لا",
                    MotherName = member.Mother?.Name
                };
            });
            Dictionary<string, object> dataSources = new Dictionary<string, object>
            {
                { "Phones", family.Phones },
                { "Members", members }
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

        public DoBusyWorkObject DoBusyWorkObject { get; } = new DoBusyWorkObject();

        [RelayCommand]
        private void ViewAll()
        {
            Families = _allFamilies;
        }

        [RelayCommand]
        private void Filter()
        {
            IsVewAll = false;
            Families = _allFamilies
                .Where(x => TopFilterViewModel.Clan is null || x.Clan is null || x.Clan.Id == TopFilterViewModel.Clan.Id)
                .Where(x => TopFilterViewModel.Branch is null || x.Branch is not null && x.Branch.Id == TopFilterViewModel.Branch.Id)
                .Where(x => TopFilterViewModel.BranchRepresentative is null || x.BranchRepresentative is null || x.BranchRepresentative.Id == TopFilterViewModel.BranchRepresentative.Id)
                .Where(x => TopFilterViewModel.SocialStatus is null || x.SocialStatus is null || x.SocialStatus.Id == TopFilterViewModel.SocialStatus.Id)
                .Where(x => TopFilterViewModel.FamilyType is null || x.FamilyType is null || x.FamilyType.Id == TopFilterViewModel.FamilyType.Id)
                .Where(x => TopFilterViewModel.OrphanType is null || x.OrphanType is null || x.OrphanType == TopFilterViewModel.OrphanType)
                .Where(x => TopFilterViewModel.District is null || x.Address.District.Id == TopFilterViewModel.District.Id)
                .Where(x => TopFilterViewModel.DistrictRepresentative is null || x.DistrictRepresentative.Id == TopFilterViewModel.DistrictRepresentative.Id)
                .Where(x => TopFilterViewModel.FeaturedPoint is null || x.Address.FeaturedPoint is null || x.Address.FeaturedPoint == TopFilterViewModel.FeaturedPoint);
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

        private readonly IMediator _mediator;
    }
}
