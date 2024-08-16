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
        }

        public override bool CanSave() => HasChangesObject.HasChanges && DataModel.IsValid;

        public override async Task LoadDataAsync()
        {
            Districts = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<District>());
        }

        [ObservableProperty]
        IEnumerable<District> _districts;

    }
}
