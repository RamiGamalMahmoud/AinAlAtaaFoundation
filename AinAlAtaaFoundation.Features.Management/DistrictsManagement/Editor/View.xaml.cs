using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;

namespace AinAlAtaaFoundation.Features.Management.DistrictsManagement.Editor
{
    internal partial class View : Window
    {
        public View(ViewModelEditorBase viewModel, IMessenger messenger)
        {
            InitializeComponent();
            DataContext = viewModel;

            messenger.Register<Shared.Messages.EntityCreated<District>>(this, (r, m) => Close());
            messenger.Register<Shared.Messages.EntityUpdated<District>>(this, (r, m) => Close());
        }
    }
}
