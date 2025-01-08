using System.Windows;

namespace AinAlAtaaFoundation.Features.Management.AssociationRepresentatives.Editor
{
    internal partial class View : Window
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
