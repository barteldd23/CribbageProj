using Cribbage.BL;
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
            bool isSuccess = false;
            string userGamesJson = "";

            string userJson = "";
            try
            {
                // Try logging in.
                User user = new User { Email = email, Password = password };
                isLoggedIn = new UserManager(options).Login(user);
                // Send Back Success/fail to client only

                if (isLoggedIn) 
                { 
                    message = "Logged in as: " + user.DisplayName;

                    List<Guid> savedGameIds = new UserGameManager(options).GetGames(user.Id);
                    Game game;
                    List<Game> savedGames = new List<Game>();

                    foreach (Guid savedGameId in savedGameIds)
                    {
                        game = new GameManager(options).LoadById(savedGameId);
                        savedGames.Add(game);
                    }

                    if (savedGames != null) isSuccess = true; // what happens if they don't have any games?
                    else isSuccess = false;

                    userGamesJson = JsonConvert.SerializeObject(savedGames);
                }
                else { message = "Error. Try Again"; }

                await Clients.All.SendAsync("ReceiveMessage", message, message);

                //await Clients.Caller.SendAsync("LogInAttempt", isLoggedIn, message);
                // On success: serialize User into Json
                userJson = JsonConvert.SerializeObject(user);
                // Send Back User Json to client only
                await Clients.Caller.SendAsync("LogInAttempt", isLoggedIn, isSuccess, message, userJson, userGamesJson);
            }
            catch (Exception ex)
            {
                isLoggedIn = false;
                isSuccess = false;
                userGamesJson = null;
                message = "Login Failed";
                await Clients.All.SendAsync("ReceiveMessage", message, message);
                await Clients.Caller.SendAsync("LogInAttempt", isLoggedIn, isSuccess, message, userJson, userGamesJson);
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
                if (rows == 1) isSuccess = true;

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
            string cribbageGameJson = "";
            string computerEmail = "computer@computer.com";
            User computer = new UserManager(options).LoadByEmail(computerEmail);
            int result;

            try
            {
                User player1 = JsonConvert.DeserializeObject<User>(user);

                // Create a Game.
                CribbageGame cribbageGame = new CribbageGame(player1, computer);
                cribbageGame.Computer = true;

                // Add Game to DB.
                result = new GameManager(options).Insert(cribbageGame);

                // Add UserGame to DB.
                UserGame userGame = new UserGame(cribbageGame.Id, player1.Id, cribbageGame.Player_1.Score);
                result = new UserGameManager(options).Insert(userGame);
                player1.GamesStarted++;
                result = new UserManager(options).Update(player1);
                cribbageGame.Player_1.GamesStarted = player1.GamesStarted;

                // Initialize Game, shuffle and deal,
                CribbageGameManager.ShuffleDeck(cribbageGame);
                CribbageGameManager.Deal(cribbageGame);
                cribbageGame.WhatToDo = "SelectCribCards";

                // Serialize CribbageGame into Json
                cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);

                // Send CribbageGame back to only that person.
                await Clients.Caller.SendAsync("StartGame", cribbageGame.GameName + "\nSelect Crib Cards", cribbageGameJson);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task NewGameVsPlayer(string user)
        {
            // Send back List of all available games to join to All connected users.

            string cribbageGameJson = "";
            int result;

            try
            {
                /*
                User player1 = JsonConvert.DeserializeObject<User>(user);

                // Create a Game, only 1 person assigned to it.

                CribbageGame cribbageGame = new CribbageGame(player1);

                // Wait for 2nd person

                // Add Game to DB.
                result = new GameManager(options).Insert(cribbageGame);

                // Add UserGame to DB.
                UserGame userGame = new UserGame(cribbageGame.Id, player1.Id, cribbageGame.Player_1.Score);
                result = new UserGameManager(options).Insert(userGame);
                player1.GamesStarted++;
                result = new UserManager(options).Update(player1);
                cribbageGame.Player_1.GamesStarted = player1.GamesStarted;

                // Serialize CribbageGame into Json
                cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);

                // Send CribbageGame back to only that person.
                await Clients.Caller.SendAsync("StartGameVsPlayer", cribbageGameJson);
                */
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task JoinGame(string user, string game)
        {
            // Assign 2nd person to that Game. and hub group
            // Add Game to DB.
            // serialize Game into Json
            // Send Json back to both players using the hub group
        }

        public async Task CardsToCrib(string game, string cards, string userJson)
        {
            string cribbageGameJson;
            try
            {
                CribbageGame cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(game);

                List<Card> cardsToCrib = JsonConvert.DeserializeObject<List<Card>>(cards);
                //User user = JsonConvert.DeserializeObject<User>(userJson);
                User user = new User();

                // Playing vs computer.
                // Get computer cards to crib, and send them to crib.
                // send message to client saying ready to play
                if (cribbageGame.Computer && cardsToCrib.Count == 2)
                {
                    List<Card> computerCribCards = CribbageGameManager.Pick_Cards_To_Crib(cribbageGame.Player_2.Hand);
                    CribbageGameManager.Give_To_Crib(cribbageGame, computerCribCards, cribbageGame.Player_2);
                    CribbageGameManager.Give_To_Crib(cribbageGame, cardsToCrib, cribbageGame.Player_1);

                    // If Computer Turn, have cut the deck
                    if (cribbageGame.PlayerTurn.Id == cribbageGame.Player_2.Id)
                    {
                        CribbageGameManager.Cut(cribbageGame);

                        // Game could technically end on a cut. Need to check for a winner.
                        if (CribbageGameManager.CheckWinner(cribbageGame))
                        {
                            cribbageGame.WhatToDo = "startnewgame";
                            cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                            // maybe add return paramter of string message saying who won.
                            await Clients.All.SendAsync("GameFinished", cribbageGameJson);
                        }
                        else
                        {
                            cribbageGame.WhatToDo = "playcard";
                            cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                            await Clients.All.SendAsync("PlayHand", cribbageGameJson, cribbageGame.PlayerTurn.DisplayName + " cut the " + cribbageGame.CutCard.name + "\n" + cribbageGame.PlayerTurn.DisplayName + "'s Turn.");
                        }
                    }
                    else
                    {
                        // The users Turn and they need to cut a card
                        cribbageGame.WhatToDo = "cutdeck";
                        cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                        await Clients.All.SendAsync("CutCard", cribbageGameJson, cribbageGame.PlayerTurn.DisplayName + " cut the deck.");
                    }
                }
                // Playing vs another pseron
                // only send to crib if they sent 2 cards to hub
                // check if the other person already sent cards.
                // if they did, send message to both players to cut the card.
                else if (!cribbageGame.Computer && cardsToCrib.Count == 2)
                {
                    if (user.Id == cribbageGame.Player_1.Id)
                    {
                        CribbageGameManager.Give_To_Crib(cribbageGame, cardsToCrib, cribbageGame.Player_1);
                    }
                    else
                    {
                        CribbageGameManager.Give_To_Crib(cribbageGame, cardsToCrib, cribbageGame.Player_2);
                    }

                    if (cribbageGame.Crib.Count == 4)
                    {
                        cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                        await Clients.All.SendAsync("CutCard", cribbageGameJson, cribbageGame.PlayerTurn.DisplayName + " cut the deck.");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task PickCardToPlay(string game)
        {
            string cribbageGameJson;
            string card;

            try
            {
                CribbageGame cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(game);
                Card pickedCard = CribbageGameManager.Pick_Card_To_Play(cribbageGame);

                cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                card = JsonConvert.SerializeObject(pickedCard);

                PlayCard(cribbageGameJson, card);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PlayCard(string game, string card)
        {
            string cribbageGameJson;    
            
            try
            {
                CribbageGame cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(game);
                Card pickedCard = JsonConvert.DeserializeObject<Card>(card);

                if(CribbageGameManager.PlayCard(cribbageGame, pickedCard))
                {
                    cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                    await Clients.All.SendAsync("PlayedCard", cribbageGameJson, "Player Turn: " + cribbageGame.PlayerTurn.DisplayName);
                }
                else
                {
                    cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                    await Clients.All.SendAsync("PlayedCard", cribbageGameJson, "Unable to play cards. Player Turn: " + cribbageGame.PlayerTurn.DisplayName);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
