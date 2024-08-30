using AinAlAtaaFoundation.Shared.Abstraction.Settings;
using System.Windows;

namespace AinAlAtaaFoundation.Features.Settings
{
    internal partial class View : Window, ISettingsView
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
