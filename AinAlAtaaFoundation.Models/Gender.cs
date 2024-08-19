using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Models
{
    [ObservableObject]
    public partial class Gender : ModelBase
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
    }
}
