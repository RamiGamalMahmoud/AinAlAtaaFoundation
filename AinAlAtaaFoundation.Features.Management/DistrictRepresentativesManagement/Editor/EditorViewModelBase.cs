using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using AinAlAtaaFoundation.Shared.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement.Editor
{
    internal abstract partial class EditorViewModelBase : ViewModelBase<DistrictRepresentativeDataModel>
    {

        protected EditorViewModelBase(IMediator mediator, IMessenger messenger, DistrictRepresentative districtRepresentative = null) : base(mediator, messenger)
        {
            DataModel = new DistrictRepresentativeDataModel(districtRepresentative);
            HasChangesObject = new HasChangesObject(SaveCommand.NotifyCanExecuteChanged);
            DataModel.PropertyChanged += DataModel_PropertyChanged;
            DataModel.Phones.CollectionChanged += Phones_CollectionChanged;
        }

        private void Phones_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            HasChangesObject.SetHaschanges();
        }

        public override async Task LoadDataAsync()
        {
            Districts = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<District>());
        }

        public override bool CanSave() => DataModel.IsValid;


        [RelayCommand(CanExecute = nameof(CanAddPhoneNumber))]
        private void AddPhoneNumber()
        {
            DataModel.Phones.Add(new Phone() { Number = PhoneNumber});
            PhoneNumber = "";
        }

        [RelayCommand]
        private void RemovePhone(Phone phone)
        {
            DataModel.Phones.Remove(phone);
        }

        private bool CanAddPhoneNumber() => !string.IsNullOrEmpty(PhoneNumber);

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddPhoneNumberCommand))]
        private string _phoneNumber;

        public IEnumerable<District> Districts { get => _districts; private set => SetProperty(ref _districts, value); }
        private IEnumerable<District> _districts;
        public IEnumerable<Address> Addresses { get; }
    }
}
