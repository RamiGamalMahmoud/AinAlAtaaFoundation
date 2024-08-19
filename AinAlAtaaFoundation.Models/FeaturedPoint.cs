using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Models
{
    [ObservableObject]
    public partial class FeaturedPoint : ModelBase
    {
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        private string _name;

        public int DistrictId { get; set; }
        public District District { get => _district; set => SetProperty(ref _district, value); }
        private District _district;
    }
}
