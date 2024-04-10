using Cribbage.API.Controllers;
using Cribbage.BL.Models;
using Cribbage.PL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;
using Microsoft.VisualBasic.ApplicationServices;
using Cribbage.API.Hubs;

namespace Cribbage.WPFUI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        BL.Models.User user = new BL.Models.User(); //pass the user info from Login to LandingPage
        //string hubAddress = "https://bigprojectapi-300089145.azurewebsites.net/CribbageHub";
        string hubAddress = "https://localhost:7186/CribbageHub";

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
                    cribbageHubConnection.Start();
                    cribbageHubConnection.Login(user);




                    //this.Close();
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

        public static void changePage()
        {
            MessageBox.Show("Change page called!");
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegistrationPage registrationPage = new RegistrationPage();
            registrationPage.Show();

            this.Close();
        }

    }
}
