using Cribbage.BL.Models;
using Microsoft.Data.SqlClient;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Media;

namespace Cribbage.WPFUI
{
    /// <summary>
    /// Interaction logic for LandingPage.xaml
    /// </summary>
    public partial class LandingPage : Window
    {
        //string hubAddress = "https://bigprojectapi-300089145.azurewebsites.net/CribbageHub";
        string hubAddress = "https://localhost:7186/CribbageHub";
        UserGame game;
        User loggedInUser;
        SignalRConnection cribbageHubConnection;

        public LandingPage(User user)
        {
            InitializeComponent();
            lblWelcomeUser.Content = "Welcome " + user.FirstName + "!";

            loggedInUser = user;

            lstSavedGames.Visibility = Visibility.Collapsed;
            lstStats.Visibility = Visibility.Collapsed;
            lblGameStats.Visibility = Visibility.Collapsed;
            lblOpenASavedGame.Visibility = Visibility.Collapsed;

            // Start the hub connection
            cribbageHubConnection = new SignalRConnection(hubAddress);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnNewGameVsComputer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(loggedInUser);
            mainWindow.Show();
            this.Close();
        }

        private void btnNewGameVsPlayer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(loggedInUser);
            mainWindow.Show();
            this.Close();
        }

        private void btnShowGameStats_Click(object sender, RoutedEventArgs e)
        {
            lstStats.Visibility = Visibility.Visible;
            lblGameStats.Visibility = Visibility.Visible;
            btnShowGameStats.Visibility = Visibility.Collapsed;

            cribbageHubConnection.GetSavedGames(loggedInUser);
            lstStats.Items.Add(loggedInUser.GamesWon);
        }

        private void btnShowSavedGames_Click(object sender, RoutedEventArgs e)
        {
            lstSavedGames.Visibility = Visibility.Visible;
            lblOpenASavedGame.Visibility = Visibility.Visible;
            btnShowSavedGames.Visibility = Visibility.Collapsed;

            
            cribbageHubConnection.GetSavedGames(loggedInUser);
            lstSavedGames.Items.Add(loggedInUser.GamesStarted);
        }
    }
}
