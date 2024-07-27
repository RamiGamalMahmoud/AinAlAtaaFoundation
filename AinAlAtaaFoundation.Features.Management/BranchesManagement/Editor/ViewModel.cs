using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchesManagement.Editor
{
    internal partial class ViewModel : ViewModelBase<BranchDataModel>
    {
        public ViewModel(IMediator mediator, IMessenger messenger, Branch branch) : base(mediator, messenger)
        {
            DataModel = new BranchDataModel(branch);
            HasChangesObject = new Shared.Helpers.HasChangesObject(SaveCommand.NotifyCanExecuteChanged);
            DataModel.PropertyChanged += DataModel_PropertyChanged;

            _editType = branch is null ? EditType.Create : EditType.Update;
        }

        public override bool CanSave() => HasChangesObject.HasChanges && DataModel.IsValid;

        public override async Task LoadDataAsync()
        {
            Clans = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Clan>());
        }

        protected override async Task Save()
        {
            if (_editType == EditType.Create)
            {
                Branch branch = await _mediator.Send(new CommandHandlers.CommandCreate(DataModel));
                if (branch is null)
                {
                    _messenger.Send(new Notifications.FailerNotification("خطأ في عملية الحفظ"));
                    return;
                }
                _messenger.Send(new Messages.EntityCreated<Branch>(branch));
                _messenger.Send(new Notifications.SuccessNotification("تم الحفظ بنجاح"));
            }
            else if (_editType == EditType.Update)
            {
                if(!(await _mediator.Send(new CommandHandlers.CommandUpdate(DataModel))))
                {
                    _messenger.Send(new Notifications.FailerNotification("خطأ في عملية الحفظ"));
                    return;
                }
                _messenger.Send(new Notifications.SuccessNotification("تم الحفظ بنجاح"));
                _messenger.Send(new Messages.EntityUpdated<Branch>(DataModel.Model));
            }
        }

        [ObservableProperty]
        private IEnumerable<Clan> _clans;

        private readonly EditType _editType;
    }
}
