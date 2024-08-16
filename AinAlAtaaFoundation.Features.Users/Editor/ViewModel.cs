using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Users.Editor
{
    internal abstract partial class ViewModel : ViewModelBase<UserDataModel>
    {
        public ViewModel(IMediator mediator, IMessenger messenger, User user) : base(mediator, messenger)
        {
            DataModel = new UserDataModel(user);

            HasChangesObject = new Shared.Helpers.HasChangesObject(SaveCommand.NotifyCanExecuteChanged);
            DataModel.PropertyChanged += DataModel_PropertyChanged;
        }

        [ObservableProperty]
        private bool _showPassword;

        public override bool CanSave() => HasChangesObject.HasChanges && DataModel.IsValid;

        public override Task LoadDataAsync()
        {
            throw new System.NotImplementedException();
        }

        public bool IsUpdate { get; protected set; } = false;
    }
}
