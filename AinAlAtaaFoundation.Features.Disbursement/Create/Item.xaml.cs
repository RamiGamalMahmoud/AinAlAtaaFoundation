using System.Windows;
using System.Windows.Controls;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.Create
{
    public partial class Item : UserControl
    {
        public Item()
        {
            InitializeComponent();
        }

        public string Title
        {
            get;
            set;
        }
        private static readonly DependencyProperty _titleProperty = DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(Item),
            new PropertyMetadata(null));

        public string Value
        {
            get;
            set;
        }
        private static readonly DependencyProperty _valueProperty = DependencyProperty.Register(
            nameof(Value),
            typeof(string),
            typeof(Item),
            new PropertyMetadata(null));
    }
}
