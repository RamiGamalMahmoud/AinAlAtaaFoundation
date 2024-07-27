using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Models
{
    public class Address : ObservableObject
    {
        public int Id { get; set; }
        public string Street { get => _street; set => SetProperty(ref _street, value); }
        private string _street;
        public int DistrictId { get; set; }
        public District District { get => _district; set => SetProperty(ref _district, value); }
        private District _district;

        public int? FeaturedPointId { get; set; }
        public FeaturedPoint FeaturedPoint { get => _featuredPoint; set => SetProperty(ref _featuredPoint, value); }
        private FeaturedPoint _featuredPoint;
    }
}
