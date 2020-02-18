using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;

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

        //Show a file select dialog and load the selected file
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                Player.LoadFile(dialog.FileName);
            }
        }

        // When closing the window stop played video
        // and Dispose native dll resources
        // A wait is necessary after stop, because
        // it doesn't happen instantainously
        private async void Window_Closing(object sender, CancelEventArgs e)
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
