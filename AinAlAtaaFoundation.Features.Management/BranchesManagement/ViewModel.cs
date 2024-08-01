using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchesManagement
{
    internal partial class ViewModel : ObservableObject
    {
        public ViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task LoadDataAsync()
        {
            Branches = await _mediator.Send(new Generic.GetAllCommand<Branch>());
        }

        [RelayCommand]
        private void ShowCreate()
        {
            _mediator.Send(new Shared.Commands.Generic.ShowCreate<Branch>());
        }

        [RelayCommand]
        private void ShowUpdate(Branch branch)
        {
            _mediator.Send(new Shared.Commands.Generic.ShowUpdate<Branch>(branch));
        }

        public IEnumerable<Branch> Branches { get => _branches; private set => SetProperty(ref _branches, value); }
        private IEnumerable<Branch> _branches;

        private readonly IMediator _mediator;
    }
}
