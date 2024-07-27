using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members
{
    internal partial class ViewModel(IMediator mediator) : ObservableObject
    {
        public async Task LoadDataAsync()
        {
            _allFamilyMembers = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<FamilyMember>());
            FamilyMembers = _allFamilyMembers;
        }

        partial void OnSearchValueChanged(string oldValue, string newValue)
        {
            FamilyMembers = _allFamilyMembers.Where(x => x.Name.Contains(newValue) || x.Family.Clan.Name.Contains(newValue));
        }

        [RelayCommand]
        private void ShowCreate()
        {
            _mediator.Send(new Shared.Commands.FamilyMembers.CommandShowCreate());
        }

        [RelayCommand]
        private void ShowUpdate(FamilyMember familyMember)
        {
            _mediator.Send(new Shared.Commands.FamilyMembers.CommandShowUpdate(familyMember));
        }

        [ObservableProperty]
        private string _searchValue;

        public IEnumerable<FamilyMember> FamilyMembers
        {
            get => _familyMembers;
            private set => SetProperty(ref _familyMembers, value);
        }
        private IEnumerable<FamilyMember> _familyMembers;
        private IEnumerable<FamilyMember> _allFamilyMembers;
        private readonly IMediator _mediator = mediator;
    }
}
