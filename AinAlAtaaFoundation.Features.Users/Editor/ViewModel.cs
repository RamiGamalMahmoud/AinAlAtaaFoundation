using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Users.Editor
{
    internal partial class ViewModel : ViewModelBase<UserDataModel>
    {
        public ViewModel(IMediator mediator, IMessenger messenger, User user) : base(mediator, messenger)
        {
            DataModel = new UserDataModel(user);

            _editType = user is null ? EditType.Create : EditType.Update;

            if(!IsUpdate)
            {
                ShowPassword = true;
            }

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

        protected override async Task Save()
        {
            if(_editType == EditType.Create)
            {
                User user = await _mediator.Send(new Shared.Commands.Generic.CommandCreate<User, UserDataModel>(DataModel));
                if(user is not null)
                {
                    _messenger.Send(new Shared.Notifications.SuccessNotification("تم الحفظ"));
                    _messenger.Send(new Shared.Messages.EntityCreated<User>(user));

                    DataModel.ClearInputs();

                }
                else _messenger.Send(new Shared.Notifications.FailerNotification("فشل في عملية التعديل , بيانات مكررة"));
            }
            else if(_editType == EditType.Update)
            {
                try
                {
                    if(await _mediator.Send(new Shared.Commands.Generic.CommandUpdate<UserDataModel>(DataModel)))
                    {
                        _messenger.Send(new Shared.Notifications.SuccessNotification("تم الحفظ"));
                        _messenger.Send(new Shared.Messages.EntityUpdated<User>(DataModel.Model));
                    }
                    else _messenger.Send(new Shared.Notifications.FailerNotification("فشل في عملية التعديل , بيانات مكررة"));
                }
                catch (Shared.Exceptions.AdminsCountException)
                {
                    _messenger.Send(new Shared.Notifications.FailerNotification("يجب أن يكون هناك مسؤل واحد على الأقل"));
                    DataModel.IsAdmin = true;
                }
            }
        }

        public bool IsUpdate => _editType == EditType.Update;

        private readonly EditType _editType = EditType.Create;

    }
}
