using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.MainWindow.Statistics
{
    internal partial class View : UserControl
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
            await Dispatcher.InvokeAsync(_viewModel.InitAsync);
        }

        private readonly ViewModel _viewModel;
    }
}
