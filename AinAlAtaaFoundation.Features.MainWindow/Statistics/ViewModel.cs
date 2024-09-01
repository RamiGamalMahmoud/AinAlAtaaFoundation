using AinAlAtaaFoundation.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.MainWindow.Statistics
{
    internal partial class ViewModel(IAppDbContextFactory appDbContextFactory) : ObservableObject
    {
        public async Task InitAsync()
        {
            using (AppDbContext dbContext = appDbContextFactory.CreateDbContext())
            {
                TotalFamilies = await dbContext.Families.CountAsync();
            }
        }

        public int TotalFamilies
        {
            get => _totalFamilies;
            set => SetProperty(ref _totalFamilies, value);
        }

        private int _totalFamilies;
    }
}
