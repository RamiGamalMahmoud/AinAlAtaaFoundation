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
        }

        private readonly ViewModel _viewModel;
    }
}
