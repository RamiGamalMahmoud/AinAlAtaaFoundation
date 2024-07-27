using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AinAlAtaaFoundation.Features.Management.FeaturedPointsManagement
{
    internal partial class FeaturedPointDataModel : ObservableValidator, IDataModel<FeaturedPoint>
    {
        public FeaturedPointDataModel(FeaturedPoint model)
        {
            if(model is not null)
            {
                Model = model;
                Name = model.Name;
                District = model.District;
            }

            ValidateAllProperties();
        }
        public bool IsValid => !HasErrors;

        public FeaturedPoint Model { get; }

        public void UpdateModel(FeaturedPoint model = null)
        {
            model.Name = Name;
            model.District = District;
        }

        public int Id { get; set; }
        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private string _name;

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
    }
}
