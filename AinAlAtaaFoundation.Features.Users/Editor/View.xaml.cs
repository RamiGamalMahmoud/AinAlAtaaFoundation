using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;

namespace AinAlAtaaFoundation.Features.Users.Editor
{
    internal partial class View : Window
    {
        public View(ViewModel viewModel, IMessenger messenger)
        {
            InitializeComponent();
            DataContext = viewModel;

            messenger.Register<Shared.Messages.EntityCreated<User>>(this, (r, m) =>
            {

            });

            messenger.Register<Shared.Messages.EntityUpdated<User>>(this, (r, m) =>
            {
                Close();
            });
        }
    }
}
