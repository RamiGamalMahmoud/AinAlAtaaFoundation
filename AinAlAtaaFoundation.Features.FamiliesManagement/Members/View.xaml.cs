using AinAlAtaaFoundation.Shared.Abstraction;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members
{
    internal partial class View : UserControl, IFamilyMembersView
    {
        private readonly ViewModel _vewModel;

        public View(ViewModel vewModel)
        {
            InitializeComponent();
            DataContext = vewModel;
            _vewModel = vewModel;
            Loaded += View_Loaded;
        }

        private async void View_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await Dispatcher.InvokeAsync(() => _vewModel.LoadDataAsync());
        }
    }
}
