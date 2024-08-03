using System.Windows;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members.Editor
{
    internal partial class View : Window
    {
        public View(EditorViewModelBase viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;

            Loaded += View_Loaded;
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
