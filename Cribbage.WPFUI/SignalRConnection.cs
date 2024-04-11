using Cribbage.BL.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Windows;

namespace Cribbage.WPFUI
{
    public class SignalRConnection
    {
        private string hubAddress;
        HubConnection _connection;

        public SignalRConnection(string hubAddress)
        {
            this.hubAddress = hubAddress;
        }

        public void Start()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(hubAddress)
                .Build();

            _connection.On<string, string>("ReceiveMessage", (s1, s2) => OnSend(s1, s2));
            _connection.On<bool, string, string>("LogInAttempt", (isLoggedIn, message, userJson) => ReceivedLoginMessage(isLoggedIn, message, userJson));
            _connection.On<bool, string>("CreateUserAttempt", (isSuccess, message) => CreateUserMessage(isSuccess, message));

            _connection.StartAsync();
        }

        private void CreateUserMessage(bool isSuccess, string message)
        {
            if (isSuccess)
            {
                MessageBox.Show(message + " successfully created user");
                //RegistrationPage.CreateUserCheck(isSuccess);
            }
            else // not logged in
            {
                MessageBox.Show(message + " did not create user");
                //RegistrationPage.CreateUserCheck(isSuccess);
            }
        }

        public void RegisterUser(User user)
        {
            string strUser = JsonConvert.SerializeObject(user);
            Start();
            try
            {
                _connection.InvokeAsync("CreateUser", strUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReceivedLoginMessage(bool isLoggedIn, string message, string userJson)
        {
            if(isLoggedIn)
            {
                WPFUI.Login.LoggedInCheck(isLoggedIn);
            }
            else // not logged in
            {
                WPFUI.Login.LoggedInCheck(isLoggedIn);
            }
        }

        private void OnSend(string user, string message)
        {
            Console.WriteLine(user + ": " + message);
        }

        public void Login(User user)
        {
            Start();
            try
            {
                _connection.InvokeAsync("Login", user.Email, user.Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}