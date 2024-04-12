using Cribbage.BL.Models;
using System.Windows;

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
        User user;

        public LandingPage(User user)
        {
            InitializeComponent();
            lblWelcomeUser.Content = "Welcome " + user.FirstName + "!";

            lstSavedGames.Items.Add(user.GamesStarted);
            lstStats.Items.Add(user.GamesWon);

            //game.PlayerId = user.Id;

            //foreach(UserGame userGame in games) 
            //{
            // need to add logic to cribbage hub & signal r connection
            //    lstSavedGames.Items.Add(numGames)
            //}

            //foreach(UserGame userGame in games) 
            //{
            // need to add logic to cribbage hub & signal r connection
            //    lstStats.Items.Add(numGames)
            //}
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnNewGameVsComputer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(user);
            mainWindow.Show();
            this.Close();
        }

        private void btnNewGameVsPlayer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(user);
            mainWindow.Show();
            this.Close();
        }
    }
}
