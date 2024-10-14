using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;

namespace AinAlAtaaFoundation.Features.FamiliesManagement.SocialStatuses.Editor
{
    internal partial class View : Window
    {
        public View(ViewModel viewModel, IMessenger messenger)
        {
            InitializeComponent();
            DataContext = viewModel;
            messenger.Register<Shared.Messages.EntityCreated<SocialStatus>>(this, (r, m) => Close());
            messenger.Register<Shared.Messages.EntityUpdated<SocialStatus>>(this, (r, m) => Close());
        }
    }
}
