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
            await Dispatcher.InvokeAsync(() => _viewModel.LoadDataAsync(false));
        }

        private readonly ViewModel _viewModel;
    }
}
