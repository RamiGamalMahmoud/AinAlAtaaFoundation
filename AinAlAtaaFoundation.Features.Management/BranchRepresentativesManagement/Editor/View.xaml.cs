﻿using AinAlAtaaFoundation.Models;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows;

namespace AinAlAtaaFoundation.Features.Management.BranchRepresentativesManagement.Editor
{
    internal partial class View : Window
    {
        public View(EditorViewModelBase viewModel, IMessenger messenger)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;
            Loaded += View_Loaded;

            messenger.Register<Shared.Messages.EntityUpdated<BranchRepresentative>>(this, (r, m) => Close());
            messenger.Register<Shared.Messages.EntityCreated<BranchRepresentative>>(this, (r, m) => Close());
        }

        private async void View_Loaded(object sender, RoutedEventArgs e)
        {
            await Dispatcher.InvokeAsync(_viewModel.LoadDataAsync);
        }

        private readonly EditorViewModelBase _viewModel;
    }
}
