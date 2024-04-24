using Cribbage.BL.Models;
using Microsoft.AspNetCore.SignalR.Client;
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
        HubConnection _connection;
        //SignalRConnection cribbageHubConnection;

        public LandingPage()
        {
            Start();
            InitializeComponent();
        }

        public LandingPage(User user)
        {
            Start();
            InitializeComponent();
            lblWelcomeUser.Content = "Welcome " + user.FirstName + "!";

            loggedInUser = user;
            lstSavedGames.Visibility = Visibility.Collapsed;
            lstStats.Visibility = Visibility.Collapsed;
            lblGameStats.Visibility = Visibility.Collapsed;
            lblOpenASavedGame.Visibility = Visibility.Collapsed;

            // Start the hub connection
            //cribbageHubConnection = new SignalRConnection(hubAddress);
   
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
            NewGameVsComputer(loggedInUser);
        }

        private void btnNewGameVsPlayer_Click(object sender, RoutedEventArgs e)
        {
            NewGameVsPlayer(loggedInUser);
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
                //GetSavedGames(loggedInUser);
                lstSavedGames.Items.Add("Total Games Started: " + loggedInUser.GamesStarted);
            }
        }

        private static void StaThreadWrapper(Action action)
        {
            var t = new Thread(o =>
            {
                action();
                System.Windows.Threading.Dispatcher.Run();
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Name = "newThread";
            t.Start();
        }

        #region "SignalRConnection"

        public void Start()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(hubAddress)
                .Build();

            _connection.On<string, string>("StartGame", (message, cribbageGameJson) => StartGameVsComputerMessage(message, cribbageGameJson));
            _connection.On<string, string>("StartGameVsPlayer", (message, cribbageGameJson) => StartGameVsPlayerMessage(message, cribbageGameJson));

            _connection.StartAsync();
        }

        private void StartGameVsComputerMessage(string message, string cribbageGameJson)
        {
            CribbageGame cribbageGame = new CribbageGame();
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);

            StaThreadWrapper(() =>
            {
                var mainWindow = new MainWindow(cribbageGame, loggedInUser);
                mainWindow.Show();
            });
        }

        public void NewGameVsPlayer(User user)
        {
            try
            {
                string strUser = JsonConvert.SerializeObject(user);
                _connection.InvokeAsync("NewGameVsPlayer", strUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StartGameVsPlayerMessage(string message, string cribbageGameJson)
        {
            MessageBox.Show(message + " " + cribbageGameJson);
        }

        public void NewGameVsComputer(User user)
        {
            //Start();
            try
            {
                string strUser = JsonConvert.SerializeObject(user);
                _connection.InvokeAsync("NewGameVsComputer", strUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
        #endregion
}
