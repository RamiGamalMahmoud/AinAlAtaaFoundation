using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AinAlAtaaFoundation.Models
{
    [ObservableObject]
    public partial class Family : ModelBase
    {
        public Family()
        {
            Phones = new Collection<Phone>();
            FamilyMembers = new Collection<FamilyMember>();
        }

        [ObservableProperty]
        private string _rationCard;

        [ObservableProperty]
        private string _notes;

        [ObservableProperty]
        private string _rationCardOwnerName;

        [ObservableProperty]
        private bool _isSponsored;

        [ObservableProperty]
        private bool _isDelted = false;

        public int ClanId { get; set; }
        public Clan Clan
        {
            get => _clan;
            set => SetProperty(ref _clan, value);
        }
        private Clan _clan;

        public int? BranchId { get; set; }
        public Branch Branch
        {
            get => _branch;
            set => SetProperty(ref _branch, value);
        }
        private Branch _branch;

        public int BranchRepresentativeId { get; set; }
        public BranchRepresentative BranchRepresentative
        {
            get => _branchRepresentative;
            set => SetProperty(ref _branchRepresentative, value);
        }
        private BranchRepresentative _branchRepresentative;

        public int DistrictRepresentativeId { get; set; }
        public DistrictRepresentative DistrictRepresentative
        {
            get => _districtRepresentative;
            set => SetProperty(ref _districtRepresentative, value);
        }
        private DistrictRepresentative _districtRepresentative;

        public int AddressId { get; set; }
        public Address Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }
        private Address _address;

        public int SocialStatusId { get; set; }
        public SocialStatus SocialStatus
        {
            get => _socialStatus;
            set => SetProperty(ref _socialStatus, value);
        }
        private SocialStatus _socialStatus;

        public int FamilyTypeId { get; set; }
        public FamilyType FamilyType
        {
            get => _familyType;
            set => SetProperty(ref _familyType, value);
        }
        private FamilyType _familyType;

        public int? OrphanTypeId { get; set; }
        public OrphanType OrphanType
        {
            get => _orphanType;
            set => SetProperty(ref _orphanType, value);
        }
        private OrphanType _orphanType;

        public int? ApplicantId { get; set; }
        public FamilyMember Applicant
        {
            get => _applicant;
            set => SetProperty(ref _applicant, value);
        }
        private FamilyMember _applicant;

        public ICollection<Phone> Phones { get; }

        public ICollection<FamilyMember> FamilyMembers { get; }
    }
}
