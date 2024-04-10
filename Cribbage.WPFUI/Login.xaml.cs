using Cribbage.BL.Models;
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
        User user = new User(); //pass the user info from Login to LandingPage

        public Login()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = txtLoginEmail.Text.Trim();
                string password = pbxPasswordBox.Password.ToString().Trim();

                user.Email = email;
                user.Password = password;

                if (email != string.Empty && password != string.Empty)
                {
                    // Start the hub connection
                    SignalRConnection cribbageHubConnection = new SignalRConnection(hubAddress);
                    //cribbageHubConnection.Start();
                    cribbageHubConnection.Login(user);
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
            t.Start();
        }

        //opens the landing page if the user is logged in successfully
        public static void LoggedInCheck(bool loggedIn) 
        {
            if(loggedIn)
            {
                StaThreadWrapper(() =>
                {
                    var landingPage = new LandingPage();
                    landingPage.Show();
                });
            }
            else
            {
                MessageBox.Show("Cannot log in with the provided credentials");
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegistrationPage registrationPage = new RegistrationPage();
            registrationPage.Show();
        }

    }
}
