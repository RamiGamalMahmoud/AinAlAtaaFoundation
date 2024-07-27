using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Models
{
    public partial class User : ObservableObject
    {
        public int Id { get; set; }

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private bool _isAdmin;

        public DeletableModel DeletableModel { get; set; }
    }
}
