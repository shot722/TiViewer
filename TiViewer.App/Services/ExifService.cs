using System;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using TiViewer.Models;

namespace TiViewer.Services
{
    public class ExifService
    {
        public ImageMetadata ExtractMetadata(string path)
        {
            var metadata = new ImageMetadata();

            if (!File.Exists(path))
                return metadata;

            try
            {
                var fileInfo = new FileInfo(path);
                metadata.FileSizeBytes = fileInfo.Length;

                var directories = ImageMetadataReader.ReadMetadata(path);
                var exifDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();

                if (exifDirectory is not null)
                {
                    metadata.CameraModel = exifDirectory.GetDescription(ExifDirectoryBase.TagModel) ?? "Unknown";
                    metadata.LensModel = exifDirectory.GetDescription(ExifDirectoryBase.TagLensModel) ?? "Unknown";
                    metadata.Iso = exifDirectory.GetDescription(ExifDirectoryBase.TagIsoEquivalent) ?? "N/A";
                    metadata.ShutterSpeed = exifDirectory.GetDescription(ExifDirectoryBase.TagExposureTime) ?? "N/A";
                    metadata.Aperture = exifDirectory.GetDescription(ExifDirectoryBase.TagFNumber) ?? "N/A";
                    metadata.DateTaken = exifDirectory.GetDescription(ExifDirectoryBase.TagDateTimeOriginal)
                        ?? exifDirectory.GetDescription(ExifDirectoryBase.TagDateTimeDigitized)
                        ?? "N/A";
                }
            }
            catch
            {
            }

            try
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(path);
                image.EndInit();
                image.Freeze();

                metadata.Width = image.PixelWidth;
                metadata.Height = image.PixelHeight;
            }
            catch
            {
            }

            return metadata;
        }
    }
}
