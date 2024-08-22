using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Shared.Components
{
    public partial class TopFilterViewModel(IMediator mediator) : ObservableObject
    {
        public async Task LoadDataAsync()
        {
            Clans = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Clan>());
            FamilyTypes = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<FamilyType>());
            SocialStatuses = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<SocialStatus>());

            Districts = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<District>());
        }

        [RelayCommand]
        private void ClearFilters()
        {
            Clan = null;

            Branch = null;
            BranchRepresentative = null;

            District = null;
            DistrictRepresentative = null;

            FamilyType = null;
            SocialStatus = null;
            OrphanType = null;
        }

        [ObservableProperty]
        private IEnumerable<Clan> _clans;
        [ObservableProperty]
        private Clan _clan;
        async partial void OnClanChanged(Clan oldValue, Clan newValue)
        {
            Branches = await _mediator.Send(new Shared.Commands.Branches.CommandGetByClan(newValue));
            BranchRepresentatives = await _mediator.Send(new Shared.Commands.BranchRepresentatives.CommandGetByClan(newValue));
        }

        [ObservableProperty]
        private IEnumerable<Branch> _branches;
        [ObservableProperty]
        private Branch _branch;
        async partial void OnBranchChanged(Branch oldValue, Branch newValue)
        {
            if (newValue is null)
            {
                BranchRepresentatives = await _mediator.Send(new Shared.Commands.BranchRepresentatives.CommandGetByClan(Clan));
            }
            else
            {
                BranchRepresentatives = await _mediator.Send(new Shared.Commands.BranchRepresentatives.CommandGetByBranch(newValue));

            }
        }

        [ObservableProperty]
        private IEnumerable<BranchRepresentative> _branchRepresentatives;
        [ObservableProperty]
        private BranchRepresentative _branchRepresentative;

        [ObservableProperty]
        private IEnumerable<FamilyType> _familyTypes;
        [ObservableProperty]
        private FamilyType _familyType;

        [ObservableProperty]
        private IEnumerable<OrphanType> _orphanTypes;
        [ObservableProperty]
        private OrphanType _orphanType;

        [ObservableProperty]
        private IEnumerable<SocialStatus> _socialStatuses;
        [ObservableProperty]
        private SocialStatus _socialStatus;
        async partial void OnSocialStatusChanged(SocialStatus oldValue, SocialStatus newValue)
        {
            if (newValue is not null && newValue.Name == "أيتام")
            {
                OrphanTypes = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<OrphanType>());
            }
            else OrphanTypes = [];
        }

        [ObservableProperty]
        private IEnumerable<District> _districts;
        [ObservableProperty]
        private District _district;
        async partial void OnDistrictChanged(District oldValue, District newValue)
        {
            DistrictRepresentatives = await _mediator.Send(new Shared.Commands.DistrictRepresentatives.CommandGetByDistrict(newValue));
            FeaturedPoints = await _mediator.Send(new Shared.Commands.FeaturedPoints.CommandGetByDistrict(newValue));
        }

        [ObservableProperty]
        private IEnumerable<DistrictRepresentative> _districtRepresentatives;
        [ObservableProperty]
        private DistrictRepresentative _districtRepresentative;

        [ObservableProperty]
        private IEnumerable<FeaturedPoint> _featuredPoints;
        [ObservableProperty]
        private FeaturedPoint _featuredPoint;

        private readonly IMediator _mediator = mediator;
    }
}
