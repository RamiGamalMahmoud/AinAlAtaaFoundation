using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members.Editor
{
    internal partial class View : Window
    {
        public View(EditorViewModelBase viewModel, IMessenger messenger)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;

            Loaded += View_Loaded;

            messenger.Register<Shared.Messages.EntityUpdated<FamilyMember>>(this, (r, m) => Close());
            messenger.Register<Shared.Messages.EntityCreated<FamilyMember>>(this, (r, m) => Close());
        }

        private async void View_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_isLoaded)
            {
                await Dispatcher.InvokeAsync(_viewModel.LoadDataAsync);
                _isLoaded = true;
            }
        }

        private readonly EditorViewModelBase _viewModel;
        private bool _isLoaded;
    }
}
