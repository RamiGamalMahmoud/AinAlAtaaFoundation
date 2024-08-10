using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members.Editor
{
    internal abstract partial class EditorViewModelBase : ViewModelBase<FamilyMemberDataModel>
    {
        public EditorViewModelBase(IMediator mediator, IMessenger messenger, FamilyMember familyMember) : base(mediator, messenger)
        {
            DataModel = new FamilyMemberDataModel(familyMember);
            Family = familyMember?.Family;
            HasChangesObject = new Shared.Helpers.HasChangesObject(SaveCommand.NotifyCanExecuteChanged);
            DataModel.PropertyChanged += DataModel_PropertyChanged;
        }

        protected override void DataModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            HasChangesObject.SetHaschanges();
        }

        async partial void OnClanChanged(Clan oldValue, Clan newValue)
        {
            Families = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Family>()))
                .Where(x => x.Clan?.Id == newValue?.Id);

            Branches = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Branch>()))
                .Where(x => x.Clan?.Id == newValue?.Id);

            //IEnumerable<FamilyMember> members = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<FamilyMember>());
            //if(DataModel.Model is null)
            //{
            //    Mothers = members.Where(x => x.Gender.Id == 2 && x.Family.Clan.Id == Clan.Id);
            //}
            //else Mothers = members.Where(x => x.Gender.Id == 2 && x.Family.Clan.Id == Clan.Id && x.Id != DataModel.Model.Id );
        }

        async partial void OnFamilyChanged(Family oldValue, Family newValue)
        {
            DataModel.Family = newValue;

            IEnumerable<FamilyMember> members = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<FamilyMember>());
            if (DataModel.Model is null)
            {
                Mothers = members.Where(x => x.Gender.Id == 2 && x.Family.Id == newValue.Id);
            }
            else Mothers = members.Where(x => x.Gender.Id == 2 && x.Family.Id == newValue.Id && x.Id != DataModel.Model.Id);
        }


        public override async Task LoadDataAsync()
        {
            Clans = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Clan>());

            Genders = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Gender>());
        }

        [ObservableProperty]
        private Clan _clan;

        [ObservableProperty]
        private Family _family;

        [ObservableProperty]
        private IEnumerable<Family> _families;

        [ObservableProperty]
        private IEnumerable<FamilyMember> _mothers;

        [ObservableProperty]
        private IEnumerable<Gender> _genders;

        [ObservableProperty]
        private IEnumerable<Clan> _clans;

        async partial void OnBranchChanged(Branch oldValue, Branch newValue)
        {
            Families = (await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<Family>()))
                .Where(x =>
                {
                    if(newValue is null)
                    {
                        return x.Clan.Id == Clan?.Id;
                    }
                    return x.Branch?.Id == newValue?.Id;
                });
        }

        [ObservableProperty]
        private IEnumerable<Branch> _branches;
        [ObservableProperty]
        private Branch _branch;

        public override bool CanSave() => !DataModel.HasErrors;
    }
}
