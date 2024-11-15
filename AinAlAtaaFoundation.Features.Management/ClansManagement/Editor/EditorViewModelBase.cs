﻿using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.ClansManagement.Editor
{
    internal abstract class EditorViewModelBase : ViewModelBase<ClanDataModel>
    {
        public EditorViewModelBase(IMediator mediator, IMessenger messenger, Clan clan) : base(mediator, messenger)
        {
            DataModel = new ClanDataModel(clan);
            HasChangesObject = new Shared.Helpers.HasChangesObject(SaveCommand.NotifyCanExecuteChanged);
            DataModel.PropertyChanged += DataModel_PropertyChanged;
        }

        public override bool CanSave() => DataModel.IsValid;

        public override Task LoadDataAsync()
        {
            return Task.CompletedTask;
        }
    }
}
