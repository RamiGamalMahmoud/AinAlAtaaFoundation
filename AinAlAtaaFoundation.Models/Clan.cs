using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AinAlAtaaFoundation.Models
{
    public class Clan : ObservableObject
    {
        public Clan()
        {
            Branches = new Collection<Branch>();
        }

        public int Id { get; set; }

        public string Name { get => _name; set => SetProperty(ref _name, value); }
        private string _name;

        public ICollection<Branch> Branches { get; }
    }
}
