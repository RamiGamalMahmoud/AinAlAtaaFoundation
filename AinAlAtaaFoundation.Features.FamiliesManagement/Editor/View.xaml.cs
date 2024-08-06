using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Editor
{
    internal partial class View : Window
    {
        public View(EditoViewModelBase viewModel, IMessenger messenger)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;
            Loaded += View_Loaded;

            PreviewKeyDown += (s, e) => { if (e.Key == System.Windows.Input.Key.Escape) Close(); };

            messenger.Register<Shared.Messages.EntityUpdated<Family>>(this, (r, m) => Close());
            messenger.Register<Shared.Messages.EntityCreated<Family>>(this, (r, m) => Close());
        }

        private async void View_Loaded(object sender, RoutedEventArgs e)
        {
            await Dispatcher.Invoke(_viewModel.LoadDataAsync);
        }

        private readonly EditoViewModelBase _viewModel;
    }
}
