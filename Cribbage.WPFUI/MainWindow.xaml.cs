﻿using Cribbage.BL;
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
        UserGame game;
        User loggedInUser = new User();
        CribbageGame cribbageGame;
        //SignalRConnection cribbageHubConnection;
        User secondPlayer = new User();
        HubConnection _connection;
        List<Card> opponentHand;
        List<Card> playerHand;

        //Refresh the screen method needed 
        //Read the "what to do property" --> goes to the correct method (switch statement) to set the screen
        //Read the player turn --> match player turn id to user id

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
                if (opponentHand.Count >= 2)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + opponentHand[1].imgPath);
                    card.EndInit();
                    imgOppenentCard2.Source = card;
                }
                if (opponentHand.Count >= 3)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + opponentHand[2].imgPath);
                    card.EndInit();
                    imgOppenentCard3.Source = card;
                }
                if (opponentHand.Count >= 4)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + opponentHand[3].imgPath);
                    card.EndInit();
                    imgOppenentCard4.Source = card;
                }
                if (opponentHand.Count >= 5)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + opponentHand[4].imgPath);
                    card.EndInit();
                    imgOppenentCard5.Source = card;
                }
                if (opponentHand.Count >= 6)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + opponentHand[5].imgPath);
                    card.EndInit();
                    imgOppenentCard6.Source = card;
                }
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
                if (opponentHand.Count >= 2)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    imgOppenentCard2.Source = card;
                }
                if (opponentHand.Count >= 3)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    imgOppenentCard3.Source = card;
                }
                if (opponentHand.Count >= 4)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    imgOppenentCard4.Source = card;
                }
                if (opponentHand.Count >= 5)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    imgOppenentCard5.Source = card;
                }
                if (opponentHand.Count >= 6)
                {
                    card = new BitmapImage();
                    card.BeginInit();
                    card.UriSource = new Uri("pack://siteoforigin:,,,/images/cardBackBlue.png");
                    card.EndInit();
                    imgOppenentCard6.Source = card;
                }
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
            if (playerHand.Count >= 2)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + playerHand[1].imgPath);
                card.EndInit();
                imgPlayerCard2.Source = card;
            }
            if (playerHand.Count >= 3)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + playerHand[2].imgPath);
                card.EndInit();
                imgPlayerCard3.Source = card;
            }
            if (playerHand.Count >= 4)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + playerHand[3].imgPath);
                card.EndInit();
                imgPlayerCard4.Source = card;
            }
            if (playerHand.Count >= 5)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + playerHand[4].imgPath);
                card.EndInit();
                imgPlayerCard5.Source = card;
            }
            if (playerHand.Count >= 6)
            {
                card = new BitmapImage();
                card.BeginInit();
                card.UriSource = new Uri("pack://siteoforigin:,,,/images/" + playerHand[5].imgPath);
                card.EndInit();
                imgPlayerCard6.Source = card;
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
