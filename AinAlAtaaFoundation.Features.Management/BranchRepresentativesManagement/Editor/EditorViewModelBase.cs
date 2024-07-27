using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using AinAlAtaaFoundation.Shared.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement.Editor
{
    internal abstract partial class EditorViewModelBase : ViewModelBase<BranchRepresentativeDataModel>
    {
        protected EditorViewModelBase(IMediator mediator, IMessenger messenger, BranchRepresentative branchRepresentative = null) : base(mediator, messenger)
        {
            DataModel = new BranchRepresentativeDataModel(branchRepresentative);
            HasChangesObject = new HasChangesObject(SaveCommand.NotifyCanExecuteChanged);
            DataModel.PropertyChanged += DataModel_PropertyChanged;
        }

        protected override async void DataModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(DataModel.Clan))
            {
                Branches = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Branch>()))
                    .Where(x => x.Clan.Id == DataModel.Clan.Id);
                HasBranches = Branches.Any();
            }
            base.DataModel_PropertyChanged(sender, e);
        }

        public override async Task LoadDataAsync()
        {
            Districts = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<District>());
            Clans = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Clan>());
        }

        [RelayCommand(CanExecute = nameof(CanAddPhoneNumber))]
        private void AddPhoneNumber()
        {
            DataModel.Phones.Add(new Phone() { Number = PhoneNumber });
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

        public override bool CanSave() => DataModel.IsValid;

        [ObservableProperty]
        private IEnumerable<Branch> _branches;

        [ObservableProperty]
        private IEnumerable<District> _districts;

        [ObservableProperty]
        private IEnumerable<Clan> _clans;

        [ObservableProperty]
        private bool _hasBranches;
        public IEnumerable<Address> Addresses { get; }
    }
}
