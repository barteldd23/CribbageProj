using Cribbage.BL.Models;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace Cribbage.WPFUI
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Window 
    {
        //string hubAddress = "https://bigprojectapi-300089145.azurewebsites.net/CribbageHub";
        string hubAddress = "https://localhost:7186/CribbageHub";

        public RegistrationPage()
        {
            InitializeComponent();
        }

        public static void CreateUserCheck(bool isSuccess)
        {
            if (isSuccess)
            {
                MessageBox.Show("User created");
            }
            else
            {
                MessageBox.Show("Cannot create user");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //Login login = new Login();
            //login.Show();
            this.Close();
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
                    //cribbageHubConnection.Start();
                    cribbageHubConnection.RegisterUser(user);

                    //MessageBox.Show("User Registered");
                    //this.Close();
                }
                else
                {
                    lblError.Foreground = new SolidColorBrush(Colors.DarkMagenta);
                    lblError.Content = "Unable to register.";
                }
            }
            catch (Exception ex)
            {
                lblError.Foreground = new SolidColorBrush(Colors.DarkMagenta);
                lblError.Content = ex.Message;

                throw;
            }
        }

    }
}
