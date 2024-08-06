using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using HandyControl.Tools.Extension;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement
{
    public partial class DistrictRepresentativeDataModel : ObservableValidator, IDataModel<DistrictRepresentative>
    {
        public DistrictRepresentativeDataModel(DistrictRepresentative model = null)
        {
            if (model is null)
            {
                Model = new DistrictRepresentative();
                Phones = [];
            }

            else
            {
                Model = model;
                Name = model.Name;
                District = model.District;
                Street = model.Address.Street;
                Phones = new ObservableCollection<Phone>(model.Phones);
            }

            ValidateAllProperties();
        }

        public DistrictRepresentative Model { get; private set; }

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private string _name;

        [ObservableProperty]
        private string _street;

        [Required(ErrorMessage = "حقل مطلوب")]
        public District District
        {
            get => _district;
            set
            {
                if (_district is null || value is null || _district.Id != value.Id)
                {
                    SetProperty(ref _district, value, true);
                }
            }
        }
        private District _district;

        public ObservableCollection<Phone> Phones { get => _phones; private set => SetProperty(ref _phones, value); }
        private ObservableCollection<Phone> _phones;

        public bool IsValid => !HasErrors;

        public void UpdateModel(DistrictRepresentative model = null)
        {
            Model.Name = Name;
            Model.District = District;
            Model.Address.District = District;
            Model.Address.Street = Street;
            Model.Phones.Clear();
            Model.Phones.AddRange(Phones);
        }
    }
}