using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Models
{
    public class Phone : ObservableObject
    {
        public int Id { get; set; }
        public string Number { get => _number; set => SetProperty(ref _number, value); }
        private string _number;
    }
}
