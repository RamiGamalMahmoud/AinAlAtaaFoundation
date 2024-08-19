using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Models
{
    [ObservableObject]
    public partial class District : ModelBase
    {
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        private string _name;
    }
}
