using CommunityToolkit.Mvvm.ComponentModel;

namespace AinAlAtaaFoundation.Models
{
    [ObservableObject]
    public partial class AssociationRepresentative : ModelBase
    {
        [ObservableProperty]
        private string _name;
    }
}
