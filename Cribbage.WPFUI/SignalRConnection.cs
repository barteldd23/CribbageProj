using Cribbage.BL.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Windows;

namespace Cribbage.WPFUI
{
    public class SignalRConnection
    {
        private string hubAddress;
        HubConnection _connection;
        string user;

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

            _connection.StartAsync();
        }

        private void ReceivedLoginMessage(bool isLoggedIn, string message, string userJson)
        {
            if(isLoggedIn)
            {
                MessageBox.Show("logged in: " + message + " " + userJson);
                WPFUI.Login.changePage();
                //return userJson;
            }
            else // not logged in
            {
                MessageBox.Show("not logged in: " + message + " " + userJson);
                //return userJson;
            }
        }

        private void OnSend(string user, string message)
        {
            Console.WriteLine(user + ": " + message);
        }

        public void Login(User user)
        {
            Start();
            string message = user.Email + " logged in";
            try
            {
                _connection.InvokeAsync("Login", user.Email, user.Password);
                //return Tuple.Create(true, message);
                //_connection.SendMessage("System", message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //return Tuple.Create(false, ex.Message);
            }
        }
    }
}