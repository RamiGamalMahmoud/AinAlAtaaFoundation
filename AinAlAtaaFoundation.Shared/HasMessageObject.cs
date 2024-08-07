using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Shared
{
    public partial class HasMessageObject : ObservableObject
    {
        public bool HasMessage => !string.IsNullOrEmpty(Message);

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasMessage))]
        private string _message;

        public void Clear() => Message = "";
    }
}
