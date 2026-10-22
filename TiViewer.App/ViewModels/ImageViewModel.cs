using CommunityToolkit.Mvvm.ComponentModel;
using TiViewer.Models;

namespace TiViewer.ViewModels
{
    public partial class ImageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ImageItem? image;

        public ImageViewModel(ImageItem item)
        {
            Image = item;
        }
    }
}
