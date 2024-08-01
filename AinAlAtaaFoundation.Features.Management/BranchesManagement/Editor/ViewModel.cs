using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchesManagement.Editor
{
    internal abstract partial class ViewModel : ViewModelBase<BranchDataModel>
    {
        public ViewModel(IMediator mediator, IMessenger messenger, Branch branch) : base(mediator, messenger)
        {
            DataModel = new BranchDataModel(branch);
            HasChangesObject = new Shared.Helpers.HasChangesObject(SaveCommand.NotifyCanExecuteChanged);
            DataModel.PropertyChanged += DataModel_PropertyChanged;
        }

        public override bool CanSave() => HasChangesObject.HasChanges && DataModel.IsValid;

        public override async Task LoadDataAsync()
        {
            Clans = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Clan>());
        }

        [ObservableProperty]
        private IEnumerable<Clan> _clans;
    }
}
