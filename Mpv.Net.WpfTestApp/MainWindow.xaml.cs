using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows;

namespace Mpv.Net.WpfTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                Player.LoadFile(dialog.FileName);
            }
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Player != null)
            {
                Player.Stop();
                await Task.Delay(1000);
                Player.Dispose();
            }
        }
    }
}
