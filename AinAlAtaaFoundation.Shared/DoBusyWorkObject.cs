using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Shared
{
    public partial class DoBusyWorkObject : ObservableObject, IDoBusyWorkObject
    {
        [ObservableProperty]
        private bool _isBusy;
    }
}
