using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace AinAlAtaaFoundation.Features.Settings
{
    internal partial class ViewModel : ObservableObject, IAppState
    {
        public ViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            messenger.Register<Messages.LoginSuccededMessage>(this, (r, m) =>
            {
                User = m.User;
            });
        }
        public User User { get => _user; private set => SetProperty(ref _user, value); }
        private User _user;

        private readonly IMessenger _messenger;
    }
}
