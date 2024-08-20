﻿using AinAlAtaaFoundation.Shared.Abstraction;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.Create
{
    internal partial class View : UserControl, IDisbursementView
    {
        private readonly ViewModel _viewModel;

        public View(ViewModel viewModel, IMessenger messenger)
        {
            InitializeComponent();

            messenger.Register<Messages.IdChangedMessage>(this, (r, m) => MainInputFocut());
            messenger.Register<Messages.ClearInputMessage>(this, (r, m) =>
            {
                MainInputFocut();
                MainInput.Text = "";
            });

            DataContext = viewModel;
            Loaded += View_Loaded;
            _viewModel = viewModel;
        }

        private async void View_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            MainInputFocut();
            await Dispatcher.Invoke(_viewModel.LoadDataAsync);
        }

        private void MainInputFocut()
        {
            MainInput.Focus();
            MainInput.SelectAll();
        }
    }
}