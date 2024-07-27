using CommunityToolkit.Mvvm.Messaging;
using System.Windows;

namespace AinAlAtaaFoundation.Features.Management.DistrictsManagement.Editor
{
    internal partial class View : Window
    {
        public View(ViewModel viewModel, IMessenger messenger)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
