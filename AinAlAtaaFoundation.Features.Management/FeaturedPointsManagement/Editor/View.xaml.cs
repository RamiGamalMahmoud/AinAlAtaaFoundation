using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;

namespace AinAlAtaaFoundation.Features.Management.FeaturedPointsManagement.Editor
{
    internal partial class View : Window
    {
        public View(ViewModelEditorBase viewModel, IMessenger messenger)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;
            Loaded += View_Loaded;

            messenger.Register<Shared.Messages.EntityUpdated<FeaturedPoint>>(this, (r, m) => Close());
            messenger.Register<Shared.Messages.EntityCreated<FeaturedPoint>>(this, (r, m) => Close());
        }

        private async void View_Loaded(object sender, RoutedEventArgs e)
        {
            await Dispatcher.InvokeAsync(_viewModel.LoadDataAsync);
        }

        private readonly ViewModelEditorBase _viewModel;
    }
}
