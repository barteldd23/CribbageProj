using Cribbage.BL.Models;
using System.Windows;

namespace Cribbage.WPFUI
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

        private void QuitGame_Click(object sender, RoutedEventArgs e)
        {
            // need to save prior to closing 
            this.Close();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // need to save prior to closing 
            this.Close();
        }

    }
}
