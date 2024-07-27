using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using HandyControl.Tools.Extension;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement
{
    public partial class BranchRepresentativeDataModel : ObservableValidator, IDataModel<BranchRepresentative>
    {
        public BranchRepresentativeDataModel(BranchRepresentative model)
        {
            if (model is null)
            {

            }

            else
            {
                Model = model;
                Name = model.Name;
                Branch = model.Branch;
                Clan = model.Clan;
                Phones = new ObservableCollection<Phone>(model.Phones);
            }
            Phones.CollectionChanged += Phones_CollectionChanged;
            ValidateAllProperties();
        }

        private void Phones_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged();
        }

        public bool IsValid => !HasErrors;

        public bool HasBranch => Branch is not null;
        public bool HasClan => Clan is not null;

        public BranchRepresentative Model { get; }

        public void UpdateModel(BranchRepresentative model = null)
        {
            Model.Name = Name;
            Model.Branch = Branch;
            Model.Phones.Clear();
            Model.Phones.AddRange(Phones);
        }

        public int Id { get; set; }
        [ObservableProperty]
        [Required(ErrorMessage = "حقل مطلوب")]
        [NotifyDataErrorInfo]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        private string _name;

        public ObservableCollection<Phone> Phones { get => _phones; private set => SetProperty(ref _phones, value); }
        private ObservableCollection<Phone> _phones = [];

        [Required(ErrorMessage = "حقل مطلوب")]
        public Clan Clan
        {
            get => _clan;
            set
            {
                if (_clan is null || value is null || _clan.Id != value.Id)
                {
                    SetProperty(ref _clan, value, true);
                    OnPropertyChanged(nameof(IsValid));
                    OnPropertyChanged(nameof(HasClan));
                }
            }
        }

        private Clan _clan;

        public Branch Branch
        {
            get => _branch;
            set
            {
                if (_branch is null || value is null || _branch.Id != value.Id)
                {
                    SetProperty(ref _branch, value, true);
                    OnPropertyChanged(nameof(HasBranch));
                }
            }
        }

        private Branch _branch;
    }
}