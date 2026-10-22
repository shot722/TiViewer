using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace TiViewer.Services
{
    public class ImageLoaderService
    {
        public BitmapImage? LoadFullImage(string path)
        {
            if (!File.Exists(path))
                return null;

            try
            {
                var img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.UriSource = new Uri(path);
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
