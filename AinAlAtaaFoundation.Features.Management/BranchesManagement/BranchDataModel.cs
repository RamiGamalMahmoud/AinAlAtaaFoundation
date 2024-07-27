using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AinAlAtaaFoundation.Features.Management.BranchesManagement
{
    internal partial class BranchDataModel : ObservableValidator, IDataModel<Branch>
    {
        public BranchDataModel(Branch model)
        {
            if(model is not null)
            {
                Model = model;
                Name = model.Name;
                Clan = model.Clan;
            }

            ValidateAllProperties();
        }

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private string _name;

        [Required(ErrorMessage = "حقل مطلوب")]
        public Clan Clan
        {
            get => _clan;
            set
            {
                if(_clan is null || value is null || _clan.Id != value.Id)
                {
                    SetProperty(ref _clan, value, true);
                }
            }
        }
        private Clan _clan;

        public bool IsValid => !HasErrors;

        public Branch Model { get; }

        public void UpdateModel(Branch model = null)
        {
            model.Name = Name;
            model.Clan = Clan;
        }
    }
}