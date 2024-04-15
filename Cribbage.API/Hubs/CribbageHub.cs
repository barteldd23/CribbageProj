﻿using Cribbage.BL;
using Cribbage.BL.Models;
using Cribbage.PL.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Cribbage.API.Hubs
{
    public class CribbageHub : Hub
    {
        protected readonly DbContextOptions<CribbageEntities> options;
        private IConfigurationRoot _configuration;

        public CribbageHub()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();

            options = new DbContextOptionsBuilder<CribbageEntities>()
                .UseSqlServer(_configuration.GetConnectionString("DatabaseConnection"))
                .UseLazyLoadingProxies()
                .Options;
        }

        public async Task Login(string email, string password)
        {

            string message;
            bool isLoggedIn;
            string userJson = "";
            try
            {
                // Try logging in.
                User user = new User { Email = email, Password = password };
                isLoggedIn = new UserManager(options).Login(user);
                // Send Back Success/fail to client only
                
                if (isLoggedIn) { message = "Logged in as: " + user.DisplayName; }
                else { message = "Error. Try Again"; }

                await Clients.All.SendAsync("ReceiveMessage", message, message);

                //await Clients.Caller.SendAsync("LogInAttempt", isLoggedIn, message);
                // On success: serialize User into Json
                userJson = JsonConvert.SerializeObject(user);
                // Send Back User Json to client only
                await Clients.Caller.SendAsync("LogInAttempt", isLoggedIn, message, userJson);
            }
            catch (Exception ex)
            {
                isLoggedIn = false;
                message = "Login Failed";
                await Clients.All.SendAsync("ReceiveMessage", message, message);
                await Clients.Caller.SendAsync("LogInAttempt", isLoggedIn, message, userJson);
            }
        }

        public async Task CreateUser(string user)
        {
            string message = "";
            bool isSuccess = false;
            string userJson;
            try
            {
                // De-serialize string to user object
                User newUser;
                
                newUser = JsonConvert.DeserializeObject<User>(user);

                // Try to create in DB
                int rows = new UserManager(options).Insert(newUser);
                if (rows == 1 ) isSuccess = true;
                
                //userJson = JsonConvert.SerializeObject(newUser);

                // Send Back Success/Fail to client only
                //Do we want them to log in still after creating an account?
                await Clients.Caller.SendAsync("CreateUserAttempt", isSuccess, message);

                // send List of available games to join to client only
            }
            catch (Exception)
            {
                message = "User already exists for that email";
                await Clients.Caller.SendAsync("CreateUserAttempt", isSuccess, message);
            }
            
        }

        public async Task GetSavedGames(string user, string message)
        {
            bool isSuccess = false;
            string userGamesJson;
            try
            {
                // De-serialize string to user object
                User newUser;
                
                newUser = JsonConvert.DeserializeObject<User>(user);

                // Get the saved games for the user
                List<Guid> savedGameIds = new UserGameManager(options).GetGames(newUser.Id);

                if (savedGameIds != null ) isSuccess = true; // what happens if they don't have any games?

                userGamesJson = JsonConvert.SerializeObject(savedGameIds);

                // Send Back Success/Fail to client only
                //Do we want them to log in still after creating an account?
                await Clients.Caller.SendAsync("SavedGames", isSuccess, userGamesJson);

                // send List of available games to join to client only
            }
            catch (Exception)
            {
                isSuccess = false;
                userGamesJson = null;
                await Clients.Caller.SendAsync("SavedGames", isSuccess, userGamesJson);
            }
        }

        public async Task GetPlayerStats(string user, string message)
        {
            // Do BL Stuff - Game Logic
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessage(string user, string message)
        {
            // Do BL Stuff - Game Logic
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendGameMessage(string message)
        {
            // send message only to people in that game group.
        }

        public async Task NewGameVsComputer(string user)
        {
            // Create a Game.
            // Add Game to DB.
            // Add UserGame to DB.
            // Create CribbageGame
            // Serialzie CribbageGame into Json
            // Send CribbageGame back to only that person.

        }
        public async Task NewGameVsPlayer(string user)
        {
            // Create a Game, only 1 person assigned to it.
            // Wait for 2nd person.
            // Send back List of all available games to join to All connected users.
        }

        public async Task JoinGame(string user, string game)
        {
            // Assign 2nd person to that Game. and hub group
            // Add Game to DB.
            // serialize Game into Json
            // Send Json back to both players using the hub group
        }
    }
}
