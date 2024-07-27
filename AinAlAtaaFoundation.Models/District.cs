using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Models
{
    public class District : ObservableObject
    {
        public int Id { get; set; }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        private string _name;
    }
}
