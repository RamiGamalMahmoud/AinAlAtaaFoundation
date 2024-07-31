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

        [ObservableProperty]
        private bool? _isActive;

        [ObservableProperty]
        private string _password;

    }
}
