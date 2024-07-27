using System.Windows;

namespace AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement.Editor
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
            await Dispatcher.InvokeAsync(_viewModel.LoadDataAsync);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
        private readonly EditorViewModelBase _viewModel;
    }
}
