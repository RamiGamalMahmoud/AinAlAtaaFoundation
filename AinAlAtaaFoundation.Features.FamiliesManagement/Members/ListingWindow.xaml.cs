using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.Members
{
    [ObservableObject]
    internal partial class ListingWindow : Window, IFamilyMembersViewWindow
    {
        public ListingWindow(View view)
        {
            InitializeComponent();
            DataContext = this;
            View = view;
        }

        public View View { get; }
    }
}
