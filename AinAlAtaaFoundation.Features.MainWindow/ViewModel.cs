using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace AinAlAtaaFoundation.Features.MainWindow
{
    public partial class ViewModel : ObservableObject
    {
        public ViewModel(IServiceProvider serviceProvider, IMessenger messenger, IAppState appState)
        {
            _serviceProvider = serviceProvider;
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
        private readonly IMessenger _messenger;

        [RelayCommand]
        private void GoToHome()
        {
            CurrentView = _serviceProvider.GetRequiredService<WelcomeView>();
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
        private void Logout()
        {
            _messenger.Send(new Shared.Commands.Generic.CommandLogout());
        }
    }
}
