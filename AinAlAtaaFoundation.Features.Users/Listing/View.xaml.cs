using AinAlAtaaFoundation.Shared.Abstraction;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.Users.Listing
{
    internal partial class View : UserControl, IUserView
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;
            Loaded += View_Loaded;
        }

        private async void View_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!_isLoaded)
            {
                await Dispatcher.Invoke(_viewModel.LoadDataAsync);
                _isLoaded = true;
            }
        }

        private readonly ViewModel _viewModel;
        private bool _isLoaded;
    }
}
