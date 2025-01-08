using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.AssociationRepresentatives.Editor
{
    internal abstract class ViewModel : ViewModelBase<AssociationRepresentativeDataModel>
    {
        public ViewModel(IMediator mediator, IMessenger messenger, AssociationRepresentative model) : base(mediator, messenger)
        {
            DataModel = new AssociationRepresentativeDataModel(model);
            HasChangesObject = new Shared.Helpers.HasChangesObject(SaveCommand.NotifyCanExecuteChanged);
            DataModel.PropertyChanged += DataModel_PropertyChanged;
        }

        public override bool CanSave() => HasChangesObject.HasChanges && DataModel.IsValid;


        public override Task LoadDataAsync()
        {
            throw new NotImplementedException();
        }
    }
}
