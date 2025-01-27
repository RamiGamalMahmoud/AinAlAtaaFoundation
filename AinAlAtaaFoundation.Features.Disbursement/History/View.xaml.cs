﻿using AinAlAtaaFoundation.Shared.Abstraction;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.History
{
    internal partial class View : UserControl, IDisbursementHistoryView
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            _viewModel = viewModel;
            Loaded += View_Loaded;
        }

        private async void View_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!_isLoaded)
            {
                await Dispatcher.InvokeAsync(_viewModel.LoadDataAsync);
                _isLoaded = true;
            }
        }

        private bool _isLoaded;
        private readonly ViewModel _viewModel;
    }
}
