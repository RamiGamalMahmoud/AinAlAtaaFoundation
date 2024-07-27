using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AinAlAtaaFoundation.Features.Management
{
    internal partial class ClanDataModel : ObservableValidator, IDataModel<Clan>
    {
        public ClanDataModel(Clan model)
        {
            if(model is not null)
            {
                Model = model;
                Name = model.Name;
            }
            ValidateAllProperties();
        }

        public bool IsValid => !HasErrors;

        public Clan Model { get; private set; }

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private string _name;

        public void UpdateModel(Clan model = null)
        {
            model.Name = Name;
        }
    }
}
