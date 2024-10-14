using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.SocialStatuses
{
    internal partial class SocialStatusDataModel : ObservableValidator, IDataModel<SocialStatus>
    {
        public SocialStatusDataModel(SocialStatus model)
        {
            if(model is not null)
            {
                Model = model;
                Name = model.Name;
            }
            ValidateAllProperties();
        }

        public bool IsValid => !HasErrors;

        public SocialStatus Model { get; }

        public void UpdateModel(SocialStatus model = null)
        {
            SocialStatus modelToUpdate = model is null ? Model : model;

            modelToUpdate.Name = Name;
        }

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private string _name;

    }
}
