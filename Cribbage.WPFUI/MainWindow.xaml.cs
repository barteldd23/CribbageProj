using Cribbage.BL;
using Cribbage.BL.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Cribbage.WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //string hubAddress = "https://bigprojectapi-300089145.azurewebsites.net/CribbageHub";
        string hubAddress = "https://localhost:7186/CribbageHub";
        CribbageGame cribbageGame = new CribbageGame();
        //SignalRConnection cribbageHubConnection;
        HubConnection _connection;
        List<Card> opponentHand;
        List<Card> playerHand;
        List<Card> selectedCards = new List<Card>();
        User loggedInUser;

        public MainWindow()
        {
            Start();
            InitializeComponent();
        }

        public MainWindow(CribbageGame cribbageGameInfo, User user)
        {
            cribbageGame = cribbageGameInfo;
            loggedInUser = user;

            // start the hub connection
            Start();

            InitializeComponent();
            SetUpGame();

        }

        #region "GameSetup"
        private void SetUpGame()
        {
            rec1.Visibility = Visibility.Collapsed;
            rec2.Visibility = Visibility.Collapsed;
            rec3.Visibility = Visibility.Collapsed;
            rec4.Visibility = Visibility.Collapsed;
            rec5.Visibility = Visibility.Collapsed;
            rec6.Visibility = Visibility.Collapsed;

            btnRefreshCards.Visibility = Visibility.Collapsed;

            lblPlayer1DisplayName.Content = cribbageGame.Player_1.DisplayName + " Score";
            lblPlayer1Score.Content = cribbageGame.Player_1.Score;
            lblPlayerHand.Content = cribbageGame.Player_1.DisplayName + "'s Hand";

            lblPlayer2DisplayName.Content = cribbageGame.Player_2.DisplayName + " Score";
            lblPlayer2Score.Content = cribbageGame.Player_2.Score;
            lblOpponentHand.Content = cribbageGame.Player_2.DisplayName + "'s Hand";

            imgCribCard1.Source = null;
            imgCribCard2.Source = null;
            imgCribCard3.Source = null;
            imgCribCard4.Source = null;

            if (cribbageGame.WhatToDo.ToString() == "SelectCribCards")
            {
                playerHand = cribbageGame.Player_1.Hand;
                opponentHand = cribbageGame.Player_2.Hand;

                displayOpponentHand(opponentHand, true);
                displayPlayerHand(playerHand);
            }
        }

        private void displayOpponentHand(List<Card> opponentHand, bool isShown = false)
        {
            BitmapImage card = new BitmapImage();
            if (isShown)
            {
                if (opponentHand.Count >= 1)
                {
                    
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + opponentHand[0].imgPath);
                    card.EndInit();
                    imgOppenentCard1.Source = card;
                }
                else
                    imgOppenentCard1.Source = null;
                if (opponentHand.Count >= 2)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + opponentHand[1].imgPath);
                    card.EndInit();
                    imgOppenentCard2.Source = card;
                }
                else
                    imgOppenentCard2.Source = null;
                if (opponentHand.Count >= 3)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + opponentHand[2].imgPath);
                    card.EndInit();
                    imgOppenentCard3.Source = card;
                }
                else
                    imgOppenentCard3.Source = null;
                if (opponentHand.Count >= 4)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + opponentHand[3].imgPath);
                    card.EndInit();
                    imgOppenentCard4.Source = card;
                }
                else
                    imgOppenentCard4.Source = null;
                if (opponentHand.Count >= 5)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + opponentHand[4].imgPath);
                    card.EndInit();
                    imgOppenentCard5.Source = card;
                }
                else
                    imgOppenentCard5.Source = null;
                if (opponentHand.Count >= 6)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + opponentHand[5].imgPath);
                    card.EndInit();
                    imgOppenentCard6.Source = card;
                }
                else
                    imgOppenentCard6.Source = null;
            }
            else
            {
                if (opponentHand.Count >= 1)
                {

                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    imgOppenentCard1.Source = card;
                }
                else
                    imgOppenentCard1.Source = null;
                if (opponentHand.Count >= 2)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    imgOppenentCard2.Source = card;
                }
                else
                    imgOppenentCard2.Source = null;
                if (opponentHand.Count >= 3)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    imgOppenentCard3.Source = card;
                }
                else
                    imgOppenentCard3.Source = null;
                if (opponentHand.Count >= 4)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    imgOppenentCard4.Source = card;
                }
                else
                    imgOppenentCard4.Source = null;
                if (opponentHand.Count >= 5)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    imgOppenentCard5.Source = card;
                }
                else
                    imgOppenentCard5.Source = null;
                if (opponentHand.Count >= 6)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    imgOppenentCard6.Source = card;
                }
                else
                    imgOppenentCard6.Source = null;
            }
        }

        private void displayPlayerHand(List<Card> playerHand)
        {
            BitmapImage card = new BitmapImage();
            if (playerHand.Count >= 1)
            {               
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + playerHand[0].imgPath);
                card.EndInit();
                imgPlayerCard1.Source = card;
            }
            else
                imgPlayerCard1.Source = null;
            if (playerHand.Count >= 2)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + playerHand[1].imgPath);
                card.EndInit();
                imgPlayerCard2.Source = card;
            }
            else
                imgPlayerCard2.Source = null;
            if (playerHand.Count >= 3)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + playerHand[2].imgPath);
                card.EndInit();
                imgPlayerCard3.Source = card;
            }
            else
                imgPlayerCard3.Source = null;
            if (playerHand.Count >= 4)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + playerHand[3].imgPath);
                card.EndInit();
                imgPlayerCard4.Source = card;
            }
            else
                imgPlayerCard4.Source = null;
            if (playerHand.Count >= 5)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + playerHand[4].imgPath);
                card.EndInit();
                imgPlayerCard5.Source = card;
            }
            else
                imgPlayerCard5.Source = null;
            if (playerHand.Count >= 6)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + playerHand[5].imgPath);
                card.EndInit();
                imgPlayerCard6.Source = card;
            }
            else
                imgPlayerCard6.Source = null;
        }
        #endregion


        #region "SignalRConnection"

        public void Start()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(hubAddress)
                .Build();

            _connection.On<string, string>("StartGame", (message, cribbageGameJson) => StartGameVsComputerMessage(message, cribbageGameJson));
            _connection.On<string, string>("StartGameVsPlayer", (message, cribbageGameJson) => StartGameVsPlayerMessage(message, cribbageGameJson));
            _connection.On<string, string>("CutCard", (cribbageGameJson, message) => CutCardMessage(cribbageGameJson, message));
            _connection.On<string>("GameFinished", (cribbageGameJson) => GameFinishedMessage(cribbageGameJson));
            _connection.On<string, string>("PlayHand", (cribbageGameJson, message) => PlayHandMessage(cribbageGameJson, message));

            _connection.StartAsync();
        }

        private void PlayHandMessage(string cribbageGameJson, string message)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);

            playerHand = cribbageGame.Player_1.Hand;
            opponentHand = cribbageGame.Player_2.Hand;

            //MessageBox.Show("Cut card is " + cribbageGame.CutCard.name);
            MessageBox.Show("Player 1 hand count: " + playerHand.Count);
            MessageBox.Show("Player 2 hand count: " + opponentHand.Count);
        }

        private void GameFinishedMessage(string cribbageGameJson)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);

            MessageBox.Show(cribbageGame.Winner.ToString());
        }

        private void CutCardMessage(string cribbageGameJson, string message)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);

            MessageBox.Show(message);
        }

        private void StartGameVsComputerMessage(string message, string cribbageGameJson)
        {
            //CribbageGame cribbageGame = new CribbageGame();
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);

            MessageBox.Show(message + " " + cribbageGame.WhatToDo);

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

        #endregion

        #region "Buttons"

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
            if(selectedCards.Count == 2)
            {
                try
                {
                    string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                    string strSelectedCards = JsonConvert.SerializeObject(selectedCards);
                    string userJson = JsonConvert.SerializeObject(loggedInUser);
                    _connection.InvokeAsync("CardsToCrib", cribbageGameJson, strSelectedCards, userJson);

                    //testing only!
                    BitmapImage card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + selectedCards[0].imgPath);
                    card.EndInit();
                    imgCribCard1.Source = card;

                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + selectedCards[1].imgPath);
                    card.EndInit();
                    imgCribCard2.Source = card;

                    // when done testing
                    //BitmapImage card = new BitmapImage();
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    //imgCribCard1.Source = card;
                    //imgCribCard2.Source = card;
                    imgCribCard3.Source = card;
                    imgCribCard4.Source = card;

                    btnRefreshCards.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Select a total of 2 cards to go to the Crib");
            }
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
        #endregion

        #region "CardsSelected"

        private void card1Selected(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (rec1.Visibility == Visibility.Collapsed)
            {
                rec1.Visibility = Visibility.Visible;
                selectedCards.Add(playerHand[0]);
            }
            else
            {
                rec1.Visibility = Visibility.Collapsed;
                selectedCards.Remove(playerHand[0]);
            }
        }

        private void card2Selected(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (rec2.Visibility == Visibility.Collapsed)
            {
                rec2.Visibility = Visibility.Visible;
                selectedCards.Add(playerHand[1]);
            }
            else
            {
                rec2.Visibility = Visibility.Collapsed;
                selectedCards.Remove(playerHand[1]);
            }
        }

        private void card3Selected(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (rec3.Visibility == Visibility.Collapsed)
            {
                rec3.Visibility = Visibility.Visible;
                selectedCards.Add(playerHand[2]);
            }
            else
            {
                rec3.Visibility = Visibility.Collapsed;
                selectedCards.Remove(playerHand[2]);
            }
        }

        private void card4Selected(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (rec4.Visibility == Visibility.Collapsed)
            {
                rec4.Visibility = Visibility.Visible;
                selectedCards.Add(playerHand[3]);
            }
            else
            {
                rec4.Visibility = Visibility.Collapsed;
                selectedCards.Remove(playerHand[3]);
            }
        }

        private void card5Selected(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (rec5.Visibility == Visibility.Collapsed)
            {
                rec5.Visibility = Visibility.Visible;
                selectedCards.Add(playerHand[4]);
            }
            else
            {
                rec5.Visibility = Visibility.Collapsed;
                selectedCards.Remove(playerHand[4]);
            }
        }

        private void card6Selected(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (rec6.Visibility == Visibility.Collapsed)
            {
                rec6.Visibility = Visibility.Visible;
                selectedCards.Add(playerHand[5]);
            }
            else
            {
                rec6.Visibility = Visibility.Collapsed;
                selectedCards.Remove(playerHand[5]);
            }
        }
        #endregion

        private void UpdateCutCard(CribbageGame cribbageGame)
        {
            if (cribbageGame.CutCard != null)
            {
                BitmapImage card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/card" + cribbageGame.CutCard.suit.ToString() + "_" + cribbageGame.CutCard.face.ToString() + ".png");
                card.EndInit();
                imgCutCard.Source = card;
            }
        }


        //Refresh the screen method 
        //Read the "what to do property" --> goes to the correct method (switch statement) to set the screen
        //Read the player turn --> match player turn id to user id
        private void ReFreshCardsClick(object sender, RoutedEventArgs e)
        {
            rec1.Visibility = Visibility.Collapsed;
            rec2.Visibility = Visibility.Collapsed;
            rec3.Visibility = Visibility.Collapsed;
            rec4.Visibility = Visibility.Collapsed;
            rec5.Visibility = Visibility.Collapsed;
            rec6.Visibility = Visibility.Collapsed;

            selectedCards = new List<Card>();

            displayPlayerHand(playerHand);
            displayOpponentHand(opponentHand, true);

            UpdateCutCard(cribbageGame);
        }
    }
}
