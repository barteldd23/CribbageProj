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

        public MainWindow(User user)
        {
            InitializeComponent();
            //if(lblPlayer1DisplayName.Content == "Player1 DisplayName")
            //{
            //    lblPlayer1DisplayName.Content = user.DisplayName;
            //    cribbageGame.Player_1 = (Player)user;
            //}
            //else
            //{
            //    lblPlayer2DisplayName.Content = user.DisplayName;
            //    cribbageGame.Player_2 = (Player)user;
            //}
            loggedInUser = user;
            //MessageBox.Show("User " + user.FullName);
        }

        private void QuitGame_Click(object sender, RoutedEventArgs e)
        {
            // need to save prior to closing 
            this.Close();
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(loggedInUser);
            mainWindow.Show();

            // need to save prior to closing 
            this.Close();
        }

    }
}
