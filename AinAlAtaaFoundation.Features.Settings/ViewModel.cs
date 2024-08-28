using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.ComponentModel;
using System.IO;

namespace AinAlAtaaFoundation.Features.Settings
{
    internal partial class ViewModel : ObservableObject, IAppState
    {
        public ViewModel(IMessenger messenger, Properties.Settings settings)
        {
            _messenger = messenger;
            _settings = settings;
            settings.Save();
            messenger.Register<ViewModel, Messages.LoginSuccededMessage>(this, (reciver, message) => User = message.User);
            messenger.Register<ViewModel, Messages.SettingsMessages.GetSatrtStatusRequestMessage>(this, (r, m) => m.Reply(r.IsFeshStart));
            messenger.Register<ViewModel, Messages.SettingsMessages.UpdateStartStatusMessage>(this, (r, m) => r.IsFeshStart = m.IsFreshStart);
        }
        public User User
        {
            get => _user;
            private set => SetProperty(ref _user, value);
        }
        private User _user;

        public bool CloseCreateWindow
        {
            get => _settings.CloseCreateWindow;
            set
            {
                _settings.CloseCreateWindow = value;
                OnPropertyChanged(nameof(CloseCreateWindow));
            }
        }

        public bool AutoSave
        {
            get => _settings.AutoSave;
            set
            {
                _settings.AutoSave = value;
                OnPropertyChanged(nameof(AutoSave));
            }
        }

        public bool IsFeshStart
        {
            get => _settings.FreshStart;
            set
            {
                _settings.FreshStart = value;
                _settings.Save();
            }
        }

        public string AppDataFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AinAlAtaaFoundation");

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (AutoSave) SaveCommand.Execute(null);
        }

        [RelayCommand]
        private void Save() => _settings.Save();


        [RelayCommand]
        private void Reset() => _settings.Reset();

        private readonly IMessenger _messenger;
        private readonly Properties.Settings _settings;
    }
}
