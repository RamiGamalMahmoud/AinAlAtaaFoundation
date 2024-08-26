using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using AinAlAtaaFoundation.Shared.Validations;
using CommunityToolkit.Mvvm.ComponentModel;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

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
                YearOfBirth = model.Applicant.YearOfBirth;
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

                IsSponsered = model.IsSponsored;
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
            model.Applicant.YearOfBirth = YearOfBirth;

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

            model.IsSponsored = IsSponsered;
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
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "حقل مطلوب")]
        [YearValidation(ErrorMessage = "تأكد من كتابة السنة بشكل صحيح")]
        private int _yearOfBirth;

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private District _district;

        public int Age => DateTime.Now.Year - YearOfBirth;

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private Gender _gender;

        [ObservableProperty]
        private bool _isSponsered;

        [ObservableProperty]
        private FeaturedPoint _featuredPoint;

        [ObservableProperty]
        private string _street;

        [ObservableProperty]
        private bool _isOrphan;

        [ObservableProperty]
        private string _notes;

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private Clan _clan;

        [ObservableProperty]
        private Branch _branch;

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private BranchRepresentative _branchRepresentative;

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private DistrictRepresentative _districtRepresentative;

        public int AddressId { get; set; }
        [ObservableProperty]
        private Address _address;

        [Required(ErrorMessage = "حقل مطلوب")]
        public SocialStatus SocialStatus
        {
            get => _socialStatus;
            set
            {
                SetProperty(ref _socialStatus, value, true);
                IsOrphan = value is not null && value.Name == "أيتام";
                if (!IsOrphan)
                {
                    OrphanType = null;
                    IsSponsered = false;
                }
            }
        }
        private SocialStatus _socialStatus;

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private FamilyType _familyType;

        [ObservableProperty]
        private OrphanType _orphanType;

        [ObservableProperty]
        private FamilyMember _applicant;

        public ObservableCollection<Phone> Phones { get; } = [];

        public ICollection<FamilyMember> FamilyMembers { get; } = [];
    }
}
