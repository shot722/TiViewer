using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using TiViewer.Models;
using TiViewer.Services;

namespace TiViewer.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ImageLoaderService _imageLoader = new();
        private readonly ThumbnailService _thumbnailService = new();
        private readonly MetadataService _metadataService = new();
        private readonly ExifService _exifService = new();
        private readonly string[] _supportedExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff", ".webp" };

        public ObservableCollection<ImageItem> Images { get; } = new();

        [ObservableProperty]
        private ImageItem? selectedImage;

        [RelayCommand]
        private void ChooseFolder()
        {
            var dialog = new OpenFolderDialog
            {
                Title = "Select a folder",
                Multiselect = false
            };

            if (dialog.ShowDialog() == true)
            {
                LoadImagesFromFolder(dialog.FolderName);
            }
        }

        public MainViewModel()
        {
            LoadImagesFromFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));
        }

        public void LoadImagesFromFolder(string folder)
        {
            if (!Directory.Exists(folder))
                return;

            Images.Clear();

            foreach (var file in Directory.GetFiles(folder)
                .Where(file => _supportedExtensions.Contains(Path.GetExtension(file).ToLowerInvariant()))
                .OrderBy(file => file, StringComparer.OrdinalIgnoreCase))
            {
                var metadata = _metadataService.ExtractMetadata(file);
                var exif = _exifService.ExtractMetadata(file);

                metadata.CameraModel = exif.CameraModel;
                metadata.LensModel = exif.LensModel;
                metadata.Iso = exif.Iso;
                metadata.ShutterSpeed = exif.ShutterSpeed;
                metadata.Aperture = exif.Aperture;
                metadata.DateTaken = exif.DateTaken;

                var item = new ImageItem
                {
                    FilePath = file,
                    Thumbnail = _thumbnailService.LoadThumbnail(file),
                    FullImage = _imageLoader.LoadFullImage(file),
                    Metadata = metadata
                };

                if (item.FullImage is not null)
                {
                    item.Metadata.Width = item.FullImage.PixelWidth;
                    item.Metadata.Height = item.FullImage.PixelHeight;
                }

                Images.Add(item);
            }

            SelectedImage = Images.FirstOrDefault();
        }

        public void SelectNext()
        {
            if (Images.Count == 0)
                return;

            var currentIndex = SelectedImage is null ? -1 : Images.IndexOf(SelectedImage);
            var nextIndex = currentIndex >= Images.Count - 1 ? 0 : currentIndex + 1;
            SelectedImage = Images[nextIndex];
        }

        public void SelectPrevious()
        {
            if (Images.Count == 0)
                return;

            var currentIndex = SelectedImage is null ? 0 : Images.IndexOf(SelectedImage);
            var previousIndex = currentIndex <= 0 ? Images.Count - 1 : currentIndex - 1;
            SelectedImage = Images[previousIndex];
        }

        public void ToggleFullScreen(Window window)
        {
            if (window is null)
                return;

            if (window.WindowStyle == WindowStyle.None)
            {
                window.WindowStyle = WindowStyle.SingleBorderWindow;
                window.WindowState = WindowState.Normal;
                window.Topmost = false;
            }
            else
            {
                window.WindowStyle = WindowStyle.None;
                window.WindowState = WindowState.Maximized;
                window.Topmost = true;
            }
        }
    }
}
