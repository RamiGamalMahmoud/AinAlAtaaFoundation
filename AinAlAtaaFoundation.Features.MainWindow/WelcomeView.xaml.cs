using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.IO;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.MainWindow
{
    [ObservableObject]
    public partial class WelcomeView : UserControl
    {
        public WelcomeView(IMessenger messenger)
        {
            InitializeComponent();
            DataContext = this;
            _messenger = messenger;
        }

        [RelayCommand]
        private void GoToManagement()
        {
            _messenger.Send(new Shared.Commands.Generic.GoToManagementCommand());
        }


        [RelayCommand]
        private void GoToDisbursement()
        {
            _messenger.Send(new Shared.Commands.Generic.GoToDisbursementCommand());
        }

        public static string CurrentDate => DateTime.Now.ToString("yyyy-MM-dd");
        public static string LogoPath => Path.Combine(Environment.CurrentDirectory, "Images\\logo-200.png");

        private readonly IMessenger _messenger;

    }
}
