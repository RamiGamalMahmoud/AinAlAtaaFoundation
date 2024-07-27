using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictsManagement.Editor
{
    internal class ViewModel : ViewModelBase<DistrictDataModel>
    {
        public ViewModel(IMediator mediator, IMessenger messenger, District district) : base(mediator, messenger)
        {
            DataModel = new DistrictDataModel(district);
            HasChangesObject = new Shared.Helpers.HasChangesObject(SaveCommand.NotifyCanExecuteChanged);
            DataModel.PropertyChanged += DataModel_PropertyChanged;

            _editType = district is null ? EditType.Create : EditType.Update;
        }

        public override bool CanSave() => HasChangesObject.HasChanges && DataModel.IsValid;

        public override Task LoadDataAsync()
        {
            return Task.CompletedTask;
        }

        protected override async Task Save()
        {
            if (_editType == EditType.Create)
            {
                District district = await _mediator.Send(new CommandHandlers.Create.Command(DataModel));
                if(district is null)
                {
                    _messenger.Send(new Notifications.FailerNotification("فشل في الحفظ , بيانات مكررة"));
                    return;
                }

                _messenger.Send(new Messages.EntityCreated<District>(district));
                _messenger.Send(new Notifications.SuccessNotification("تمت الإضافة"));
            }

            else if (_editType == EditType.Update)
            {
                if(!(await _mediator.Send(new CommandHandlers.Update.Command(DataModel))))
                {
                    _messenger.Send(new Notifications.FailerNotification("فشل في الحفظ , بيانات مكررة"));
                    return;
                }
                _messenger.Send(new Notifications.SuccessNotification("تم حقظ التعديل"));

            }
        }

        private readonly EditType _editType;
    }
}
