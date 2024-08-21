using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.Create
{
    internal partial class View : UserControl, IDisbursementView
    {
        private readonly ViewModel _viewModel;

        public View(ViewModel viewModel, IMessenger messenger)
        {
            InitializeComponent();

            DataContext = viewModel;
            Loaded += View_Loaded;
            _viewModel = viewModel;

            messenger.Register<Messages.MessageInputFinished>(this, (r, m) =>
            {
                _currentInputBox.Focus();
                _currentInputBox.SelectAll();
            });

            messenger.Register<Messages.MessageManualInputChanged>(this, (r, m) =>
            {
                _currentInputBox.Focus();
                _currentInputBox.SelectAll();
            });
        }

        private async void View_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            MainInput.Focus();
            MainInput.SelectAll();
            await Dispatcher.Invoke(_viewModel.LoadDataAsync);
        }

        private void ClearAll()
        {
            RationCardInput.Text = "";
            MainInput.Text = "";
        }

        private void Input_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if(sender is TextBox)
            {
                _currentInputBox = sender as TextBox;
                _currentInputBox.SelectAll();
            }
            else
            {
                _currentInputBox = null;
            }
        }

        private TextBox _currentInputBox;
    }
}
