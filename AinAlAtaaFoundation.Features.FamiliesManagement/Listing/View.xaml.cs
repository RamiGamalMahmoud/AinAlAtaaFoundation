using AinAlAtaaFoundation.Shared.Abstraction;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Listing
{
    internal partial class View : UserControl, IFamiliesView
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            Loaded += View_Loaded;
            _viewModel = viewModel;
        }

        private async void View_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!_isLoaded)
            {
                await Dispatcher.Invoke(() => _viewModel.LoadDataAsync(false));
                _isLoaded = true;
            }
        }
        private bool _isLoaded;
        private readonly ViewModel _viewModel;

        private void DataGrid_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            (sender as DataGrid).SelectedItem = null;
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }
    }
}
