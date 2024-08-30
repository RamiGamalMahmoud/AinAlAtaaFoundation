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

        [RelayCommand]
        private void Backup() => _messenger.Send<Messages.Database.BackupMessage>();

        [RelayCommand]
        private void Restore() => _messenger.Send<Messages.Database.ResoreMessage>();

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
