using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
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
                BirthDate = model.BirthDate;
                Mother = model.Mother;
                IsOrphan = model.IsOrphan;
            }
            ValidateAllProperties();
        }

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private string _name;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Age))]
        [NotifyPropertyChangedFor(nameof(IsNowOrphan))]
        [Required(ErrorMessage = "حقل مطلوب")]
        private DateTime _birthDate = new DateTime(1950, 1, 1);

        partial void OnBirthDateChanged(DateTime oldValue, DateTime newValue)
        {
            if (newValue > DateTime.Now)
            {
                BirthDate = oldValue;
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        public bool IsNowOrphan => Family.SocialStatus.Name == "أيتام" && Age < 18;

        public bool IsOrphan
        {
            get => _isOrphan;
            set => SetProperty(ref _isOrphan, value);
        }
        private bool _isOrphan = false;

        public int Age => (int.Parse(DateTime.Now.ToString("yyyyMMdd")) - int.Parse(BirthDate.ToString("yyyyMMdd"))) / 10000;

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
            model.BirthDate = BirthDate;
            model.Family = Family;
            model.Gender = Gender;
            model.Mother = Mother;
            model.IsOrphan = IsOrphan;
        }
    }
}
