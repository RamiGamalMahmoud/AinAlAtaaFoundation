using System.Windows;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Editor
{
    internal partial class View : Window
    {
        public View(EditoViewModelBase viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;
            Loaded += View_Loaded;
        }

        private async void View_Loaded(object sender, RoutedEventArgs e)
        {
            await Dispatcher.Invoke(_viewModel.LoadDataAsync);
        }

        private readonly EditoViewModelBase _viewModel;
    }
}
