using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace TiViewer.Services
{
    public class ThumbnailService
    {
        public BitmapImage? LoadThumbnail(string path, int maxWidth = 300)
        {
            if (!File.Exists(path))
                return null;

            try
            {
                var img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.UriSource = new Uri(path);
                img.DecodePixelWidth = maxWidth;
                img.EndInit();
                img.Freeze();
                return img;
            }
            catch
            {
                return null;
            }
        }
    }
}
