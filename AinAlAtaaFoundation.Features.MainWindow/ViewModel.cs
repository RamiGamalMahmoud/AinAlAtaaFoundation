using AinAlAtaaFoundation.Models;
using AinAlAtaaFoundation.Shared;
using AinAlAtaaFoundation.Shared.Abstraction;
using AinAlAtaaFoundation.Shared.Abstraction.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.IO;

namespace AinAlAtaaFoundation.Features.MainWindow
{
    public partial class ViewModel : ObservableObject
    {
        public ViewModel(IServiceProvider serviceProvider, IMediator mediator, IMessenger messenger, IAppState appState)
        {
            _serviceProvider = serviceProvider;
            _mediator = mediator;
            _messenger = messenger;
            AppState = appState;
            GoToHome();

            messenger.Register<Shared.Commands.Generic.GoToManagementCommand>(this, (reciver, message) => GoToManagement());
            messenger.Register<Shared.Commands.Generic.GoToDisbursementCommand>(this, (reciver, message) => GoToDisbursement());
            messenger.Register<Shared.Commands.Generic.GoToHomeCommand>(this, (reciver, message) => GoToHome());
        }

        [ObservableProperty]
        private object _currentView;

        public static string LogoPath => Path.Combine(Environment.CurrentDirectory, "Images\\logo-100.png");

        public IAppState AppState { get; }

        private readonly IServiceProvider _serviceProvider;
        private readonly IMediator _mediator;
        private readonly IMessenger _messenger;

        [RelayCommand]
        private void GoToHome()
        {
            CurrentView = _serviceProvider.GetRequiredService<WelcomeView>();
        }

        [RelayCommand]
        private void ShowSettings()
        {
            _serviceProvider.GetRequiredService<ISettingsView>().Show();
        }

        [RelayCommand]
        private void AddFamily()
        {
            _mediator.Send(new Shared.Commands.Generic.ShowCreate<Family>());
        }

        [RelayCommand]
        private void GoToManagement()
        {
            CurrentView = _serviceProvider.GetRequiredService<IManagementView>();
        }

        [RelayCommand]
        private void GoToDisbursement()
        {
            CurrentView = _serviceProvider.GetRequiredService<IDisbursementView>();
        }

        [RelayCommand]
        private void GoToDisbursementHistory()
        {
            CurrentView = _serviceProvider.GetRequiredService<IDisbursementHistoryView>();
        }

        [RelayCommand]
        private void GoToFamilies()
        {
            CurrentView = _serviceProvider.GetRequiredService<IFamiliesView>();
        }

        [RelayCommand]
        private void GoToFamilyMembers()
        {
            CurrentView = _serviceProvider.GetRequiredService<IFamilyMembersView>();
        }

        [RelayCommand]
        private void GoToStatistics()
        {
            CurrentView = _serviceProvider.GetRequiredService<Statistics.View>();
        }

        [RelayCommand]
        private void Logout()
        {
            _messenger.Send(new Shared.Commands.Generic.CommandLogout());
        }

        [RelayCommand]
        private void OpenDirectory(string dirName = "")
        {
            string directory = Path.Combine(_outputDirectory, string.IsNullOrWhiteSpace(dirName) ? "" : dirName);

            if(Directory.Exists(directory))
            {
                Process.Start("explorer", directory);
            }
            else
            {
                _messenger.Send(new Shared.Notifications.FailerNotification(dirName));
                _messenger.Send(new Shared.Notifications.FailerNotification("مسار غير موجود"));
            }
        }

        private static readonly string _outputDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AinAlAtaaFoundation");
    }
}
