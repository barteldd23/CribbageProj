using Cribbage.BL.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Cribbage.WPFUI
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        string hubAddress = "https://bigprojectapi-300089145.azurewebsites.net/CribbageHub";
        //string hubAddress = "https://localhost:7186/CribbageHub";
        HubConnection _connection;

        public Register()
        {
            Start();
            InitializeComponent();
            this.MouseLeftButtonDown += delegate { DragMove(); };
            txtFirstName.Focus();
        }

        private void btnRegisterSubmit_Click(object sender, RoutedEventArgs e)
        {
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

                    if (firstName != string.Empty && lastName != string.Empty
                        && displayName != string.Empty && email != string.Empty
                        && user.Password != string.Empty)
                    {
                        RegisterUser(user);
                    }
                    else
                    {
                        lblRegisterError.Foreground = new SolidColorBrush(Colors.Red);
                        lblRegisterError.Content = "Please complete all of the fields";
                    }
                }
                else
                {
                    user.Password = string.Empty;
                    lblRegisterError.Foreground = new SolidColorBrush(Colors.Red);
                    lblRegisterError.Content = "Passwords do not match";
                }
            }
            catch (Exception ex)
            {
                lblRegisterError.Foreground = new SolidColorBrush(Colors.Red);
                lblRegisterError.Content = ex.Message;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region "SignalRConnection"

        public void Start()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(hubAddress)
                .Build();

            _connection.On<bool, string>("CreateUserAttempt", (isSuccess, message) => CreateUserMessage(isSuccess, message));

            _connection.StartAsync();
        }


        public void CreateUserMessage(bool isSuccess, string message)
        {
            if (isSuccess)
            {
                Dispatcher.Invoke(() =>
                {
                    this.Close();
                });
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    lblRegisterError.Foreground = new SolidColorBrush(Colors.Red);
                    lblRegisterError.Content = message;
                });
            }
        }

        private void RegisterUser(User user)
        {
            try
            {
                string userJson = JsonConvert.SerializeObject(user);
                _connection.InvokeAsync("CreateUser", userJson);
            }
            catch (Exception ex)
            {

                lblRegisterError.Foreground = new SolidColorBrush(Colors.Red);
                lblRegisterError.Content = ex.Message;
            }
        }
        #endregion
    }
}
