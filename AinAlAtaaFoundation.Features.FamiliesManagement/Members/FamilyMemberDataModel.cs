using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using AinAlAtaaFoundation.Shared.Validations;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members
{
    internal partial class FamilyMemberDataModel : ObservableValidator, IDataModel<FamilyMember>
    {
        public FamilyMemberDataModel(FamilyMember model)
        {
            if (model is not null)
            {
                Model = model;
                Name = model.Name;
                Family = model.Family;
                Gender = model.Gender;
                YearOfBirth = model.YearOfBirth;
                Mother = model.Mother;
                IsOrphan = model.IsOrphan;
                IsDeserves = model.IsDeserves;
                IsSponsored = model.IsSponsored;
            }
            ValidateAllProperties();
        }

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private string _name;

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [YearValidation(ErrorMessage = "تأكد من كتابة السنة بشكل صحيح")]
        [NotifyPropertyChangedFor(nameof(Age))]
        [NotifyPropertyChangedFor(nameof(IsNowOrphan))]
        [NotifyDataErrorInfo]
        private int _yearOfBirth;

        [ObservableProperty]
        private bool _isDeserves = true;

        [ObservableProperty]
        private bool _isSponsored = false;

        public bool IsNowOrphan => Family?.SocialStatus.Name == "أيتام" && Age < 18;

        [ObservableProperty]
        private bool _isOrphan = false;

        public int Age => DateTime.Now.Year - YearOfBirth;

        [Required(ErrorMessage = "حقل مطلوب")]
        public Gender Gender
        {
            get => _gender;
            set
            {
                if (_gender is null || value is null || _gender.Id != value.Id)
                    SetProperty(ref _gender, value, true);
            }
        }
        private Gender _gender;

        [Required(ErrorMessage = "حقل مطلوب")]
        public Family Family
        {
            get => _family;
            set
            {
                if (_family is null || value is null || _family.Id != value.Id)
                    SetProperty(ref _family, value, true);
            }
        }
        private Family _family;

        public FamilyMember Mother
        {
            get => _mother;

            set
            {
                if(_mother is null || value is null || _mother.Id != value.Id)
                {
                    SetProperty(ref _mother, value);
                }
            }
        }
        private FamilyMember _mother;

        public bool IsValid => !HasErrors;

        public FamilyMember Model { get; }

        public void UpdateModel(FamilyMember model = null)
        {
            model.Name = Name;
            model.YearOfBirth = YearOfBirth;
            model.Family = Family;
            model.Gender = Gender;
            model.Mother = Mother;
            model.IsOrphan = IsOrphan;
            model.IsDeserves = IsDeserves;
            model.IsSponsored = IsSponsored;
        }
    }
}
