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
        List<Card> opponentHand;
        List<Card> playerHand;

        public MainWindow()
        {
            Start();
            InitializeComponent();
        }

        public MainWindow(CribbageGame cribbageGameInfo)
        {
            cribbageGame = cribbageGameInfo;

            // start the hub connection
            Start();

            InitializeComponent();
            SetUpGame();

        }

        private void SetUpGame()
        {
            lblPlayer1DisplayName.Content = cribbageGame.Player_1.DisplayName + " Score";
            lblPlayer1Score.Content = cribbageGame.Player_1.Score;

            lblPlayer2DisplayName.Content = cribbageGame.Player_2.DisplayName + " Score";
            lblPlayer2Score.Content = cribbageGame.Player_2.Score;

            if(cribbageGame.WhatToDo.ToString() == "SelectCribCards")
            {
                playerHand = cribbageGame.Player_1.Hand;
                opponentHand = cribbageGame.Player_2.Hand;

                displayOpponentHand(opponentHand, true);
                displayPlayerHand(playerHand);
            }

            //imgPlayerCard1.Source = cribbageGame.Player_1.Hand[0];
        }

        private void displayOpponentHand(List<Card> opponentHand, bool isShown = false)
        {
            if(isShown)
            {
                if (opponentHand.Count >= 1)
                {
                    MessageBox.Show(opponentHand.Count + " " + opponentHand[0]);
                    //imgOppenentCard1.Source = "images/" + opponentHand[0] + ".png";
                }
            }
            else
            {

            }
        }

        private void displayPlayerHand(List<Card> playerHand)
        {
            throw new NotImplementedException();
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

            MessageBox.Show(message + " " + cribbageGame.WhatToDo);

            StaThreadWrapper(() =>
            {
                var mainWindow = new MainWindow(cribbageGame);
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

        #endregion

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
