using Cribbage.BL;
using Cribbage.BL.Models;
using Cribbage.PL.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Cribbage.API.Hubs
{
    public class CribbageHub : Hub
    {
        private readonly DbContextOptions<CribbageEntities> options;

        public async Task Login(string email, string password)
        {
            // Try logging in.
            User user = new User { Email = email, Password = password };
            bool isLoggedIn = new UserManager(options).Login(user);
            // Send Back Success/fail to client only
            string message;
            if (isLoggedIn) { message = "Logged in as: " + user.DisplayName; }
            else { message = "Error. Try Again"; }
            await Clients.Caller.SendAsync("LoggedIn", message);
            // On success: serialize User into Json
            string UserJson = JsonConvert.SerializeObject(user);
            // Send Back User Json to client only
            //await Clients.Caller.SendAsync("User", UserJson);
        }

        public async Task CreateUser(string user)
        {
            // De-serialize string to user object
            // Try to create in DB
            // Send Back Success/Fail to client only
            // send List of available games to join to client only
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
