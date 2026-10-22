using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TiViewer.Views
{
    public partial class ImageView : UserControl
    {
        private bool _isDragging;
        private Point _dragStart;
        private Point _origin;
        private double _zoom = 1.0;

        public ImageView()
        {
            InitializeComponent();
            DataContextChanged += (_, _) => ResetView();
        }

        private void ResetView()
        {
            _zoom = 1.0;
            ImageScaleTransform.ScaleX = 1.0;
            ImageScaleTransform.ScaleY = 1.0;
            ImageTranslateTransform.X = 0;
            ImageTranslateTransform.Y = 0;
        }

        private void MainImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var delta = e.Delta > 0 ? 0.12 : -0.12;
            _zoom = Math.Clamp(_zoom + delta, 0.25, 6.0);
            ImageScaleTransform.ScaleX = _zoom;
            ImageScaleTransform.ScaleY = _zoom;
            e.Handled = true;
        }

        private void MainImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isDragging = true;
            _dragStart = e.GetPosition(this);
            _origin = new Point(ImageTranslateTransform.X, ImageTranslateTransform.Y);
            MainImage.CaptureMouse();
            e.Handled = true;
        }

        private void MainImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging)
                return;

            var current = e.GetPosition(this);
            var dx = current.X - _dragStart.X;
            var dy = current.Y - _dragStart.Y;

            ImageTranslateTransform.X = _origin.X + dx;
            ImageTranslateTransform.Y = _origin.Y + dy;
        }

        private void MainImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            if (MainImage.IsMouseCaptured)
                MainImage.ReleaseMouseCapture();
        }

        private void MainImage_MouseLeave(object sender, MouseEventArgs e)
        {
            _isDragging = false;
            if (MainImage.IsMouseCaptured)
                MainImage.ReleaseMouseCapture();
        }
    }
}
