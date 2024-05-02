using Cribbage.BL.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.DirectoryServices.ActiveDirectory;
using System.Windows;

namespace Cribbage.WPFUI
{
    /// <summary>
    /// Interaction logic for LandingPage.xaml
    /// </summary>
    public partial class LandingPage : Window
    {
        User loggedInUser;
        CribbageGame cribbageGame = new CribbageGame();
        bool hasSavedGames = false;
        string strUserGames = "";

        public LandingPage()
        {
            InitializeComponent();

            if (loggedInUser == null)
            {
                Login login = new Login();
                login.ShowDialog();
            }
            this.Hide();
        }

        public LandingPage(User user, bool isSuccess, string userGamesJson)
        {
            InitializeComponent();
            lblWelcomeUser.Content = "Welcome " + user.FirstName + "!";

            loggedInUser = user;

            ShowStats();
            SavedGamesCheck(isSuccess, userGamesJson);
        }

        public void ShowStats()
        {
            lstStats.Items.Add("Total Games Started: " + loggedInUser.GamesStarted);
            lstStats.Items.Add("Games Won: " + loggedInUser.GamesWon);
            lstStats.Items.Add("Games Lost: " + loggedInUser.GamesLost);
            lstStats.Items.Add("Win Streak: " + loggedInUser.WinStreak);
            lstStats.Items.Add("Average Points Per Game: " + loggedInUser.AvgPtsPerGame);
        }

        public void SavedGamesCheck(bool isSuccess, string userGamesJson)
        {
            if (isSuccess)
            {
                List<Game> userGames = JsonConvert.DeserializeObject<List<Game>>(userGamesJson);

                foreach (Game game in userGames)
                {
                    lstSavedGames.Items.Add(game.Date.ToShortDateString() + " " + game.GameName);
                }
            }
            else
            {
                lstSavedGames.Items.Add("No saved games.");
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnNewGameVsComputer_Click(object sender, RoutedEventArgs e)
        {
            cribbageGame.Computer = true;
            MainWindow mainWindow = new MainWindow(cribbageGame, loggedInUser, hasSavedGames, strUserGames);
            mainWindow.ShowDialog();

            this.Hide();
        }

        private void btnNewGameVsPlayer_Click(object sender, RoutedEventArgs e)
        {
            cribbageGame.Computer = false;
            MainWindow mainWindow = new MainWindow(cribbageGame, loggedInUser, hasSavedGames, strUserGames);
            mainWindow.ShowDialog();

            this.Hide();
        }
    }
}
