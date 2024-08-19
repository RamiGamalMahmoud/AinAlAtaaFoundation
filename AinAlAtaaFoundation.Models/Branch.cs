using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AinAlAtaaFoundation.Models
{
    [ObservableObject]
    public partial class Branch : ModelBase
    {
        public Branch()
        {
            BranchRepresentatives = new Collection<BranchRepresentative>();
        }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        private string _name;

        public int ClanId { get; set; }

        public Clan Clan { get => _clan; set => SetProperty(ref _clan, value); }
        private Clan _clan;

        public ICollection<BranchRepresentative> BranchRepresentatives { get; }

    }
}
