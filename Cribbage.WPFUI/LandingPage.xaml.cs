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
        User loggedInUser;
        HubConnection _connection;

        public LandingPage()
        {
            Start();
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
            Start();
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

               foreach(Game game in userGames)
               {
                    lstSavedGames.Items.Add(game.Date.ToShortDateString() + " " + game.GameName);
               }
            }
            else
            {
                lstSavedGames.Items.Add("No saved games.");
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
            this.Hide();
        }

        private void btnNewGameVsPlayer_Click(object sender, RoutedEventArgs e)
        {
            NewGameVsPlayer(loggedInUser);
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
            CribbageGame cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);

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
