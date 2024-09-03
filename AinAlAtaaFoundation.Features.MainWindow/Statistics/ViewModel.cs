using AinAlAtaaFoundation.Data;
using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AinAlAtaaFoundation.Features.MainWindow.Statistics
{
    internal partial class ViewModel(IMediator mediator, IAppDbContextFactory appDbContextFactory) : ObservableObject
    {
        public async Task InitAsync()
        {
            Families = await mediator.Send(new Shared.Commands.Generic.GetAllCommand<Family>());

            TotalFamilies = Families.Count();
            Types = Families.GroupBy(x => x.FamilyType).Count();
            FamilyTypes = Families.GroupBy(x => x.FamilyType).Select(x => new Item(x.Key.Name, x.Count()));
            SocialStatuses = Families.GroupBy(x => x.SocialStatus).Select(x => new Item(x.Key.Name, x.Count()));
            Sponsored = Families.GroupBy(x => x.IsSponsored).Select(x => new Item(x.Key == true ? "مكفول" : "غير مكفول", x.Count()));

            BranchRepresentatives = Families.GroupBy(x => x.BranchRepresentative.Name).Select(x => new Item(x.Key, x.Count()));
            Clans = Families.GroupBy(x => x.Clan.Name).Select(x => new Item(x.Key, x.Count()));

            using (AppDbContext dbContext = appDbContextFactory.CreateDbContext())
            {
                TotalClans = await dbContext.Clans.CountAsync();
                TotalDistricts = await dbContext.Districts.CountAsync();
                Members = (await dbContext.FamilyMembers.GroupBy(x => x.Gender.NameAr).ToListAsync()).Select(x => new Item(x.Key, x.Count()));
                TotalDisbursements = await dbContext.Disbursements.CountAsync();
                TotalTodayDisbursements = await dbContext.Disbursements.Where(x => x.Date.Date == DateTime.Now.Date).CountAsync();
                Disbursements =
                [
                    new Item("إجمالي الصرف", TotalDisbursements),
                    new Item("إجمالي الصرف اليوم", TotalTodayDisbursements)
                ];
            }
        }

        public int TotalFamilies
        {
            get => _totalFamilies;
            set => SetProperty(ref _totalFamilies, value);
        }
        private int _totalFamilies;

        [ObservableProperty]
        private int _totalClans;

        [ObservableProperty]
        private int _totalDistricts;

        [ObservableProperty]
        private int _types;

        [ObservableProperty]
        private IEnumerable<Family> _families;

        [ObservableProperty]
        private IEnumerable<Item> _familyTypes;

        [ObservableProperty]
        private IEnumerable<Item> _socialStatuses;

        [ObservableProperty]
        private IEnumerable<Item> _sponsored;

        [ObservableProperty]
        private IEnumerable<Item> _members;

        [ObservableProperty]
        private int _totalDisbursements;

        [ObservableProperty]
        private int _totalTodayDisbursements;

        [ObservableProperty]
        private IEnumerable<Item> _disbursements;

        [ObservableProperty]
        private IEnumerable<Item> _branchRepresentatives;

        [ObservableProperty]
        private IEnumerable<Item> _clans;
    }

    public record Item(string Name, int Value);
}
