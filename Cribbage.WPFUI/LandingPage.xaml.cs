using Cribbage.BL.Models;
using Microsoft.Data.SqlClient;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Media;
using Newtonsoft.Json;
using System.Collections;

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
        //public ObservableCollection<string> Stats;
        Window landingPage;

        public LandingPage(User user)
        {
            landingPage = this;
            this.DataContext = this;
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

        //public LandingPage() {}

        //private static void StaThreadWrapper(Action action)
        //{
        //    var t = new Thread(o =>
        //    {
        //        action();
        //        System.Windows.Threading.Dispatcher.Run();
        //    });
        //    t.SetApartmentState(ApartmentState.STA);
        //    t.Start();
        //}

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

                //StaThreadWrapper(() =>
                //{
                //    // NOTE: making a new page makes the listbox NULL!!
                //    var games = new LandingPage();
                //    //games.lstSavedGames.ItemsSource = LoadUserGames(userGames);
                //    games.AddSavedGames(userGames);
                //});

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

        public void closePage()
        {
            landingPage.Close();
        }
        
        //private static ArrayList LoadUserGames(List<string> userGames)
        //{
        //    ArrayList gamesList = new ArrayList();
        //    foreach (var game in userGames)
        //    {
        //        gamesList.Add(game);
        //    }
        //    return gamesList;
        //}

        //private void AddSavedGames(List<string> userGames)
        //{
        //    MessageBox.Show("game");
        //    //foreach(var game in userGames)
        //    //{
        //    //    lstSavedGames.Items.Add(game);
        //    //}
        //}


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnNewGameVsComputer_Click(object sender, RoutedEventArgs e)
        {


            //Player computer = new Player();
            //computer.Id = new Guid();
            //computer.Email = "computer@computer.computer";
            //computer.DisplayName = "Computer Bot";
            //computer.FirstName = "Computer";
            //computer.LastName = "Bot";
            //computer.Password = "computer";
            //computer.GamesStarted = 0;
            //computer.GamesWon = 0;
            //computer.GamesLost = 0;
            //computer.WinStreak = 0;
            //computer.AvgPtsPerGame = 0;

            cribbageHubConnection.NewGameVsComputer(loggedInUser);

            //MainWindow mainWindow = new MainWindow(loggedInUser, computer);
            //mainWindow.Show();
            //this.Close();
        }

        private void btnNewGameVsPlayer_Click(object sender, RoutedEventArgs e)
        {


            //Player human = new Player();
            //MainWindow mainWindow = new MainWindow(loggedInUser, human);
            //mainWindow.Show();
            //this.Close();
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
