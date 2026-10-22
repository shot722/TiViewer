using System.Windows.Media.Imaging;

namespace TiViewer.Models
{
    public class ImageItem
    {
        public string FilePath { get; set; } = string.Empty;
        public string FileName => System.IO.Path.GetFileName(FilePath);

        public BitmapImage? Thumbnail { get; set; }
        public BitmapImage? FullImage { get; set; }

        public ImageMetadata Metadata { get; set; } = new();
    }
}
