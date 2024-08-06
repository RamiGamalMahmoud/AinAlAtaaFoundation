using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace AinAlAtaaFoundation.Models
{
    public partial class FamilyMember : ObservableObject
    {
        public int Id { get; set; }

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Age))]
        [NotifyPropertyChangedFor(nameof(IsNowOrphan))]
        private DateTime _birthDate;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNowOrphan))]
        private bool _isOrphan = false;

        [ObservableProperty]
        private bool _isApplicant = false;

        [ObservableProperty]
        private bool _isDeserves = true;

        public bool IsNowOrphan => Family.SocialStatus.Name == "أيتام" && Age < 18;
        public int Age => (int.Parse(DateTime.Now.ToString("yyyyMMdd")) - int.Parse(BirthDate.ToString("yyyyMMdd"))) / 10000;

        public int GenderId { get; set; }
        public Gender Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }
        private Gender _gender;

        public int FamilyId { get; set; }
        public Family Family
        {
            get => _family;
            set => SetProperty(ref _family, value);
        }
        private Family _family;

        public int? MotherId { get; set; }
        public FamilyMember Mother
        {
            get => _mother;
            set => SetProperty(ref _mother, value);
        }
        private FamilyMember _mother;

    }
}
