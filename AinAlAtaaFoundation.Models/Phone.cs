using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Models
{
    [ObservableObject]
    public partial class Phone : ModelBase
    {
        public string Number { get => _number; set => SetProperty(ref _number, value); }
        private string _number;
    }
}
