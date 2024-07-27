using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AinAlAtaaFoundation.Models
{
    public class DistrictRepresentative : ObservableObject
    {
        public DistrictRepresentative()
        {
            Phones = new ObservableCollection<Phone>();    
        }

        public int Id { get; set; }

        public string Name { get => _name; set => SetProperty(ref _name, value); }
        private string _name;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }

        public int DistrictId { get; set; }
        public District District { get => _district; set => SetProperty(ref _district, value); }
        private District _district;

        public int AddressId { get; set; }
        public Address Address { get => _address; set => SetProperty(ref _address, value); }
        private Address _address;

        public ObservableCollection<Phone> Phones { get; }
    }
}
