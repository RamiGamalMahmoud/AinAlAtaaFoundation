using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Models
{
    [ObservableObject]
    public partial class OrphanType : ModelBase
    {
        [ObservableProperty]
        private string _name;
    }
}
