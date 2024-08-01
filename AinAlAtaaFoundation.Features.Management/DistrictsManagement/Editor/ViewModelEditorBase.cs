using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.DistrictsManagement.Editor
{
    internal abstract class ViewModelEditorBase : ViewModelBase<DistrictDataModel>
    {
        public ViewModelEditorBase(IMediator mediator, IMessenger messenger, District district) : base(mediator, messenger)
        {
            DataModel = new DistrictDataModel(district);
            HasChangesObject = new Shared.Helpers.HasChangesObject(SaveCommand.NotifyCanExecuteChanged);
            DataModel.PropertyChanged += DataModel_PropertyChanged;
        }

        public override bool CanSave() => HasChangesObject.HasChanges && DataModel.IsValid;

        public override Task LoadDataAsync()
        {
            return Task.CompletedTask;
        }
    }
}
