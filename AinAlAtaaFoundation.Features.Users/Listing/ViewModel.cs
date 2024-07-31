using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using MediatR;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.Users.Listing
{
    internal partial class ViewModel : ObservableObject
    {
        public ViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task LoadDataAsync()
        {
            Users = await _mediator.Send(new Shared.Commands.Generic.GetAllCommand<User>());
        }

        [ObservableProperty]
        private IEnumerable<User> _users;
        private readonly IMediator _mediator;
    }
}
