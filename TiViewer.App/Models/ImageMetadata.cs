namespace TiViewer.Models
{
    public class ImageMetadata
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public long FileSizeBytes { get; set; }
        public string CameraModel { get; set; } = "Unknown";
        public string LensModel { get; set; } = "Unknown";
        public string Iso { get; set; } = "N/A";
        public string ShutterSpeed { get; set; } = "N/A";
        public string Aperture { get; set; } = "N/A";
        public string DateTaken { get; set; } = "N/A";

        public string Resolution => $"{Width} x {Height}";
        public string FileSizeKB => FileSizeBytes switch
        {
            >= 1048576 => $"{(double)FileSizeBytes / 1048576:0.##} MB",
            >= 1024 => $"{(double)FileSizeBytes / 1024:0.##} KB",
            _ => $"{FileSizeBytes} B"
        };
    }
}
