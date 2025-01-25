using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace AinAlAtaaFoundation.Features.Management.AssociationRepresentatives
{
    internal partial class AssociationRepresentativeDataModel : ObservableValidator, IDataModel<AssociationRepresentative>
    {
        public AssociationRepresentativeDataModel(AssociationRepresentative model)
        {
            if (model is not null)
            {
                Model = model;
                Name = model.Name;
            }
            ValidateAllProperties();
        }

        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private string _name;

        public bool IsValid => !HasErrors;

        public AssociationRepresentative Model { get; private set; }

        public void UpdateModel(AssociationRepresentative model = null)
        {
            if (model is null)
            {
                model = Model;
            }
            model.Name = Name;
        }
    }
}
