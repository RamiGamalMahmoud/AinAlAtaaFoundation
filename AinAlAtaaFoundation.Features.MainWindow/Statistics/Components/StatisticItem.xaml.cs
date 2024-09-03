using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.MainWindow.Statistics.Components
{
    [ObservableObject]
    public partial class StatisticItem : UserControl
    {
        public StatisticItem()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => (string)GetValue(_titmeProperty);
            set => SetValue(_titmeProperty, value);
        }
        private static DependencyProperty _titmeProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(StatisticItem),
                new PropertyMetadata(null)
                );

        public IEnumerable<Item> Items
        {
            get => (IEnumerable<Item>)GetValue(_itemsProperty);
            set => SetValue(_itemsProperty, value);
        }
        private static DependencyProperty _itemsProperty =
            DependencyProperty.Register(
                nameof(Items),
                typeof(IEnumerable<Item>),
                typeof(StatisticItem),
                new PropertyMetadata(null)
                );

    }
}
