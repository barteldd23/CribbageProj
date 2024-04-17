using Cribbage.BL.Models;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Cribbage.WPFUI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary> 
    public partial class Login : Window
    {
        //string hubAddress = "https://bigprojectapi-300089145.azurewebsites.net/CribbageHub";
        string hubAddress = "https://localhost:7186/CribbageHub";
        Window window1;
        //public delegate void Callback(string message);
        public delegate void Callback();
        //public static System.Windows.Threading.Dispatcher
        //    CurrentDispatcher
        //{ get; }

        //public System.Threading.Thread Thread { get; set;  }

        public Login()
        {
            InitializeComponent();
            LoginVisible();
        }

        // Create a method for a delegate.
        public static void DelegateMethod()
        {
            CloseAllWindows();
            //Window window = new Window(this);
            //MessageBox.Show(message);
        }



        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = txtLoginEmail.Text.Trim();
                string password = pbxPasswordBox.Password.ToString().Trim();

                User user = new User();

                user.Email = email;
                user.Password = password;

                if (email != string.Empty && password != string.Empty)
                {
                    // Start the hub connection
                    SignalRConnection cribbageHubConnection = new SignalRConnection(hubAddress);
                    cribbageHubConnection.Login(user);
                    //Window window1 = new Window();
                    //window1.Close();
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

        //opens the landing page if the user is logged in successfully
        public static void LoggedInCheck(bool loggedIn, string userJson) 
        {
            if(loggedIn)
            {
                User loggedInUser = new User();
                loggedInUser = JsonConvert.DeserializeObject<User>(userJson);

                StaThreadWrapper(() =>
                {
                    // Instantiate the delegate.
                    //Callback handler = DelegateMethod;

                    // Call the delegate.
                    //handler("Hello World");
                    //handler();
                    //Dispatcher.BeginInvoke();
                    //Thread currentThread = Thread.CurrentThread;
                    //MessageBox.Show(currentThread.ToString());

                    //Thread.BeginThreadAffinity();

                    //Dispatcher currentDispatcher = CurrentDispatcher;

                    //MessageBox.Show(currentDispatcher.ToString());

                    //CloseAllWindows();
                    var landingPage = new LandingPage(loggedInUser);
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
            RegistrationVisible();
        }

        private void RegistrationVisible()
        {
            txtFirstName.Visibility = Visibility.Visible;
            txtLastName.Visibility = Visibility.Visible;
            txtDisplayName.Visibility = Visibility.Visible;
            txtEmail.Visibility = Visibility.Visible;
            pbxSetPassword.Visibility = Visibility.Visible;
            pbxReEnterPassword.Visibility = Visibility.Visible;
            btnRegisterSubmit.Visibility = Visibility.Visible;
            btnBack.Visibility = Visibility.Visible;
            lblFirstName.Visibility = Visibility.Visible;
            lblLastName.Visibility = Visibility.Visible;
            lblDisplayName.Visibility = Visibility.Visible;
            lblRegisterEmail.Visibility = Visibility.Visible;
            lblRegisterPassword.Visibility = Visibility.Visible;
            lblReEnterPassword.Visibility = Visibility.Visible;
            lblNewUserRegistration.Visibility = Visibility.Visible;
            lblRegisterError.Content = "";
            lblRegisterError.Visibility = Visibility.Visible;

            txtFirstName.Focus();

            txtLoginEmail.Visibility = Visibility.Collapsed;
            pbxPasswordBox.Visibility = Visibility.Collapsed;
            btnLogin.Visibility = Visibility.Collapsed;
            btnExit.Visibility = Visibility.Collapsed;
            lblEmail.Visibility = Visibility.Collapsed;
            lblPassword.Visibility = Visibility.Collapsed;
            lblReturningUser.Visibility = Visibility.Collapsed;
            btnRegister.Visibility = Visibility.Collapsed;
            lblNewUser.Visibility = Visibility.Collapsed;
            lblError.Content = "";
            lblError.Visibility = Visibility.Collapsed;
        }

        public static void CreateUserCheck(bool isSuccess)
        {
            if (isSuccess)
            {
                MessageBox.Show("User created");
                StaThreadWrapper(() =>
                {
                    //    window1.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    //new UpdateUIDelegate(
                    //    {
                    //        for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                    //            App.Current.Windows[intCounter].Close();
                    //    });

   

                    var login = new Login();
                    login.Show();
                });
            }
            else
            {
                MessageBox.Show("Cannot create user");
            }
        }

        private static void CloseAllWindows()
        {
            {
                for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                    App.Current.Windows[intCounter].Hide();
            }
        }

        private void LoginVisible()
        {
            txtFirstName.Visibility = Visibility.Collapsed;
            txtLastName.Visibility = Visibility.Collapsed;
            txtDisplayName.Visibility = Visibility.Collapsed;
            txtEmail.Visibility = Visibility.Collapsed;
            pbxSetPassword.Visibility = Visibility.Collapsed;
            pbxReEnterPassword.Visibility = Visibility.Collapsed;
            btnRegisterSubmit.Visibility = Visibility.Collapsed;
            btnBack.Visibility = Visibility.Collapsed;
            lblFirstName.Visibility = Visibility.Collapsed;
            lblLastName.Visibility = Visibility.Collapsed;
            lblDisplayName.Visibility = Visibility.Collapsed;
            lblRegisterEmail.Visibility = Visibility.Collapsed;
            lblRegisterPassword.Visibility = Visibility.Collapsed;
            lblReEnterPassword.Visibility = Visibility.Collapsed;
            lblNewUserRegistration.Visibility = Visibility.Collapsed;
            lblRegisterError.Content = "";
            lblRegisterError.Visibility = Visibility.Collapsed;

            txtLoginEmail.Visibility = Visibility.Visible;
            pbxPasswordBox.Visibility = Visibility.Visible;
            btnLogin.Visibility = Visibility.Visible;
            btnExit.Visibility = Visibility.Visible;
            lblEmail.Visibility = Visibility.Visible;
            lblPassword.Visibility = Visibility.Visible;
            lblReturningUser.Visibility = Visibility.Visible;
            btnRegister.Visibility = Visibility.Visible;
            lblNewUser.Visibility = Visibility.Visible;
            lblError.Content = "";
            lblError.Visibility = Visibility.Visible;

            txtLoginEmail.Focus();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            LoginVisible();
        }

        private void btnRegisterSubmit_Click(object sender, RoutedEventArgs e)
        {
            //need to test that the registration works before opening a new game and closing the page
            try
            {
                string firstName = txtFirstName.Text.Trim();
                string lastName = txtLastName.Text.Trim();
                string displayName = txtDisplayName.Text.Trim();
                string email = txtEmail.Text.Trim();
                string setPassword = pbxSetPassword.Password.ToString().Trim();
                string checkPassword = pbxReEnterPassword.Password.ToString().Trim();

                User user = new User();

                user.FirstName = firstName;
                user.LastName = lastName;
                user.DisplayName = displayName;
                user.Email = email;

                if (setPassword == checkPassword)
                {
                    user.Password = setPassword;
                }
                else
                {
                    user.Password = string.Empty;
                }

                if (firstName != string.Empty && lastName != string.Empty
                    && displayName != string.Empty && email != string.Empty
                    && user.Password != string.Empty)
                {
                    // Start the hub connection
                    SignalRConnection cribbageHubConnection = new SignalRConnection(hubAddress);
                    cribbageHubConnection.RegisterUser(user);
                }
                else
                {
                    lblRegisterError.Foreground = new SolidColorBrush(Colors.DarkMagenta);
                    lblRegisterError.Content = "Unable to register.";
                }
            }
            catch (Exception ex)
            {
                lblRegisterError.Foreground = new SolidColorBrush(Colors.DarkMagenta);
                lblRegisterError.Content = ex.Message;

                throw;
            }
        }
    }
}
