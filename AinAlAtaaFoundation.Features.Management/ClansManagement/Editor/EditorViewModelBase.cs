using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.ClansManagement.Editor
{
    internal class EditorViewModelBase : ViewModelBase<ClanDataModel>
    {
        public EditorViewModelBase(IMediator mediator, IMessenger messenger, Clan clan) : base(mediator, messenger)
        {
            _isCreate = clan is null;
            DataModel = new ClanDataModel(clan);
            HasChangesObject = new Shared.Helpers.HasChangesObject(SaveCommand.NotifyCanExecuteChanged);
            DataModel.PropertyChanged += DataModel_PropertyChanged;
        }

        public override bool CanSave() => DataModel.IsValid;

        public override Task LoadDataAsync()
        {
            return Task.CompletedTask;
        }

        protected override async Task Save()
        {
            if (_isCreate)
            {
                Clan clan = await _mediator.Send(new CommandHandlerCreate.Command(DataModel));

                if (clan is null)
                {
                    _messenger.Send(new Shared.Notifications.FailerNotification("خطأ"));
                }
                else
                {
                    _messenger.Send(new Shared.Messages.EntityCreated<Clan>(DataModel.Model));
                    _messenger.Send(new Shared.Notifications.SuccessNotification("تم الحفظ بنجاح"));
                    DataModel.Name = "";
                }
            }
            else
            {
                if (await _mediator.Send(new CommandHandlerUpdate.Command(DataModel)))
                {
                    _messenger.Send(new Shared.Messages.EntityUpdated<Clan>(DataModel.Model));
                    _messenger.Send(new Shared.Notifications.SuccessNotification("تم الحفظ بنجاح"));
                }
                else
                {
                    _messenger.Send(new Shared.Notifications.FailerNotification("خطأ"));
                }
            }
        }

        private bool _isCreate;
    }
}
