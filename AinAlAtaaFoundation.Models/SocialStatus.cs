using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Models
{
    public partial class SocialStatus : ObservableObject
    {
        public int Id { get; set; }

        [ObservableProperty]
        private string _name;
    }
}
