using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace AinAlAtaaFoundation.Features.FamiliesManagement
{
    internal partial class FamilyDataModel : ObservableValidator, IDataModel<Family>
    {
        public FamilyDataModel(Family model)
        {
            if (model is null)
            {
                Model = new Family
                {
                    Address = new Address(),
                    Applicant = new FamilyMember(),
                };

                Phones = [];
                FamilyMembers = new ObservableCollection<FamilyMember>();
            }

            else
            {
                Model = model;
                Address = model.Address;
                Applicant = model.Applicant;
                BirthDate = model.Applicant.BirthDate;
                Branch = model.Branch;
                BranchRepresentative = model.BranchRepresentative;
                Clan = model.Clan;

                Gender = model.Applicant.Gender;

                District = model.DistrictRepresentative.District;
                DistrictRepresentative = model.DistrictRepresentative;

                FamilyMembers = model.FamilyMembers;
                FamilyType = model.FamilyType;
                FeaturedPoint = model.Address.FeaturedPoint;
                Name = model.Applicant.Name;
                Notes = model.Notes;
                OrphanType = model.OrphanType;

                Phones = new ObservableCollection<Phone>(model.Phones);
                RationCard = model.RationCard;
                RationCardOwnerName = model.RationCardOwnerName;

                SocialStatus = model.SocialStatus;
                Street = model.Address.Street;
            }
            ValidateAllProperties();
        }

        public bool IsValid => !HasErrors;

        public Family Model { get; }

        public void UpdateModel(Family model)
        {
            model.RationCard = RationCard;
            model.RationCardOwnerName = RationCardOwnerName;

            model.Branch = Branch;
            model.Clan = Clan;

            model.BranchRepresentative = BranchRepresentative;
            model.DistrictRepresentative = DistrictRepresentative;

            model.Applicant.Gender = Gender;

            model.FamilyType = FamilyType;
            model.Applicant.Name = Name;
            model.Applicant.BirthDate = BirthDate;
            model.Applicant = Applicant;

            model.Address.Street = Street;
            model.Address.FeaturedPoint = FeaturedPoint;
            model.Address.District = District;

            model.RationCard = RationCard;
            model.RationCardOwnerName = RationCardOwnerName;

            model.SocialStatus = SocialStatus;
            model.OrphanType = OrphanType;

            model.Notes = Notes;
            model.Phones.Clear();
            model.Phones.AddRange(Phones);
        }

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private string _rationCard;

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private string _name;

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private string _rationCardOwnerName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Age))]
        [Required(ErrorMessage = "حقل مطلوب")]
        private DateTime _birthDate = new DateTime(1950, 1, 1);

        [Required(ErrorMessage = "حقل مطلوب")]
        public District District
        {
            get => _district;
            set
            {
                SetProperty(ref _district, value, true);
                OnPropertyChanged(nameof(IsValid));
            }
        }
        private District _district;

        public int Age => (int.Parse(DateTime.Now.ToString("yyyyMMdd")) - int.Parse(BirthDate.ToString("yyyyMMdd"))) / 10000;

        [Required(ErrorMessage = "حقل مطلوب")]
        public Gender Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value, true);
        }
        private Gender _gender;

        public FeaturedPoint FeaturedPoint
        {
            get => _featuredPoint;
            set
            {
                SetProperty(ref _featuredPoint, value, true);
                OnPropertyChanged(nameof(IsValid));
            }
        }
        private FeaturedPoint _featuredPoint;

        [ObservableProperty]
        private string _street;

        public bool IsOrphan { get => _isOrphan; private set => SetProperty(ref _isOrphan, value); }
        private bool _isOrphan;

        [ObservableProperty]
        private string _notes;

        [Required(ErrorMessage = "حقل مطلوب")]
        public Clan Clan
        {
            get => _clan;
            set
            {
                SetProperty(ref _clan, value, true);
                OnPropertyChanged(nameof(IsValid));
            }
        }
        private Clan _clan;

        public Branch Branch
        {
            get => _branch;
            set
            {
                SetProperty(ref _branch, value, true);
                OnPropertyChanged(nameof(IsValid));
            }
        }
        private Branch _branch;

        [Required(ErrorMessage = "حقل مطلوب")]
        public BranchRepresentative BranchRepresentative
        {
            get => _branchRepresentative;
            set
            {
                SetProperty(ref _branchRepresentative, value, true);
                OnPropertyChanged(nameof(IsValid));
            }
        }
        private BranchRepresentative _branchRepresentative;

        [Required(ErrorMessage = "حقل مطلوب")]
        public DistrictRepresentative DistrictRepresentative
        {
            get => _districtRepresentative;
            set
            {
                SetProperty(ref _districtRepresentative, value, true);
                OnPropertyChanged(nameof(IsValid));
            }
        }
        private DistrictRepresentative _districtRepresentative;

        public int AddressId { get; set; }
        public Address Address
        {
            get => _address;
            set
            {
                SetProperty(ref _address, value, true);
                OnPropertyChanged(nameof(IsValid));
            }
        }
        private Address _address;

        [Required(ErrorMessage = "حقل مطلوب")]
        public SocialStatus SocialStatus
        {
            get => _socialStatus;
            set
            {
                SetProperty(ref _socialStatus, value, true);
                IsOrphan = _socialStatus is not null && _socialStatus.Id == 1;
                if (!IsOrphan) OrphanType = null;
            }
        }
        private SocialStatus _socialStatus;

        [Required(ErrorMessage = "حقل مطلوب")]
        public FamilyType FamilyType
        {
            get => _familyType;
            set
            {
                SetProperty(ref _familyType, value, true);
                OnPropertyChanged(nameof(IsValid));
            }
        }
        private FamilyType _familyType;

        public OrphanType OrphanType
        {
            get => _orphanType;
            set
            {
                SetProperty(ref _orphanType, value, true);
                OnPropertyChanged(nameof(IsValid));
            }
        }
        private OrphanType _orphanType;

        public FamilyMember Applicant
        {
            get => _applicant;
            set
            {
                SetProperty(ref _applicant, value, true);
                OnPropertyChanged(nameof(IsValid));
            }
        }
        private FamilyMember _applicant;

        public ObservableCollection<Phone> Phones { get; } = [];

        public ICollection<FamilyMember> FamilyMembers { get; } = [];
    }
}
