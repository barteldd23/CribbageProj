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
        BL.Models.User user; //pass the user info from Login to LandingPage

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
            string email = txtLoginEmail.Text.Trim();
            string password = pbxPasswordBox.ToString().Trim();

            BL.Models.User user = new BL.Models.User();

            user.Email = email;
            user.Password = password;

            if(user.Email != string.Empty && user.Password != string.Empty)
            {
                CribbageHub cribbageHub = new CribbageHub();

                cribbageHub.Login(email, password);

                LandingPage landingPage = new LandingPage();
                landingPage.Show();
                this.Close();
            }
            else
            {
                lblError.Foreground = new SolidColorBrush(Colors.DarkMagenta);
                lblError.Content = "Please enter an email and password."; 
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // need to set up to register a user
            this.Close();
        }

    }
}
