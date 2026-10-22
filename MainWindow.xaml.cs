using System.Windows;
using System.Windows.Input;
using TiViewer.ViewModels;

namespace TiViewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataContext is not MainViewModel vm)
                return;

            switch (e.Key)
            {
                case Key.F11:
                    vm.ToggleFullScreen(this);
                    e.Handled = true;
                    break;
                case Key.Right:
                    vm.SelectNext();
                    e.Handled = true;
                    break;
                case Key.Left:
                    vm.SelectPrevious();
                    e.Handled = true;
                    break;
            }
        }
    }
}
