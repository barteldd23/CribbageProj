using Microsoft.Data.SqlClient;
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

namespace Cribbage.WPFUI
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Window
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();

            this.Close();
        }

        private void btnRegisterSubmit_Click(object sender, RoutedEventArgs e)
        {
            //need to test that the registration works before opening a new game and closing the page
            try
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                this.Close();
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
