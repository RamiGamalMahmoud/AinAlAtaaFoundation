using AinAlAtaaFoundation.Data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Features.MainWindow.Statistics
{
    internal partial class ViewModel : ObservableObject
    {
        public ViewModel(AppDbContextFactory appDbContextFactory)
        {
            _appDbContextFactory = appDbContextFactory;
        }
        private readonly AppDbContextFactory _appDbContextFactory;
    }
}
