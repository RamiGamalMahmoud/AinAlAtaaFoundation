using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.SocialStatuses.Editor
{
    internal abstract class ViewModel : ViewModelBase<SocialStatusDataModel>
    {
        public ViewModel(IMediator mediator, IMessenger messenger, SocialStatus model) : base(mediator, messenger)
        {
            DataModel = new SocialStatusDataModel(model);
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
