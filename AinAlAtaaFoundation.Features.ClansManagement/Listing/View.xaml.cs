using AinAlAtaaFoundation.Shared.Abstraction;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.ClansManagement.Listing
{
    internal partial class View : UserControl, IClansView
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;
        }

        private readonly ViewModel _viewModel;
    }
}
