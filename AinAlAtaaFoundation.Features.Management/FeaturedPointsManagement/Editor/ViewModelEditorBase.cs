using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using AinAlAtaaFoundation.Shared.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.FeaturedPointsManagement.Editor
{
    internal abstract partial class ViewModelEditorBase : ViewModelBase<FeaturedPointDataModel>
    {
        public ViewModelEditorBase(IMediator mediator, IMessenger messenger, FeaturedPoint featuredPoint) : base(mediator, messenger)
        {
            DataModel = new FeaturedPointDataModel(featuredPoint);
            HasChangesObject = new Shared.Helpers.HasChangesObject(SaveCommand.NotifyCanExecuteChanged);
            DataModel.PropertyChanged += DataModel_PropertyChanged;
            _editType = featuredPoint is null ? EditType.Create : EditType.Update;
        }

        public override bool CanSave() => HasChangesObject.HasChanges && DataModel.IsValid;

        public override async Task LoadDataAsync()
        {
            Districts = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<District>());
        }

        protected override async Task Save()
        {
            if (_editType is EditType.Create)
            {
                FeaturedPoint featuredPoint = await _mediator.Send(new CommandHandlers.Create.Command(DataModel));
                if (featuredPoint is null)
                {
                    _messenger.Send(new Notifications.FailerNotification("فشل في عملية الحفظ , بيانات مكررة!"));
                    return;
                }
                else
                {
                    _messenger.Send(new Messages.EntityCreated<FeaturedPoint>(featuredPoint));
                    _messenger.Send(new Notifications.SuccessNotification("تمت عملية الحفظ بنجاح"));
                }
            }
            else if (_editType is EditType.Update)
            {
                if (!(await _mediator.Send(new CommandHandlers.Update.Command(DataModel))))
                {
                    _messenger.Send(new Notifications.FailerNotification("فشل في عملية الحفظ , بيانات مكررة!"));
                    return;
                }
                _messenger.Send(new Messages.EntityUpdated<FeaturedPoint>(DataModel.Model));
                _messenger.Send(new Notifications.SuccessNotification("تمت عملية الحفظ بنجاح"));
            }
        }

        [ObservableProperty]
        IEnumerable<District> _districts;

        private readonly EditType _editType;

    }
}
