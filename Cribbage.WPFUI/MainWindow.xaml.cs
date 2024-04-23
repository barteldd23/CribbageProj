using Cribbage.BL;
using Cribbage.BL.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
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
        UserGame game;
        User loggedInUser = new User();
        CribbageGame cribbageGame;
        //SignalRConnection cribbageHubConnection;
        User secondPlayer = new User();
        HubConnection _connection;

        public MainWindow()
        {
            Start();

            string email = "tester@gmail.com";
            string password = "maple";

            //loggedInUser.Id = Guid.NewGuid();
            //loggedInUser.Email = "firsttester@test.test";
            //loggedInUser.DisplayName = "FirstTester";
            //loggedInUser.FirstName = "First";
            //loggedInUser.LastName = "Tester";
            //loggedInUser.Password = "test";
            //loggedInUser.GamesStarted = 0;
            //loggedInUser.GamesWon = 0;
            //loggedInUser.GamesLost = 0;
            //loggedInUser.WinStreak = 0;
            //loggedInUser.AvgPtsPerGame = 0;

            //secondPlayer.Id = Guid.NewGuid();
            //secondPlayer.Email = "secondtester@test.test";
            //secondPlayer.DisplayName = "SecondTester";
            //secondPlayer.FirstName = "Second";
            //secondPlayer.LastName = "Tester";
            //secondPlayer.Password = "test";
            //secondPlayer.GamesStarted = 0;
            //secondPlayer.GamesWon = 0;
            //secondPlayer.GamesLost = 0;
            //secondPlayer.WinStreak = 0;
            //secondPlayer.AvgPtsPerGame = 0;

            

            //Player firstPlayer = new Player(loggedInUser);
            //NewGameVsComputer(loggedInUser);

            InitializeComponent();

            Login(email, password);

            //SetUpGame(loggedInUser, secondPlayer);
        }

        public MainWindow(User user, Player player)
        {
            InitializeComponent();
            secondPlayer = player;
            loggedInUser = user;

            // need to convert User to Player --> it keeps crashing

            if (player.Email == "computer@computer.computer")
            {
                lblPlayer1DisplayName.Content = player.DisplayName + " Score";
                cribbageGame.Player_1 = player;

                lblPlayer2DisplayName.Content = user.DisplayName + " Score";

                //Player secondPlayer = new Player(user);
                //cribbageGame.Player_2 = secondPlayer;
            }
            else if (cribbageGame.Player_1 == null)
            {
                lblPlayer1DisplayName.Content = user.DisplayName;
                //Player firstPlayer = new Player(user);
                //cribbageGame.Player_1 = firstPlayer;
            }
            else
            {
                lblPlayer2DisplayName.Content = user.DisplayName;
                //Player secondPlayer = new Player(user);
                //cribbageGame.Player_2 = secondPlayer;
            }

            // Start the hub connection
            //cribbageHubConnection = new SignalRConnection(hubAddress);
        }

        #region "SignalRConnection"

        public void Start()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(hubAddress)
                .Build();

            _connection.On<bool, string, string>("LogInAttempt", (isLoggedIn, message, userJson) => ReceivedLoginMessage(isLoggedIn, message, userJson));
            _connection.On<string>("StartGameVsComputer", (cribbageGameJson) => StartGameVsComputerMessage(cribbageGameJson));
            _connection.On<string>("StartGameVsPlayer", (cribbageGameJson) => StartGameVsPlayerMessage(cribbageGameJson));

            _connection.StartAsync();
        }

        private void StartGameVsPlayerMessage(string cribbageGameJson)
        {
            MessageBox.Show(cribbageGameJson);
            //LandingPage.StartGameVsPlayer(message);
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

        private void StartGameVsComputerMessage(string cribbageGameJson)
        {
            MessageBox.Show(cribbageGameJson);
            //LandingPage.StartGameVsComputer(message);
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

        public void Login(string email, string password)
        {
            //Start();
            try
            {
                _connection.InvokeAsync("Login", email, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReceivedLoginMessage(bool isLoggedIn, string message, string userJson)
        {
            if (isLoggedIn)
            {
                loggedInUser = JsonConvert.DeserializeObject<User>(userJson);
                //MessageBox.Show(loggedInUser.DisplayName);
                NewGameVsComputer(loggedInUser);
                //WPFUI.Login.LoggedInCheck(isLoggedIn, userJson);
            }
            else // not logged in
            {
                MessageBox.Show("Failed log in");
                //WPFUI.Login.LoggedInCheck(isLoggedIn, userJson);
            }
        }

        #endregion

        private void SetUpGame(User user1, User user2)
        {
            Player firstPlayer = new Player(user1);
            Player secondPlayer = new Player(user2);

            lblPlayer1DisplayName.Content = firstPlayer.DisplayName + " Score";
            lblPlayer1Score.Content = firstPlayer.Score;

            lblPlayer2DisplayName.Content = secondPlayer.DisplayName + " Score";
            lblPlayer2Score.Content = secondPlayer.Score;
        }


        private void QuitGame_Click(object sender, RoutedEventArgs e)
        {
            // need to save prior to closing
            this.Close();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            //MainWindow mainWindow = new MainWindow(loggedInUser, secondPlayer);
            mainWindow.Show();

            // need to save prior to closing 
            this.Close();
        }

        private void btnSendToCrib_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPlayCard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNextHand_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
