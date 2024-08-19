using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AinAlAtaaFoundation.Models
{
    [ObservableObject]
    public partial class Clan : ModelBase
    {
        public Clan()
        {
            Branches = new Collection<Branch>();
        }

        public string Name { get => _name; set => SetProperty(ref _name, value); }
        private string _name;

        public ICollection<Branch> Branches { get; }

        //public static bool operator ==(Clan left, Clan right) => left.Equals(right);

        //public static bool operator !=(Clan left, Clan right) => !(left == right);

        //public override bool Equals(object obj)
        //{
        //    if (!(obj is Clan clan)) return false;
        //    return Id == clan.Id;
        //}

        //public override int GetHashCode()
        //{
        //    return Id.GetHashCode();
        //}

        //public override opera
    }
}
