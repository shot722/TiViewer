using System.IO;
using System.Windows.Media.Imaging;
using TiViewer.Models;

namespace TiViewer.Services
{
    public class MetadataService
    {
        public ImageMetadata ExtractMetadata(string path)
        {
            var metadata = new ImageMetadata();

            if (!File.Exists(path))
                return metadata;

            var fileInfo = new FileInfo(path);
            metadata.FileSizeBytes = fileInfo.Length;

            try
            {
                var img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.UriSource = new System.Uri(path);
                img.EndInit();

                metadata.Width = img.PixelWidth;
                metadata.Height = img.PixelHeight;
            }
            catch
            {
            }

            return metadata;
        }
    }
}
