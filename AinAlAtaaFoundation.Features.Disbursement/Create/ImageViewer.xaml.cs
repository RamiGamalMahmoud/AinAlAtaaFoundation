using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

namespace AinAlAtaaFoundation.Features.DisbursementManagement.Create
{
    [ObservableObject]
    public partial class ImageViewer : Window
    {
        public ImageViewer(string imagtePath)
        {
            InitializeComponent();
            DataContext = this;
            ImagePath = imagtePath;
        }

        public static void ShowImage(string imagePath) => new ImageViewer(imagePath).ShowDialog();

        [ObservableProperty]
        private string _imagePath;
    }
}
