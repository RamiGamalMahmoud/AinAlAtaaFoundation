using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Models
{
    [ObservableObject]
    public partial class User : ModelBase
    {
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
