using AinAlAtaaFoundation.Shared.Abstraction;
using AinAlAtaaFoundation.Shared.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.IO;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.MainWindow
{
    [ObservableObject]
    public partial class WelcomeView : UserControl
    {
        public WelcomeView(IAppState appState)
        {
            InitializeComponent();
            DataContext = this;
            AppState = appState;
        }

        public static Clock Clock => new Clock();

        public static string CurrentDate => DateTime.Now.ToString("yyyy-MM-dd");
        public static string LogoPath => Path.Combine(Environment.CurrentDirectory, "Images\\logo-200.png");

        public IAppState AppState { get; }
    }
}
