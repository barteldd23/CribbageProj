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
            if (cribbageGame.Dealer == 1) dealer = cribbageGame.Player_1.DisplayName;
            else dealer = cribbageGame.Player_2.DisplayName;

            rec1.Visibility = Visibility.Collapsed;
            rec2.Visibility = Visibility.Collapsed;
            rec3.Visibility = Visibility.Collapsed;
            rec4.Visibility = Visibility.Collapsed;
            rec5.Visibility = Visibility.Collapsed;
            rec6.Visibility = Visibility.Collapsed;

            btnRefreshScreen.Visibility = Visibility.Collapsed;
            btnNextHand.Visibility = Visibility.Collapsed;
            btnPlayCard.Visibility = Visibility.Collapsed;
            btnGo.Visibility = Visibility.Collapsed;
            btnCountCards.Visibility = Visibility.Collapsed;
            btnCutDeck.Visibility = Visibility.Collapsed;
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
            _connection.On<string, string>("StartNewHand", (message, cribbageGameJson) => StartNewHandMessage(message, cribbageGameJson));
            _connection.On<string, string>("CutCard", (cribbageGameJson, message) => CutCardMessage(cribbageGameJson, message));
            _connection.On<string, string>("CardCut", (cribbageGameJson, message) => CardCutMessage(cribbageGameJson, message));
            _connection.On<string, string>("Action", (cribbageGameJson, message) => PlayCardMessage(cribbageGameJson, message));
            _connection.On<string, string>("HandsCounted", (cribbageGameJson, message) => HandsCountedMessage(cribbageGameJson, message));
            _connection.On<string>("GameFinished", (cribbageGameJson) => GameFinishedMessage(cribbageGameJson));
            

            _connection.StartAsync();
        }

        private void CardCutMessage(string cribbageGameJson, string message)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);
            signalRMessage = message;
        }

        private void HandsCountedMessage(string cribbageGameJson, string message)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);
            signalRMessage = message;
        }

        private void StartNewHandMessage(string message, string cribbageGameJson)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);
            playerHand = cribbageGame.Player_1.Hand;
            opponentHand = cribbageGame.Player_2.Hand;
            currentCount = cribbageGame.CurrentCount.ToString();
            signalRMessage = "";
            signalRMessage = message;
            newHand = true;
        }

        private void PlayCardMessage(string cribbageGameJson, string message)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);
            playerHand = cribbageGame.Player_1.Hand;
            opponentHand = cribbageGame.Player_2.Hand;
            currentCount = cribbageGame.CurrentCount.ToString();
            signalRMessage = message;
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
                    MessageBox.Show("Select one card to play.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
            }
        }

        private void GameFinishedMessage(string cribbageGameJson)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);

            MessageBox.Show(cribbageGame.Winner.ToString());
        }

        private void CutCardMessage(string cribbageGameJson, string message)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);
            signalRMessage = message;
        }

        private void StartGameVsComputerMessage(string message, string cribbageGameJson)
        {
            cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(cribbageGameJson);

            MessageBox.Show(message + " " + cribbageGame.WhatToDo);

            StaThreadWrapper(() =>
            {
                var mainWindow = new MainWindow(cribbageGame, loggedInUser);
                mainWindow.ShowDialog();
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
            if (cribbageGame.Computer)
            {
                try
                {
                    string strUser = JsonConvert.SerializeObject(loggedInUser);
                    _connection.InvokeAsync("NewGameVsComputer", strUser);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            StaThreadWrapper(() =>
            {
                var mainWindow = new MainWindow(cribbageGame, loggedInUser);
                mainWindow.ShowDialog();
            });

            // need to save prior to closing 
            this.Hide();
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

                    btnRefreshScreen.Visibility = Visibility.Visible;
                    btnSendToCrib.Visibility = Visibility.Collapsed;

                    lblMessageToPlayers.Content = "Click 'Refresh Screen' to update the screen.";
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
            string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
            _connection.InvokeAsync("Go", cribbageGameJson);

            btnPlayCard.Visibility = Visibility.Collapsed;    
            btnGo.Visibility = Visibility.Collapsed;

            lblMessageToPlayers.Content = "Click 'Refresh Screen' to update the screen.";
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

                    btnRefreshScreen.Visibility = Visibility.Visible;
                    btnPlayCard.Visibility = Visibility.Collapsed;
                    btnGo.Visibility = Visibility.Collapsed;

                    lblMessageToPlayers.Content = "Click 'Refresh Screen' to update the screen.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Select one card to play to create a current count sum <= 31.");
            }
        }

        private void btnNextHand_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                _connection.InvokeAsync("NewHand", cribbageGameJson);

                btnNextHand.Visibility = Visibility.Collapsed;

                lblMessageToPlayers.Content = "Click 'Refresh Screen' to update the screen.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCountCards_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                _connection.InvokeAsync("CountHands", cribbageGameJson);

                playerHand = cribbageGame.Player_1.PlayedCards;
                opponentHand = cribbageGame.Player_2.PlayedCards;
                cribbageGame.PlayedCards = new List<Card>();

                displayOpponentHand(opponentHand, true);
                displayPlayerHand(playerHand);
                displayCribCards(true);
                displayPlayedCards();

                btnNextHand.Visibility = Visibility.Visible;
                btnCountCards.Visibility = Visibility.Collapsed;

                lblMessageToPlayers.Content = "Click 'Next Hand' to deal next hand.";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnCutDeck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                _connection.InvokeAsync("CutDeck", cribbageGameJson);

                btnCutDeck.Visibility = Visibility.Collapsed;
                lblMessageToPlayers.Content = "Click 'Refresh Screen' to update the screen.";
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
        private void btnRefreshScreen_Click(object sender, RoutedEventArgs e)
        {
            if(newHand)
            {
                SetUpGame();
                UpdateCutCard(cribbageGame);
                newHand = false;
            }
            else if(cribbageGame.WhatToDo == "cutdeck")
            {
                lblMessageToPlayers.Content = signalRMessage;
                btnCutDeck.Visibility = Visibility.Visible;
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
                this.lstMessages.SelectedIndex = this.lstMessages.Items.Count - 1;
                lblMessageToPlayers.Content = " Player's Turn: " + cribbageGame.PlayerTurn.DisplayName;

                //Update the cards, buttons, and selections
                displayPlayerHand(playerHand);
                displayOpponentHand(opponentHand, true);
                displayPlayedCards();
                displayCribCards(true);
                UpdateCutCard(cribbageGame);
                RemoveSelectedItems();
                UpdateButtonSelection();
            }
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

            if (cribbageGame.GoCount != 2 && (opponentHand.Count >= 1 || playerHand.Count >= 1))
            {
                if (cribbageGame.PlayerTurn.Id == loggedInUser.Id) 
                {
                    if (cribbageGame.WhatToDo == "playcard" && playerHand.Count >= 1)
                    {
                        btnPlayCard.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        btnGo.Visibility = Visibility.Visible;
                    }
                } 
            }
            else
            {
                btnCountCards.Visibility = Visibility.Visible;
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


    }
}
