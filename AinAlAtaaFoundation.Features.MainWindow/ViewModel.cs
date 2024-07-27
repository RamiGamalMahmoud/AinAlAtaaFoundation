using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Notification.Wpf;
using System;
using System.IO;

namespace AinAlAtaaFoundation.Features.MainWindow
{
    public partial class ViewModel : ObservableObject
    {
        public ViewModel(IServiceProvider serviceProvider, IMessenger messenger)
        {
            _serviceProvider = serviceProvider;
            _notificationManager = new NotificationManager();
            _notificationManager.Show("رسالة","مرحبا", NotificationType.Information);
            GoToHome();

            messenger.Register<Shared.Commands.Generic.GoToManagementCommand>(this, (reciver, message) => GoToManagement());
            messenger.Register<Shared.Commands.Generic.GoToDisbursementCommand>(this, (reciver, message) => GoToDisbursement());
            messenger.Register<Shared.Commands.Generic.GoToHomeCommand>(this, (reciver, message) => GoToHome());

            messenger.Register<Shared.Notifications.SuccessNotification>(this, (r, m) =>
            {
                _notificationManager.Show("رسالة", m.Message, NotificationType.Success);
            });

            messenger.Register<Shared.Notifications.FailerNotification>(this, (r, m) =>
            {
                _notificationManager.Show("رسالة", m.Message, NotificationType.Error);
            });
        }

        [ObservableProperty]
        private object _currentView;

        public static string LogoPath => Path.Combine(Environment.CurrentDirectory, "Images\\logo-100.png");

        private readonly IServiceProvider _serviceProvider;

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
        private void GoToFamilies()
        {
            CurrentView = _serviceProvider.GetRequiredService<IFamiliesView>();
        }

        [RelayCommand]
        private void GoToFamilyMembers()
        {
            CurrentView = _serviceProvider.GetRequiredService<IFamilyMembersView>();
        }

        private readonly NotificationManager _notificationManager;
    }
}
