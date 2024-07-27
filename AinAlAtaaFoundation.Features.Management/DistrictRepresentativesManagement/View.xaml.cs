﻿using AinAlAtaaFoundation.Shared.Abstraction;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.Management.DistrictRepresentativesManagement
{
    internal partial class View : UserControl, IClanRepresentativesView
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
            await Dispatcher.InvokeAsync(_viewModel.LoadDataAsync);
        }

        private readonly ViewModel _viewModel;
    }
}
