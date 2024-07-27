using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace AinAlAtaaFoundation.Features.Management.DistrictsManagement
{
    internal partial class DistrictDataModel : ObservableValidator, IDataModel<District>
    {
        public DistrictDataModel(District model)
        {
            if(model is not null)
            {
                Model = model;
                Name = model.Name;
            }
            ValidateAllProperties();
        }
        public bool IsValid => !HasErrors;

        public District Model { get; }

        public void UpdateModel(District model)
        {
            ArgumentNullException.ThrowIfNull(model);

            model.Name = Name;
        }

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private string _name;
    }
}
