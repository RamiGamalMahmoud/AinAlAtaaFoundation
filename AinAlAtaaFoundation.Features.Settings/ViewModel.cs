using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Syncfusion.Data.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AinAlAtaaFoundation.Features.Settings
{
    internal partial class ViewModel : ObservableObject
    {
        public ViewModel(AppState appState, IMessenger messenger)
        {
            AppState = appState;
            _messenger = messenger;
            PrinterSettings printerSettings = new PrinterSettings();

            DefaultPrinter = printerSettings.PrinterName;
            IEnumerable<string> printers = PrinterSettings.InstalledPrinters.ToList<string>();
            _printers = new List<string>(printers) { "Default" };
        }
        public AppState AppState { get; }
        public string DefaultPrinter { get; }

        [ObservableProperty]
        private IEnumerable<string> _printers;

        public IEnumerable<string> Backups
        {
            get
            {
                DatabaseInfo version = _messenger.Send(new Messages.Database.GetCurrentDatabaseVersion()).Response.Result;

                string fileNamePattern = $@"(\d{{4}}_\d{{2}}_\d{{2}}__\d{{2}}_\d{{2}}_\d{{2}}__\d{{{4}}} \[V-{version.Version:00}\])";
                Regex regex = new Regex(fileNamePattern);
                return Directory.EnumerateFiles(Path.Combine(AppState.AppDataFolder, "backups"))
                    .Where(x => regex.Match(x).Success)
                    .Select(x => regex.Match(x).Value);
            }
        }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RestoreCommand))]
        private string _selectedBackup;

        [RelayCommand]
        private void Backup()
        {
            _messenger.Send<Messages.Database.BackupMessage>();
            OnPropertyChanged(nameof(Backups));
        }

        [RelayCommand(CanExecute = nameof(CanRestore))]
        private void Restore()
        {
            bool isOk = _messenger.Send(new Messages.ConfirmRequestMessage("أنت على وشك استعادة نسخة من قاعدة البيانات, هل تريد الإستمرار ؟"));
            if (!isOk) return;

            _messenger.Send(new Shared.Notifications.SuccessNotification("سوف تتم العملية بعد إعادة تشغيل البرنامج"));
            _messenger.Send(new Messages.Database.RestoreMessage(SelectedBackup));
            _messenger.Send(new Messages.Application.RestartMessage());
            SelectedBackup = null;
        }
        private bool CanRestore => !string.IsNullOrEmpty(SelectedBackup);

        [RelayCommand]
        private void ResetDatabase() => _messenger.Send<Messages.Database.ResetMessage>();

        [RelayCommand]
        private void OpenDataFolder()
        {
            Process.Start("explorer.exe", AppState.AppDataFolder);
        }

        private readonly IMessenger _messenger;
    }
}
