using AinAlAtaaFoundation.Shared.Abstraction;
using System.Windows;

namespace AinAlAtaaFoundation.Features.Auth.Login
{
    internal partial class View : Window, ILoginView
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = (sender as HandyControl.Controls.PasswordBox).Password;
        }

        private readonly ViewModel _viewModel;
    }
}
