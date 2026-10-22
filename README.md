# TiViewer

TiViewer is a modern WPF image viewer built with .NET 8 and the MVVM pattern. It supports browsing a folder of images, viewing thumbnails, zooming and panning images, switching with keyboard navigation, and inspecting EXIF metadata in a sleek dark UI.

## Features

- Folder picker dialog to choose an image library
- Thumbnail browser for all supported images
- Full-size image viewer with smooth zoom and pan controls
- Mouse wheel zoom
- Click-and-drag pan support
- Fullscreen toggle with F11
- Left and right keyboard navigation
- EXIF metadata panel for camera, lens, ISO, shutter, aperture, and date
- Dark Fluent-style modern interface
- MVVM architecture with CommunityToolkit.Mvvm

## Supported image formats

- .jpg
- .jpeg
- .png
- .bmp
- .gif
- .tiff
- .webp

## Project structure

```text
TiViewer/
├── App.xaml
├── App.xaml.cs
├── MainWindow.xaml
├── MainWindow.xaml.cs
├── TiViewer.App.csproj
├── README.md
├── TiViewer.App/
│   ├── Models/
│   │   ├── ImageItem.cs
│   │   └── ImageMetadata.cs
│   ├── Services/
│   │   ├── ExifService.cs
│   │   ├── ImageLoaderService.cs
│   │   ├── MetadataService.cs
│   │   └── ThumbnailService.cs
│   ├── ViewModels/
│   │   ├── ImageViewModel.cs
│   │   └── MainViewModel.cs
│   └── Views/
│       ├── ImageView.xaml
│       ├── ImageView.xaml.cs
│       ├── MetadataPanel.xaml
│       └── MetadataPanel.xaml.cs
└──
```

## Requirements

- Windows 10 or later
- .NET 8 SDK
- Visual Studio 2022 with .NET desktop development workload

## Getting started

1. Clone the repository:

```bash
git clone https://github.com/shot722/TiViewer.git
cd TiViewer
```

2. Open the solution in Visual Studio or run the project with the .NET CLI.

3. Restore NuGet packages:

```bash
dotnet restore
```

4. Build the project:

```bash
dotnet build
```

5. Run the app:

```bash
dotnet run
```

## Usage

- Click Choose Folder to pick a directory containing images.
- Use the thumbnail list to select an image.
- Use the mouse wheel to zoom in or out.
- Hold the left mouse button and drag to pan the image.
- Press F11 to toggle fullscreen mode.
- Use the Left and Right arrow keys to navigate between images.

## Keyboard shortcuts

- Left Arrow: previous image
- Right Arrow: next image
- F11: toggle fullscreen

## EXIF metadata support

The app reads EXIF metadata using MetadataExtractor. The metadata panel displays:

- camera model
- lens model
- ISO
- shutter speed
- aperture
- date taken
- image resolution
- file size

## Notes

This project is intended as a modern desktop image viewer starter and can be extended with features such as:

- drag/drop support
- slideshow mode
- rating and tagging
- advanced editing tools
- image filtering and sorting

## License

This project is provided as-is for learning and personal use.
