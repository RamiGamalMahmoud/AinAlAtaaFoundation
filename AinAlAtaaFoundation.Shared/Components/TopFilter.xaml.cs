using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AinAlAtaaFoundation.Shared.Components
{
    public partial class TopFilter : UserControl
    {
        public TopFilter()
        {
            InitializeComponent();
        }

        public ICommand FilterCommand
        {
            get => (ICommand) GetValue(_filterCommandProperty);
            set => SetValue(_filterCommandProperty, value);
        }
        private static readonly DependencyProperty _filterCommandProperty =
            DependencyProperty.Register(
                nameof(FilterCommand),
                typeof(ICommand),
                typeof(TopFilter),
                new PropertyMetadata(null)
                );

        public TopFilterViewModel TopFilterViewModel
        {
            get => (TopFilterViewModel)GetValue(_topFilterViewModelProperty);
            set => SetValue(_topFilterViewModelProperty, value);
        }
        private static readonly DependencyProperty _topFilterViewModelProperty =
            DependencyProperty.Register(
                nameof(TopFilterViewModel),
                typeof(TopFilterViewModel),
                typeof(TopFilter),
                new PropertyMetadata(null)
                );
    }
}
