using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using AinAlAtaaFoundation.Shared.Commands;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement
{
    internal partial class ViewModel : ObservableObject
    {
        public ViewModel(IMediator mediator, IMessenger messenger)
        {
            _mediator = mediator;

            messenger.Register<Messages.EntityCreated<BranchRepresentative>>(this, async (r, m) =>
            {
                BranchRepresentatives = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<BranchRepresentative>(true));
            });
        }

        public DoBusyWorkObject DoBusyWorkObject => new DoBusyWorkObject();

        public async Task LoadDataAsync()
        {
            using (DoBusyWorkFactory.CreateBusyWork(DoBusyWorkObject))
            {
                BranchRepresentatives = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<BranchRepresentative>());
            }
        }

        [RelayCommand]
        private async Task ShowCreate()
        {
            await _mediator.Send(new BranchRepresentatives.ShowCreate());
        }

        [RelayCommand]
        private async Task ShowUpdate(BranchRepresentative branchRepresentative)
        {
            await _mediator.Send(new BranchRepresentatives.ShowUpdate(branchRepresentative));
        }

        public IEnumerable<BranchRepresentative> BranchRepresentatives { get => _branchRepresentatives; set => SetProperty(ref _branchRepresentatives, value); }
        private IEnumerable<BranchRepresentative> _branchRepresentatives;

        private readonly IMediator _mediator;
    }
}
