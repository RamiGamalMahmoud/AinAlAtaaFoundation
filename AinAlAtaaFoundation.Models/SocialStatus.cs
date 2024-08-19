using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Models
{
    [ObservableObject]
    public partial class SocialStatus : ModelBase
    {
        [ObservableProperty]
        private string _name;
    }
}
