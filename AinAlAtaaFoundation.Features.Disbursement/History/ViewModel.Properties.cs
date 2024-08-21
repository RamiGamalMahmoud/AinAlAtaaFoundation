using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.History
{
    internal partial class ViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Disbursement> _disbursements;

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
        }

        [ObservableProperty]
        private IEnumerable<DistrictRepresentative> _districtRepresentatives;
        [ObservableProperty]
        private DistrictRepresentative _districtRepresentative;
    }
}
