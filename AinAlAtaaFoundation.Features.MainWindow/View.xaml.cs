using System.Windows;

namespace AinAlAtaaFoundation.Features.MainWindow
{
    public partial class View : Window
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
