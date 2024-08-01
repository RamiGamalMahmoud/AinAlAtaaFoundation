using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;

namespace AinAlAtaaFoundation.Features.Management.BranchesManagement.Editor
{
    internal partial class View : Window
    {
        public View(ViewModel viewModel, IMessenger messenger)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;

            Loaded += View_Loaded;

            messenger.Register<Shared.Messages.EntityCreated<Branch>>(this, (r, m) => Close());
            messenger.Register<Shared.Messages.EntityUpdated<Branch>>(this, (r, m) => Close());
        }

        private async void View_Loaded(object sender, RoutedEventArgs e)
        {
            await Dispatcher.InvokeAsync(_viewModel.LoadDataAsync);
        }

        private readonly ViewModel _viewModel;
    }
}
