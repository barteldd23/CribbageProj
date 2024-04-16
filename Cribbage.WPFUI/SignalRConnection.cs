﻿using Cribbage.BL.Models;
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
            _connection.On<bool, string>("GameStats", (isSuccess, userStatsJson) => GameStatsMessage(isSuccess, userStatsJson));

            _connection.StartAsync();
        }

        private void GameStatsMessage(bool isSuccess, string userStatsJson)
        {
            if (isSuccess)
            {
                //MessageBox.Show("Game stats is TRUE! userStatsJson: " + userStatsJson);
                LandingPage.GameStatsCheck(isSuccess, userStatsJson);
            }
            else
            {
                //MessageBox.Show("Game stats is FALSE! userStatsJson: " + userStatsJson);
                LandingPage.GameStatsCheck(isSuccess, userStatsJson);
            }
        }

        private void SavedGamesMessage(bool isSuccess, string userGamesJson)
        {
            if(isSuccess)
            {
                //MessageBox.Show("Saved games is TRUE! UserGamesJson: " + userGamesJson);
                LandingPage.SavedGamesCheck(isSuccess, userGamesJson);
            }
            else
            {
                //MessageBox.Show("Saved games is FALSE! UserGamesJson: " + userGamesJson);
                LandingPage.SavedGamesCheck(isSuccess, userGamesJson);
            }
        }

        private void CreateUserMessage(bool isSuccess, string message)
        {
            if (isSuccess)
            {
                //MessageBox.Show(message + " successfully created user");
                WPFUI.Login.CreateUserCheck(isSuccess);
            }
            else // not logged in
            {
                //MessageBox.Show(message + " did not create user");
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
                WPFUI.Login.LoggedInCheck(isLoggedIn, userJson);
            }
            else // not logged in
            {
                WPFUI.Login.LoggedInCheck(isLoggedIn, userJson);
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

        public void GetSavedGames(User user)
        {
            Start();
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

        internal void NewGameVsComputer(User user)
        {
            Start();
            try
            {
                string strUser = JsonConvert.SerializeObject(user);
                //_connection.InvokeAsync("GetSavedGames", strUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal void GetGameStats(User user)
        {
            Start();
            try
            {
                string strUser = JsonConvert.SerializeObject(user);
                _connection.InvokeAsync("GetGameStats", strUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}