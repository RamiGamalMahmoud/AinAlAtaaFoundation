using AinAlAtaaFoundation.Shared.Abstraction;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.Management.ClansManagement
{
    internal partial class View : UserControl, IClansView
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;
            Loaded += View_Loaded;
        }

        private async void View_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!_isLoaded)
            {
                await Dispatcher.InvokeAsync(_viewModel.LoadDataAsync);
                _isLoaded = true;
            }
        }

        private readonly ViewModel _viewModel;
        private bool _isLoaded;
    }
}
