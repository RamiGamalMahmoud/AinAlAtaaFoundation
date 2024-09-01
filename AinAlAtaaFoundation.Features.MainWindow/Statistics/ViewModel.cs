using AinAlAtaaFoundation.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Data.Extensions;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.MainWindow.Statistics
{
    internal partial class ViewModel : ObservableObject
    {
        public ViewModel(IAppDbContextFactory appDbContextFactory)
        {
            _appDbContextFactory = appDbContextFactory;
        }

        public async Task InitAsync()
        {
            using (AppDbContext dbContext = _appDbContextFactory.CreateDbContext())
            {
                TotalFamilies = await dbContext.Families.CountAsync();
            }
        }

        public int TotalFamilies
        {
            get => _totalFamilies;
            set => SetProperty(ref _totalFamilies, value);
        }

        private readonly IAppDbContextFactory _appDbContextFactory;
        private int _totalFamilies;
    }
}
