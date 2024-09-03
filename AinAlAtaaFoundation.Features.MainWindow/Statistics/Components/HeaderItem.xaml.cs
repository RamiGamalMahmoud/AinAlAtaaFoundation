using System.Windows;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.MainWindow.Statistics.Components
{
    public partial class HeaderItem : UserControl
    {
        public HeaderItem()
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
                typeof(HeaderItem),
                new PropertyMetadata(null)
                );

        public int Value
        {
            get => (int)GetValue(_valueProperty);
            set => SetValue(_valueProperty, value);
        }
        private static DependencyProperty _valueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(int),
                typeof(HeaderItem),
                new PropertyMetadata(null)
                );
    }
}
