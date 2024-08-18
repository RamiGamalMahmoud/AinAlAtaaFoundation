using AinAlAtaaFoundation.Shared.Abstraction;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.History
{
    internal partial class View : UserControl, IDisbursementHistoryView
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
