using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AinAlAtaaFoundation.Models
{
    public class BranchRepresentative : ObservableObject
    {
        public BranchRepresentative()
        {
            Phones = new Collection<Phone>();
        }

        public int Id { get; set; }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        private string _name;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }

        public string AllPhones => Phones.Count > 0? Phones.Select(x => x.Number).Aggregate((current, next) => current + ", " + next) : "" ;

        public ICollection<Phone> Phones { get; }

        public int ClanId { get; set; }
        public Clan Clan
        {
            get => _clan;
            set => SetProperty(ref _clan, value);
        }
        private Clan _clan;

        public int? BranchId { get; set; }
        public Branch Branch { get => _branch; set => SetProperty(ref _branch, value); }
        private Branch _branch;
    }
}
