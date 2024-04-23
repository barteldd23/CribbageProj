using Cribbage.BL.Models;
using Newtonsoft.Json;
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

        public static Tuple<List<string>, List<DateTime>> SavedGamesCheck(bool isSuccess, string userGamesJson)
        {
            if (isSuccess)
            {
                MessageBox.Show("UserGamesJson: " + userGamesJson);
                List<Game> userGames = new List<Game>();
                userGames = JsonConvert.DeserializeObject<List<Game>>(userGamesJson);
                string gameName;
                List<string> savedGameNames = new List<string>();
                List<DateTime> savedDates = new List<DateTime>();

                foreach(Game game in userGames)
                {
                    savedGameNames.Add(game.GameName);
                    savedDates.Add(game.Date);
                }

                MessageBox.Show("Saved Games Check TRUE: " + savedGameNames[0] + " " + savedDates[0]);

                return Tuple.Create(savedGameNames, savedDates);
            }
            else
            {
                List<string> savedGameNames = new List<string>();
                List<DateTime> savedDates = new List<DateTime>();

                MessageBox.Show("Saved Games Check FALSE");
                return Tuple.Create(savedGameNames, savedDates);
            }
        }

        public static void StartGameVsComputer(string message)
        {
            MessageBox.Show(message);
        }


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnNewGameVsComputer_Click(object sender, RoutedEventArgs e)
        {
            cribbageHubConnection.NewGameVsComputer(loggedInUser);
        }

        private void btnNewGameVsPlayer_Click(object sender, RoutedEventArgs e)
        {
            cribbageHubConnection.NewGameVsPlayer(loggedInUser);
        }

        private void btnShowGameStats_Click(object sender, RoutedEventArgs e)
        {
            lstStats.Visibility = Visibility.Visible;
            lblGameStats.Visibility = Visibility.Visible;
            btnShowGameStats.Visibility = Visibility.Collapsed;

            lstStats.Items.Add("Total Games Started: " + loggedInUser.GamesStarted);
            lstStats.Items.Add("Games Won: " + loggedInUser.GamesWon);
            lstStats.Items.Add("Games Lost: " + loggedInUser.GamesLost);
            lstStats.Items.Add("Win Streak: " + loggedInUser.WinStreak);
            lstStats.Items.Add("Average Points Per Game: " + loggedInUser.AvgPtsPerGame);
        }

        private void btnShowSavedGames_Click(object sender, RoutedEventArgs e)
        {
            lstSavedGames.Visibility = Visibility.Visible;
            lblOpenASavedGame.Visibility = Visibility.Visible;
            btnShowSavedGames.Visibility = Visibility.Collapsed;

            if(loggedInUser.GamesStarted == 0)
            {
                lstSavedGames.Items.Add("Total Games Started: " + loggedInUser.GamesStarted);
            }
            else
            {
                cribbageHubConnection.GetSavedGames(loggedInUser);
                lstSavedGames.Items.Add("Total Games Started: " + loggedInUser.GamesStarted);
            }
        }
    }
}
