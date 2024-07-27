using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members.Editor
{
    internal abstract partial class EditorViewModelBase : ViewModelBase<FamilyMemberDataModel>
    {
        public EditorViewModelBase(IMediator mediator, IMessenger messenger, FamilyMember familyMember) : base(mediator, messenger)
        {
            DataModel = new FamilyMemberDataModel(familyMember);
            HasChangesObject = new Shared.Helpers.HasChangesObject(SaveCommand.NotifyCanExecuteChanged);
            DataModel.PropertyChanged += DataModel_PropertyChanged;
        }

        protected override void DataModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            HasChangesObject.SetHaschanges();
        }

        public override async Task LoadDataAsync()
        {
            Families = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Family>());
            Genders = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Gender>());
        }

        [ObservableProperty]
        private IEnumerable<Family> _families;

        [ObservableProperty]
        private IEnumerable<Gender> _genders;

        public override bool CanSave() => !DataModel.HasErrors;
    }
}
