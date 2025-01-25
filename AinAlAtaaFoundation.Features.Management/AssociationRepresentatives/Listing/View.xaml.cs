using AinAlAtaaFoundation.Shared.Abstraction;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.Management.AssociationRepresentatives.Listing
{
    internal partial class View : UserControl, IAssociationRepresentativesListingView
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
            await Dispatcher.Invoke(_viewModel.LoadDataAsync);
        }

        private ViewModel _viewModel;
    }
}
