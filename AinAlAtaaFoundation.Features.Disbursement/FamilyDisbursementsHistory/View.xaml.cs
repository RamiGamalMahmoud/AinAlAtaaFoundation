using System.Windows;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.FamilyDisbursementsHistory
{
    internal partial class View : Window
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;
            Loaded += View_Loaded;
        }

        private async void View_Loaded(object sender, RoutedEventArgs e)
        {
            await Dispatcher.InvokeAsync(_viewModel.LoadDataAsync);
        }

        private readonly ViewModel _viewModel;

        private void Pagination_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            _viewModel.NextPageCommand.Execute(e.Info - 1);
        }
    }
}
