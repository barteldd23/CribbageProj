using Cribbage.BL.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Media;

namespace Cribbage.WPFUI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary> 
    public partial class Login : Window
    {
        //string hubAddress = "https://bigprojectapi-300089145.azurewebsites.net/CribbageHub";
        string hubAddress = "https://localhost:7186/CribbageHub";
        HubConnection _connection;
        User loggedInUser = new User();

        public Login()
        {
            
            Start();
            InitializeComponent();
            this.MouseLeftButtonDown += delegate { DragMove(); };
            txtLoginEmail.Focus();
            txtLoginEmail.Text = "tester@gmail.com";
            pbxPasswordBox.Password = "maple";
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = txtLoginEmail.Text.Trim();
                //string password = pbxPasswordBox.Password.ToString().Trim();
                string password = "maple";

                User user = new User();

                user.Email = email;
                user.Password = password;

                if (email != string.Empty && password != string.Empty)
                {
                    UserLogin(email, password);

                    this.Close();
                }
                else
                {
                    lblError.Foreground = new SolidColorBrush(Colors.DarkMagenta);
                    lblError.Content = "Please enter an email and password.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();
        }

        #region "SignalRConnection"

        private void Start()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(hubAddress)
                .Build();

            _connection.On<bool, bool, string, string, string>("LogInAttempt", (isLoggedIn, isSuccess, message, userJson, userGamesJson) => ReceivedLoginMessage(isLoggedIn, isSuccess, message, userJson, userGamesJson));

            _connection.StartAsync();
        }

        private void UserLogin(string email, string password)
        {
            try
            {
                _connection.InvokeAsync("Login", email, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReceivedLoginMessage(bool isLoggedIn, bool isSuccess, string message, string userJson, string userGamesJson)
        {
            if (isLoggedIn)
            {
                loggedInUser = JsonConvert.DeserializeObject<User>(userJson);

                StaThreadWrapper(() =>
                {
                    var landingPage = new LandingPage(loggedInUser, isSuccess, userGamesJson);
                    landingPage.ShowDialog();
                });

                Dispatcher.Invoke(() => { this.Close(); });
            }
            else // not logged in
            {
                MessageBox.Show("Failed log in");
                StaThreadWrapper(() =>
                {
                    var login = new Login();
                    login.ShowDialog();
                });
            }
        }
        #endregion
    }
}
