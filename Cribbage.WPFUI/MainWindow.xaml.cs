using Cribbage.BL.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
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
        HubConnection _connection;
        List<Card> opponentHand;
        List<Card> playerHand;
        List<Card> selectedCards = new List<Card>();
        User loggedInUser;
        string dealer;
        string currentCount;
        string signalRMessage;
        bool newHand = false;
        bool endGame = false;
        bool hasSavedGames = false;
        string strUserGames = "";
        bool mainMenuClick = false;
        bool exitClick = false;

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

            if(cribbageGame.WhatToDo == "readytostart")
            {
                // Update buttons
                btnNextHand.Visibility = Visibility.Collapsed;
                btnPlayCard.Visibility = Visibility.Collapsed;
                btnGo.Visibility = Visibility.Collapsed;
                btnCountCards.Visibility = Visibility.Collapsed;
                btnCutDeck.Visibility = Visibility.Collapsed;
                btnMainMenu.Visibility = Visibility.Collapsed;
                btnExit.Visibility = Visibility.Collapsed;
                btnSendToCrib.Visibility = Visibility.Collapsed;

                lblPlayer1DisplayName.Content = cribbageGame.Player_1.DisplayName + " Score";
                lblPlayer1Score.Content = cribbageGame.Player_1.Score;
                lblPlayerHand.Content = cribbageGame.Player_1.DisplayName + "'s Hand";

                lblPlayer2DisplayName.Content = cribbageGame.Player_2.DisplayName + " Score";
                lblPlayer2Score.Content = cribbageGame.Player_2.Score;
                lblOpponentHand.Content = cribbageGame.Player_2.DisplayName + "'s Hand";

                lblPlayersCrib.Content = dealer + "'s Crib";

                imgCribCard1.Source = null;
                imgCribCard2.Source = null;
                imgCribCard3.Source = null;
                imgCribCard4.Source = null;

                imgPlayedCard1.Source = null;
                imgPlayedCard2.Source = null;
                imgPlayedCard3.Source = null;
                imgPlayedCard4.Source = null;
                imgPlayedCard5.Source = null;
                imgPlayedCard6.Source = null;
                imgPlayedCard7.Source = null;
                imgPlayedCard8.Source = null;

                displayOpponentHand(opponentHand, true);
                displayPlayerHand(playerHand);
            }
            else if(cribbageGame.WhatToDo == "waitingforplayer2")
            {
                // Update buttons
                btnNextHand.Visibility = Visibility.Collapsed;
                btnPlayCard.Visibility = Visibility.Collapsed;
                btnGo.Visibility = Visibility.Collapsed;
                btnCountCards.Visibility = Visibility.Collapsed;
                btnCutDeck.Visibility = Visibility.Collapsed;
                btnMainMenu.Visibility = Visibility.Collapsed;
                btnExit.Visibility = Visibility.Collapsed;
                btnSendToCrib.Visibility = Visibility.Collapsed;

                lblPlayer1DisplayName.Content = cribbageGame.Player_1.DisplayName + " Score";
                lblPlayer1Score.Content = cribbageGame.Player_1.Score;
                lblPlayerHand.Content = cribbageGame.Player_1.DisplayName + "'s Hand";

                lblPlayer2DisplayName.Content = "Player 2 Score";
                lblPlayer2Score.Content = 0;
                lblOpponentHand.Content = "Player 2's Hand";

                lblPlayersCrib.Content = "Dealer's Crib";

                imgCribCard1.Source = null;
                imgCribCard2.Source = null;
                imgCribCard3.Source = null;
                imgCribCard4.Source = null;

                imgPlayedCard1.Source = null;
                imgPlayedCard2.Source = null;
                imgPlayedCard3.Source = null;
                imgPlayedCard4.Source = null;
                imgPlayedCard5.Source = null;
                imgPlayedCard6.Source = null;
                imgPlayedCard7.Source = null;
                imgPlayedCard8.Source = null;

                displayOpponentHand(opponentHand, true);
                displayPlayerHand(playerHand);
            }
            else SetUpGame();
        }

        public MainWindow(CribbageGame cribbageGameInfo, User user, bool isSuccess, string userGamesJson)
        {
            cribbageGame = cribbageGameInfo;
            loggedInUser = user;
            hasSavedGames = isSuccess;
            strUserGames = userGamesJson;

            // start the hub connection
            Start();

            InitializeComponent();
            SetUpGame();
        }

        #region "GameSetup"
        private void SetUpGame()
        {
            if (cribbageGame.Dealer == 1) dealer = cribbageGame.Player_1.DisplayName;
            else dealer = cribbageGame.Player_2.DisplayName;

            RemoveSelectedItems();

            // Testing button
            //btnEndGame.Visibility = Visibility.Visible;

            // Update buttons
            btnNextHand.Visibility = Visibility.Collapsed;
            btnPlayCard.Visibility = Visibility.Collapsed;
            btnGo.Visibility = Visibility.Collapsed;
            btnCountCards.Visibility = Visibility.Collapsed;
            btnCutDeck.Visibility = Visibility.Collapsed;
            btnMainMenu.Visibility = Visibility.Collapsed;
            btnExit.Visibility = Visibility.Collapsed;
            btnSendToCrib.Visibility = Visibility.Visible;

            lblPlayer1DisplayName.Content = cribbageGame.Player_1.DisplayName + " Score";
            lblPlayer1Score.Content = cribbageGame.Player_1.Score;
            lblPlayerHand.Content = cribbageGame.Player_1.DisplayName + "'s Hand";

            lblPlayer2DisplayName.Content = cribbageGame.Player_2.DisplayName + " Score";
            lblPlayer2Score.Content = cribbageGame.Player_2.Score;
            lblOpponentHand.Content = cribbageGame.Player_2.DisplayName + "'s Hand";

            lblPlayersCrib.Content = dealer + "'s Crib";

            imgCribCard1.Source = null;
            imgCribCard2.Source = null;
            imgCribCard3.Source = null;
            imgCribCard4.Source = null;

            imgPlayedCard1.Source = null;
            imgPlayedCard2.Source = null;
            imgPlayedCard3.Source = null;
            imgPlayedCard4.Source = null;
            imgPlayedCard5.Source = null;
            imgPlayedCard6.Source = null;
            imgPlayedCard7.Source = null;
            imgPlayedCard8.Source = null;


            lblMessageToPlayers.Content = "Pick 2 cards to send to the Crib.";

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
                if (opponentHand.Count >= 5 && cribbageGame.WhatToDo == "SelectCribCards")
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + opponentHand[4].imgPath);
                    card.EndInit();
                    imgOppenentCard5.Source = card;
                }
                else
                    imgOppenentCard5.Source = null;
                if (opponentHand.Count >= 6 && cribbageGame.WhatToDo == "SelectCribCards")
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
                if (opponentHand.Count >= 5 && cribbageGame.WhatToDo == "SelectCribCards")
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    imgOppenentCard5.Source = card;
                }
                else
                    imgOppenentCard5.Source = null;
                if (opponentHand.Count >= 6 && cribbageGame.WhatToDo == "SelectCribCards")
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
            if (playerHand.Count >= 5 && cribbageGame.WhatToDo == "SelectCribCards")
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + playerHand[4].imgPath);
                card.EndInit();
                imgPlayerCard5.Source = card;
            }
            else
                imgPlayerCard5.Source = null;
            if (playerHand.Count >= 6 && cribbageGame.WhatToDo == "SelectCribCards")
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
            _connection.On<string, string>("ReadyToStart", (cribbageGameJson, message) => StartGameVsPlayerMessage(cribbageGameJson, message));
            _connection.On<string, string>("WaitingForConfirmation", (cribbageGameJson, message) => WaitingForConfirmationMessage(cribbageGameJson, message));
            _connection.On<string, string>("StartNewHand", (message, cribbageGameJson) => StartNewHandMessage(message, cribbageGameJson));
            _connection.On<string, string>("CutCard", (cribbageGameJson, message) => CutCardMessage(cribbageGameJson, message));
            _connection.On<string, string>("CardWasCut", (cribbageGameJson, message) => CardCutMessage(cribbageGameJson, message));
            _connection.On<string, string>("Action", (cribbageGameJson, message) => PlayCardMessage(cribbageGameJson, message));
            _connection.On<string, string>("HandsCounted", (cribbageGameJson, message) => HandsCountedMessage(cribbageGameJson, message));
            _connection.On<string, string>("GameFinished", (cribbageGameJson, message) => GameFinishedMessage(cribbageGameJson, message));
            _connection.On<string>("PlayerLeft", (message) => PlayerLeftMessage(message));

            _connection.StartAsync();
        }

        private void WaitingForConfirmationMessage(string cribbageGameJson, string message)
        {
            Dispatcher.Invoke(() => 
            {
                lblMessageToPlayers.Content = message;
                lstMessages.Items.Add(message);
            });
        }

        private void PlayerLeftMessage(string message)
        {
            if(mainMenuClick)
            {
                StaThreadWrapper(() =>
                {
                    var landingPage = new LandingPage(loggedInUser, hasSavedGames, strUserGames);
                    landingPage.ShowDialog();
                });

                Dispatcher.Invoke(() => { this.Close(); });
            }
            else if (exitClick)
            {
                Dispatcher.Invoke(() => { this.Close(); });
            }
            else
            {
                cribbageGame.Complete = true;
                endGame = true;
                Dispatcher.Invoke(() => { lblMessageToPlayers.Content = message; });
                RefreshScreen();
            }
        }

        private void GameFinishedMessage(string cribbageGameJson, string message)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);
            signalRMessage = message;

            RefreshScreen();
        }

        private void EndGame(CribbageGame cribbageGame)
        {
            try
            {
                string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                _connection.InvokeAsync("CheckCompletedGame", cribbageGameJson);
            }
            catch (Exception ex)
            {
                lblMessageToPlayers.Content = ex.Message;
            }
        }

        private void CardCutMessage(string cribbageGameJson, string message)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);
            playerHand = cribbageGame.Player_1.Hand;
            opponentHand = cribbageGame.Player_2.Hand;
            currentCount = cribbageGame.CurrentCount.ToString();
            signalRMessage = message;
            RefreshScreen();
        }

        private void CutCardMessage(string cribbageGameJson, string message)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);
            playerHand = cribbageGame.Player_1.Hand;
            opponentHand = cribbageGame.Player_2.Hand;
            currentCount = cribbageGame.CurrentCount.ToString();
            signalRMessage = message;
            RefreshScreen();
        }

        private void HandsCountedMessage(string cribbageGameJson, string message)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);
            signalRMessage = message;

            if (cribbageGame.Player_1.Score >= 121 || cribbageGame.Player_2.Score >= 121)
            {
                endGame = true;
            }

            RefreshScreen();

            Dispatcher.Invoke(() =>
            { 
                playerHand = cribbageGame.Player_1.PlayedCards;
                opponentHand = cribbageGame.Player_2.PlayedCards;
                cribbageGame.PlayedCards = new List<Card>();

                displayOpponentHand(opponentHand, true);
                displayPlayerHand(playerHand);
                displayCribCards(true);
                displayPlayedCards();

                btnNextHand.Visibility = Visibility.Visible;
                btnCountCards.Visibility = Visibility.Collapsed;
                btnGo.Visibility = Visibility.Collapsed;
                lblMessageToPlayers.Content = "Click 'Next Hand' to deal next hand.";
            });
        }

        private void StartNewHandMessage(string message, string cribbageGameJson)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);
            playerHand = cribbageGame.Player_1.Hand;
            opponentHand = cribbageGame.Player_2.Hand;
            currentCount = cribbageGame.CurrentCount.ToString();
            signalRMessage = "";
            signalRMessage = message;
            RefreshScreen();
        }

        private void PlayCardMessage(string cribbageGameJson, string message)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);
            playerHand = cribbageGame.Player_1.Hand;
            opponentHand = cribbageGame.Player_2.Hand;
            currentCount = cribbageGame.CurrentCount.ToString();
            signalRMessage = message;
            RefreshScreen();
        }

        public void PlayCard(CribbageGame cribbageGame, Card card)
        {
            try
            {
                if (selectedCards.Count == 1)
                {
                    string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                    string cardJson = JsonConvert.SerializeObject(card);

                    _connection.InvokeAsync("PlayCard", cribbageGameJson, cardJson);
                }
                else
                    lblMessageToPlayers.Content = "Select one card to play.";
            }
            catch (Exception ex)
            {
                lblMessageToPlayers.Content = ex.Message;
            }
        }

        public void PickCardToPlay(CribbageGame cribbageGame)
        {
            try
            {
                string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                _connection.InvokeAsync("PickCardToPlay", cribbageGameJson);
            }
            catch (Exception ex)
            {
                lblMessageToPlayers.Content = ex.Message;
            }
        }

        private void StartGameVsComputerMessage(string message, string cribbageGameJson)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);

            StaThreadWrapper(() =>
            {
                var mainWindow = new MainWindow(cribbageGame, loggedInUser);
                mainWindow.Show();
            });

            Dispatcher.Invoke(() => { this.Close(); });
        }

        private void StartGameVsPlayerMessage(string cribbageGameJson, string message)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);

            StaThreadWrapper(() =>
            {
                var mainWindow = new MainWindow(cribbageGame, loggedInUser);
                mainWindow.ShowDialog();
            });

            Dispatcher.Invoke(() => { this.Close(); });
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
        private void btnReadyToStart_Click(object sender, RoutedEventArgs e)
        {
            if(cribbageGame.Player_1.Id == loggedInUser.Id)
            {
                cribbageGame.Player_1.Ready = true;
            }
            else
            {
                cribbageGame.Player_2.Ready = true;
            }
            btnReadyToStart.Visibility = Visibility.Collapsed;
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (cribbageGame.Complete || cribbageGame.Computer)
            {
                this.Close();
            }
            else
            {
                exitClick = true;
                try
                {
                    string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                    string strUser = JsonConvert.SerializeObject(loggedInUser);
                    _connection.InvokeAsync("QuitGame", cribbageGameJson, strUser);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            if (cribbageGame.Complete || cribbageGame.Computer)
            {
                StaThreadWrapper(() =>
                {
                    var landingPage = new LandingPage(loggedInUser, hasSavedGames, strUserGames);
                    landingPage.ShowDialog();
                });

                Dispatcher.Invoke(() => { this.Close(); });
            }
            else
            {
                mainMenuClick = true;
                try
                {
                    string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                    string strUser = JsonConvert.SerializeObject(loggedInUser);
                    _connection.InvokeAsync("QuitGame", cribbageGameJson, strUser);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void btnSendToCrib_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCards.Count == 2)
            {
                try
                {
                    string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                    string strSelectedCards = JsonConvert.SerializeObject(selectedCards);
                    string userJson = JsonConvert.SerializeObject(loggedInUser);
                    _connection.InvokeAsync("CardsToCrib", cribbageGameJson, strSelectedCards, userJson);

                    btnSendToCrib.Visibility = Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    lblMessageToPlayers.Content = ex.Message;
                }
            }
            else
            {
                lblMessageToPlayers.Content = "Select a total of 2 cards to go to the Crib";
            }
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
            _connection.InvokeAsync("Go", cribbageGameJson);

            btnPlayCard.Visibility = Visibility.Collapsed;    
            btnGo.Visibility = Visibility.Collapsed;
        }

        private void btnPlayCard_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCards.Count == 1)
            {
                try
                {
                    string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                    string strSelectedCard = JsonConvert.SerializeObject(selectedCards);
                    string userJson = JsonConvert.SerializeObject(loggedInUser);
                    _connection.InvokeAsync("PlayCard", cribbageGameJson, strSelectedCard);

                    btnPlayCard.Visibility = Visibility.Collapsed;
                    btnGo.Visibility = Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    lblMessageToPlayers.Content = ex.Message;
                }
            }
            else
            {
                lblMessageToPlayers.Content = "Select one card to play to create a current count sum <= 31.";
            }
        }

        private void btnNextHand_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Reset the cards
                ResetCards();

                UpdateCutCard(cribbageGame);
                displayCribCards();
                displayPlayedCards();
                displayOpponentHand(opponentHand, true);
                displayPlayerHand(playerHand);

                // Send a message to the hub
                string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                _connection.InvokeAsync("NewHand", cribbageGameJson);

                // Fix the screen
                newHand = true;
                btnNextHand.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                lblMessageToPlayers.Content = ex.Message;
            }
        }

        private void ResetCards()
        {
            cribbageGame.PlayedCards.Clear();
            cribbageGame.Player_1.Hand.Clear();
            playerHand.Clear();
            cribbageGame.Player_2.Hand.Clear();
            opponentHand.Clear();
            cribbageGame.Player_1.PlayedCards.Clear();
            cribbageGame.Player_2.PlayedCards.Clear();
            cribbageGame.Crib.Clear();
            cribbageGame.CutCard = null;
        }

        private void btnCountCards_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnGo.Visibility = Visibility.Collapsed;
                lblMessageToPlayers.Content = "Click 'Next Hand' to deal next hand.";

                string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                _connection.InvokeAsync("CountHands", cribbageGameJson);
            }
            catch (Exception ex)
            {

                lblMessageToPlayers.Content = ex.Message;
            }
        }

        private void btnCutDeck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                _connection.InvokeAsync("CutDeck", cribbageGameJson);

                btnCutDeck.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {

                throw;
            }
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

        #region "UpdateScreenMethods"
        //Refresh the screen method 
        //Read the "what to do property" --> goes to the correct method (switch statement) to set the screen
        //Read the player turn --> match player turn id to user id
        private void RefreshScreen()
        {
            Dispatcher.Invoke(() =>
            {
                if (newHand)
                {
                    SetUpGame();

                    UpdateCutCard(cribbageGame);
                    lstMessages.Items.Add(signalRMessage);
                    newHand = false;
                }
                else if (cribbageGame.WhatToDo == "cutdeck")
                {
                    RemoveSelectedItems();
                    displayPlayerHand(playerHand);
                    displayOpponentHand(opponentHand, true);
                    displayCribCards(true);

                    lstMessages.Items.Add(signalRMessage);
                    lblMessageToPlayers.Content = signalRMessage;

                    btnCutDeck.Visibility = Visibility.Visible;
                }
                else if (endGame || cribbageGame.Player_1.Score >= 121 || cribbageGame.Player_2.Score >= 121)
                {
                    // Update the Board
                    EndGame();

                    // Call to SignalR
                    EndGame(cribbageGame);
                }
                else
                {
                    //Player scores updated
                    lblPlayer1Score.Content = cribbageGame.Player_1.Score;
                    lblPlayer2Score.Content = cribbageGame.Player_2.Score;

                    //Current count updated
                    lblCurrentCount.Content = cribbageGame.CurrentCount;

                    //Messages to players updated
                    lstMessages.Items.Add(signalRMessage);
                    lstMessages.SelectedIndex = lstMessages.Items.Count - 1;
                    lstMessages.ScrollIntoView(lstMessages.SelectedItem);

                    lblMessageToPlayers.Content = "Player's Turn: " + cribbageGame.PlayerTurn.DisplayName;

                    //Update the cards, buttons, and selections
                    displayPlayerHand(playerHand);
                    displayOpponentHand(opponentHand, true);
                    displayPlayedCards();
                    displayCribCards(true);
                    UpdateCutCard(cribbageGame);
                    RemoveSelectedItems();
                    UpdateButtonSelection();
                }
            });
        }

        private void EndGame()
        {
            // Player scores updated
            lblPlayer1Score.Content = cribbageGame.Player_1.Score;
            lblPlayer2Score.Content = cribbageGame.Player_2.Score;

            // Current count updated
            lblCurrentCount.Content = cribbageGame.CurrentCount;

            // Messages to players updated
            lstMessages.Items.Add(signalRMessage);
            lstMessages.SelectedIndex = lstMessages.Items.Count - 1;
            lstMessages.ScrollIntoView(lstMessages.SelectedItem);

            lblMessageToPlayers.Content = "Game Over";

            // Update the cards, buttons, and selections
            displayPlayerHand(playerHand);
            displayOpponentHand(opponentHand, true);
            displayPlayedCards();
            displayCribCards(true);
            RemoveSelectedItems();
            UpdateButtonSelection();
        }

        private void RemoveSelectedItems()
        {
            rec1.Visibility = Visibility.Collapsed;
            rec2.Visibility = Visibility.Collapsed;
            rec3.Visibility = Visibility.Collapsed;
            rec4.Visibility = Visibility.Collapsed;
            rec5.Visibility = Visibility.Collapsed;
            rec6.Visibility = Visibility.Collapsed;

            selectedCards = new List<Card>();
        }

        private void UpdateButtonSelection()
        {

            if (!endGame && cribbageGame.GoCount != 2 && (opponentHand.Count >= 1 || playerHand.Count >= 1))
            {
                if (cribbageGame.PlayerTurn.Id == loggedInUser.Id) 
                {
                    if (cribbageGame.WhatToDo == "playcard" && playerHand.Count >= 1)
                    {
                        btnPlayCard.Visibility = Visibility.Visible;
                    }
                    else if(cribbageGame.WhatToDo == "startnewhand")
                    {
                        btnGo.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        btnGo.Visibility = Visibility.Visible;
                    }
                } 
            }
            else if (endGame)
            {
                btnNextHand.Visibility = Visibility.Collapsed;
                btnPlayCard.Visibility = Visibility.Collapsed;
                btnGo.Visibility = Visibility.Collapsed;
                btnCountCards.Visibility = Visibility.Collapsed;
                btnCutDeck.Visibility = Visibility.Collapsed;
                btnSendToCrib.Visibility = Visibility.Collapsed;

                // Main Menu & Exit buttons
                btnMainMenu.Visibility = Visibility.Visible;
                btnExit.Visibility = Visibility.Visible;
            }
            else
            {
                btnCountCards.Visibility = Visibility.Visible;
                lblMessageToPlayers.Content = "Click 'Count Cards' to count the cards.";
            }
        }

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
            else
            {
                BitmapImage card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                card.EndInit();
                imgCutCard.Source = card;
            }
        }

        private void displayPlayedCards()
        {
            BitmapImage card = new BitmapImage();
            if (cribbageGame.PlayedCards.Count >= 1)
            {
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + cribbageGame.PlayedCards[0].imgPath);
                card.EndInit();
                imgPlayedCard1.Source = card;
            }
            else
                imgPlayedCard1.Source = null;
            if (cribbageGame.PlayedCards.Count >= 2)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + cribbageGame.PlayedCards[1].imgPath);
                card.EndInit();
                imgPlayedCard2.Source = card;
            }
            else
                imgPlayedCard2.Source = null;
            if (cribbageGame.PlayedCards.Count >= 3)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + cribbageGame.PlayedCards[2].imgPath);
                card.EndInit();
                imgPlayedCard3.Source = card;
            }
            else
                imgPlayedCard3.Source = null;
            if (cribbageGame.PlayedCards.Count >= 4)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + cribbageGame.PlayedCards[3].imgPath);
                card.EndInit();
                imgPlayedCard4.Source = card;
            }
            else
                imgPlayedCard4.Source = null;
            if (cribbageGame.PlayedCards.Count >= 5)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + cribbageGame.PlayedCards[4].imgPath);
                card.EndInit();
                imgPlayedCard5.Source = card;
            }
            else
                imgPlayedCard5.Source = null;
            if (cribbageGame.PlayedCards.Count >= 6)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + cribbageGame.PlayedCards[5].imgPath);
                card.EndInit();
                imgPlayedCard6.Source = card;
            }
            else
                imgPlayedCard6.Source = null;
            if (cribbageGame.PlayedCards.Count >= 7)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + cribbageGame.PlayedCards[6].imgPath);
                card.EndInit();
                imgPlayedCard7.Source = card;
            }
            else
                imgPlayedCard7.Source = null;
            if (cribbageGame.PlayedCards.Count >= 8)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + cribbageGame.PlayedCards[7].imgPath);
                card.EndInit();
                imgPlayedCard8.Source = card;
            }
            else
                imgPlayedCard8.Source = null;
        }

        private void displayCribCards(bool isShown = false)
        {
            BitmapImage card = new BitmapImage();
            if (isShown)
            {
                if (cribbageGame.Crib.Count >= 1)
                {

                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + cribbageGame.Crib[0].imgPath);
                    card.EndInit();
                    imgCribCard1.Source = card;
                }
                else
                    imgCribCard1.Source = null;
                if (cribbageGame.Crib.Count >= 2)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + cribbageGame.Crib[1].imgPath);
                    card.EndInit();
                    imgCribCard2.Source = card;
                }
                else
                    imgCribCard2.Source = null;
                if (cribbageGame.Crib.Count >= 3)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + cribbageGame.Crib[2].imgPath);
                    card.EndInit();
                    imgCribCard3.Source = card;
                }
                else
                    imgCribCard3.Source = null;
                if (cribbageGame.Crib.Count >= 4)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + cribbageGame.Crib[3].imgPath);
                    card.EndInit();
                    imgCribCard4.Source = card;
                }
                else
                    imgCribCard4.Source = null;
            }
            else
            {
                if (cribbageGame.Crib.Count >= 1)
                {

                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    imgCribCard1.Source = card;
                }
                else
                    imgCribCard1.Source = null;
                if (cribbageGame.Crib.Count >= 2)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    imgCribCard2.Source = card;
                }
                else
                    imgCribCard2.Source = null;
                if (cribbageGame.Crib.Count >= 3)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    imgCribCard3.Source = card;
                }
                else
                    imgCribCard3.Source = null;
                if (cribbageGame.Crib.Count >= 4)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    imgCribCard4.Source = card;
                }
                else
                    imgCribCard4.Source = null;
            }
        }
        #endregion

        //testing button
        //private void btnEndGame_Click(object sender, RoutedEventArgs e)
        //{
        //    cribbageGame.Player_1.Score = 121;
        //    endGame = true;

        //    signalRMessage = "Game Over.\nWinner: " + cribbageGame.Player_1.DisplayName;
        //    EndGame();
        //    EndGame(cribbageGame);
        //}
    }
}