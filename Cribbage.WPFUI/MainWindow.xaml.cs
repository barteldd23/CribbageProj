using Cribbage.BL.Models;
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
        User loggedInUser;
        CribbageGame cribbageGame;
        SignalRConnection cribbageHubConnection;
        Player secondPlayer;

        public MainWindow(User user, Player player)
        {
            InitializeComponent();
            secondPlayer = player;
            loggedInUser = user;

            // need to convert User to Player --> it keeps crashing

            //cribbageGame = new CribbageGame();

            if (player.Email == "computer@computer.computer")
            {
                lblPlayer1DisplayName.Content = player.DisplayName;
                cribbageGame.Player_1 = player;

                lblPlayer2DisplayName.Content = user.DisplayName;

                //cribbageGame.Player_2 = (Player)user;
            }
            else if (cribbageGame.Player_1 == null)
            {
                lblPlayer1DisplayName.Content = user.DisplayName;
                //cribbageGame.Player_1 = (Player)user;
            }
            else
            {
                lblPlayer2DisplayName.Content = user.DisplayName;
                //cribbageGame.Player_2 = (Player)user;
            }

            // Start the hub connection
            cribbageHubConnection = new SignalRConnection(hubAddress);
        }

        private void QuitGame_Click(object sender, RoutedEventArgs e)
        {
            // need to save prior to closing 
            this.Close();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(loggedInUser, secondPlayer);
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
