using System.Windows;

namespace Cribbage.WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //string hubAddress = "https://bigprojectapi-300089145.azurewebsites.net/CribbageHub";
        string hubAddress = "https://localhost:7186/CribbageHub";

        public MainWindow()
        {
            InitializeComponent();

            // Start the hub connection
            SignalRConnection cribbageHubConnection = new SignalRConnection(hubAddress);
            cribbageHubConnection.Start();
        }

        private void QuitGame_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
