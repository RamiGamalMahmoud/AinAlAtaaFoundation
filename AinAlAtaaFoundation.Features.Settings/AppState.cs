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
    internal partial class AppState : ObservableObject, IAppState
    {
        public AppState(IMessenger messenger, Properties.Settings settings)
        {
            _messenger = messenger;
            _settings = settings;

            messenger.Register<AppState, Messages.LoginSuccededMessage>(this, (reciver, message) => User = message.User);
            messenger.Register<AppState, Messages.SettingsMessages.GetSatrtStatusRequestMessage>(this, (r, m) => m.Reply(r.IsFeshStart));
            messenger.Register<AppState, Messages.SettingsMessages.UpdateStartStatusMessage>(this, (r, m) => r.IsFeshStart = m.IsFreshStart);
            messenger.Register<AppState, Messages.Database.RestoreMessage>(this, (r, m) =>
            {
                _settings.BackupFileToRestore = m.FileName;
                Save();
            });

            messenger.Register<Messages.Database.DatabaseBackedupMessage>(this, (r, m) =>
            {
                _settings.BackupFileToRestore = null;
                Save();
            });

            // clear BackupFileToRestore
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

        public bool HasBackupFileToRestore => !string.IsNullOrEmpty(_settings.BackupFileToRestore);
        public string BackupFileToRestore => _settings.BackupFileToRestore;

        public bool IsFeshStart
        {
            get => _settings.FreshStart;
            set => SetProperty(_settings.FreshStart, value, _settings, (u, n) => u.FreshStart = n);
        }

        #region Printer
        public string RecipePrinter
        {
            get => _settings.RecipePrinter;
            set => SetProperty(_settings.RecipePrinter, value, _settings, (settings, recipeprinter) => settings.RecipePrinter = recipeprinter);
        }

        public string LabelPrinter
        {
            get => _settings.LabelPrinter;
            set => SetProperty(_settings.LabelPrinter, value, _settings, (settings, labelPrinter) => settings.LabelPrinter = labelPrinter);
        }

        public string DefaultPrinter
        {
            get => _settings.DefaultPrinter;
            set => SetProperty(_settings.DefaultPrinter, value, _settings, (settings, defaultPrinter) => settings.DefaultPrinter = defaultPrinter);
        }
        #endregion

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
