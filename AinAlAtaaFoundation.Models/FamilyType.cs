using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Models
{
    [ObservableObject]
    public partial class FamilyType : ModelBase
    {
        [ObservableProperty]
        private string _name;
    }
}
