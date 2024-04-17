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
            _connection.On<bool, string>("SavedGames", (isSuccess, userGamesJson) => SavedGamesMessage(isSuccess, userGamesJson));
            _connection.On<string>("StartGame", (message) => StartGameVsComputerMessage(message));
            _connection.On<string>("StartGameVsPlayer", (message) => StartGameVsPlayerMessage(message));

            _connection.StartAsync();
        }

        private void StartGameVsPlayerMessage(string message)
        {
            MessageBox.Show(message);
            //LandingPage.StartGameVsPlayer(message);
        }

        public void NewGameVsPlayer(User user)
        {
            try
            {
                string strUser = JsonConvert.SerializeObject(user);
                _connection.InvokeAsync("NewGameVsPlayer", strUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StartGameVsComputerMessage(string message)
        {
            MessageBox.Show(message);
            //LandingPage.StartGameVsComputer(message);
        }

        public void NewGameVsComputer(User user)
        {
            Start();
            try
            {
                string strUser = JsonConvert.SerializeObject(user);
                _connection.InvokeAsync("NewGameVsComputer", strUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SavedGamesMessage(bool isSuccess, string userGamesJson)
        {
            if(isSuccess)
            {
                LandingPage.SavedGamesCheck(isSuccess, userGamesJson);
            }
            else
            {
                LandingPage.SavedGamesCheck(isSuccess, userGamesJson);
            }
        }

        public void GetSavedGames(User user)
        {
            try
            {
                string strUser = JsonConvert.SerializeObject(user);
                _connection.InvokeAsync("GetSavedGames", strUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateUserMessage(bool isSuccess, string message)
        {
            if (isSuccess)
            {
                WPFUI.Login.CreateUserCheck(isSuccess);
            }
            else // not logged in
            {
                WPFUI.Login.CreateUserCheck(isSuccess);
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
                MessageBox.Show("Got logged in");
                WPFUI.Login.LoggedInCheck(isLoggedIn, userJson);
            }
            else // not logged in
            {
                MessageBox.Show("Failed log in");
                WPFUI.Login.LoggedInCheck(isLoggedIn, userJson);
            }
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

        private void OnSend(string user, string message)
        {
            Console.WriteLine(user + ": " + message);
        }
    }
}