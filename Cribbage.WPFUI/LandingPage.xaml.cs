using Cribbage.BL.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.VisualBasic.Devices;
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
        CribbageGame cribbageGame;
        bool hasSavedGames = false;
        string strUserGames = "";
        bool computer;

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
            hasSavedGames = isSuccess;
            strUserGames = userGamesJson;

            if (strUserGames == "[]") hasSavedGames = false;

            cribbageGame = new CribbageGame();

            this.MouseLeftButtonDown += delegate { DragMove(); };

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
            List<Game> userGames = JsonConvert.DeserializeObject<List<Game>>(userGamesJson);

            if (isSuccess && userGames.Count > 0)
            {
                foreach (Game game in userGames)
                {
                    lstSavedGames.Items.Add(game.Date.ToShortDateString() + " " + game.GameName);
                }
            }
            else
            {
                hasSavedGames = false;
                lstSavedGames.Items.Add("No saved games");
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnNewGameVsComputer_Click(object sender, RoutedEventArgs e)
        {
            computer = true;

            MainWindow mainWindow = new MainWindow(cribbageGame, loggedInUser, computer, hasSavedGames, strUserGames);
            mainWindow.ShowDialog();

            Dispatcher.Invoke(() =>
            {
                this.Close();
            });
        }

        private void btnNewGameVsPlayer_Click(object sender, RoutedEventArgs e)
        {
            computer = false;

            MainWindow mainWindow = new MainWindow(cribbageGame, loggedInUser, computer, hasSavedGames, strUserGames);
            mainWindow.ShowDialog();

            Dispatcher.Invoke(() =>
            {
                this.Close();
            });
        }
    }
}
