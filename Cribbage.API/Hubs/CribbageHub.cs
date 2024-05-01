using Cribbage.BL;
using Cribbage.BL.Models;
using Cribbage.PL.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Policy;
using System.Text.RegularExpressions;

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
            { // need to add more for hashed passwords
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
                userGame = new UserGame(cribbageGame.Id, cribbageGame.Player_2.Id, cribbageGame.Player_2.Score);
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

        public async Task NewHand(string game)
        {
            string cribbageGameJson;

            try
            {
                CribbageGame cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(game);

                cribbageGame.Team1_Score = cribbageGame.Player_1.Score;
                cribbageGame.Team2_Score = cribbageGame.Player_2.Score;
                new UserGameManager(options).Update(cribbageGame);

                if (cribbageGame.Computer)
                {
                    // Initialize Game, shuffle and deal,
                    CribbageGameManager.NextDealer(cribbageGame);
                    CribbageGameManager.ShuffleDeck(cribbageGame);
                    CribbageGameManager.Deal(cribbageGame);
                    cribbageGame.WhatToDo = "SelectCribCards";

                    // Need to update UserGames with correct scores.
                    
                    // Serialize CribbageGame into Json
                    cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);

                    // Send CribbageGame back to only that person.
                    await Clients.Caller.SendAsync("StartNewHand", cribbageGame.GameName + "\nSelect Crib Cards", cribbageGameJson);
                }
                else
                {
                    //need to wait for 2nd player / only update screen after they are all set 

                    // Initialize Game, shuffle and deal,
                    CribbageGameManager.NextDealer(cribbageGame);
                    CribbageGameManager.ShuffleDeck(cribbageGame);
                    CribbageGameManager.Deal(cribbageGame);
                    cribbageGame.WhatToDo = "SelectCribCards";

                    // Serialize CribbageGame into Json
                    cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);

                    // Send CribbageGame back to only that person.
                    await Clients.Caller.SendAsync("StartNewHand", cribbageGame.GameName + "\nSelect Crib Cards", cribbageGameJson);
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task JoinGame(string game, string user)
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
                        cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                        await Clients.All.SendAsync("CardWasCut", cribbageGameJson, cribbageGame.PlayerTurn.DisplayName + " cut the " + cribbageGame.CutCard.name + "\n" + cribbageGame.PlayerTurn.DisplayName + "'s turn");
                        await Task.Delay(1000);
                        // Game could technically end on a cut. Need to check for a winner.
                        CheckCompletedGame(cribbageGame);
                        if (!cribbageGame.Complete)
                        {
                            cribbageGame.WhatToDo = "playcard";
                            Card card = CribbageGameManager.Pick_Card_To_Play(cribbageGame);
                            string message = cribbageGame.PlayerTurn.DisplayName + " played the " + card.name + "\n";
                            CribbageGameManager.PlayCard(cribbageGame, card);
                            cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                            await Clients.All.SendAsync("Action", cribbageGameJson, message + cribbageGame.PlayerTurn.DisplayName + "'s turn");
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

        public async Task CutDeck(string game)
        {
            string cribbageGameJson;

            try
            {
                CribbageGame cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(game);
                CribbageGameManager.Cut(cribbageGame);
                cribbageGame.WhatToDo = "playcard";

                cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                await Clients.All.SendAsync("CardWasCut", cribbageGameJson, cribbageGame.PlayerTurn.DisplayName + " cut the " + cribbageGame.CutCard.name + "\n" + cribbageGame.PlayerTurn.DisplayName + "'s turn");
                CheckCompletedGame(cribbageGame);
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
                List<Card> pickedCards = JsonConvert.DeserializeObject<List<Card>>(card);
                Card pickedCard = pickedCards[0];
                string message1 = cribbageGame.PlayerTurn.DisplayName + " played the " + pickedCard.name + "\n";
                if (CribbageGameManager.PlayCard(cribbageGame, pickedCard))
                {
                    cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                    message1 += cribbageGame.PlayerTurn.DisplayName + "'s turn";
                    await Clients.All.SendAsync("Action", cribbageGameJson, message1);
                    CheckCompletedGame(cribbageGame);

                    while (!cribbageGame.Complete 
                            && cribbageGame.Computer
                            && cribbageGame.PlayerTurn.Id == cribbageGame.Player_2.Id
                            && (cribbageGame.WhatToDo == "playcard" || cribbageGame.WhatToDo == "go"))
                    {
                        await Task.Delay(1000);
                        if (cribbageGame.WhatToDo == "playcard")
                        {
                            Card computerCard = CribbageGameManager.Pick_Card_To_Play(cribbageGame);
                            string message = cribbageGame.PlayerTurn.DisplayName + " played the " + computerCard.name + "\n";
                            CribbageGameManager.PlayCard(cribbageGame, computerCard);
                            
                            cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                            await Clients.All.SendAsync("Action", cribbageGameJson, message + cribbageGame.PlayerTurn.DisplayName + "'s turn");
                            CheckCompletedGame(cribbageGame);
                        }
                        else if (cribbageGame.WhatToDo == "go")
                        {
                            string message = cribbageGame.PlayerTurn.DisplayName + " said go.\n";
                            CribbageGameManager.Go(cribbageGame);

                            cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                            await Clients.All.SendAsync("Action", cribbageGameJson, message + cribbageGame.PlayerTurn.DisplayName + "'s turn");
                            CheckCompletedGame(cribbageGame);
                        }
                    }

                    if (!cribbageGame.Complete && cribbageGame.WhatToDo == "counthands")
                    {
                        string message = "All cards played. Count Hands";
                        cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                        await Clients.All.SendAsync("RallyOver", cribbageGameJson, message);
                    }
                }
                else
                {
                    cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                    await Clients.Caller.SendAsync("Action", cribbageGameJson, "Unable to play selected card. " + cribbageGame.PlayerTurn.DisplayName + "'s turn.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Go(string game)
        {
            string cribbageGameJson;

            try
            {
                CribbageGame cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(game);
                string message;

                message = cribbageGame.PlayerTurn.DisplayName + " said go.\n";
                CribbageGameManager.Go(cribbageGame);

                cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                await Clients.All.SendAsync("Action", cribbageGameJson, message + cribbageGame.PlayerTurn.DisplayName + "'s turn");
                CheckCompletedGame(cribbageGame);

                while (!cribbageGame.Complete
                            && cribbageGame.Computer
                            && cribbageGame.PlayerTurn.Id == cribbageGame.Player_2.Id
                            && (cribbageGame.WhatToDo == "playcard" || cribbageGame.WhatToDo == "go"))
                {
                    await Task.Delay(1000);
                    if (cribbageGame.WhatToDo == "playcard")
                    {
                        Card computerCard = CribbageGameManager.Pick_Card_To_Play(cribbageGame);
                        message = cribbageGame.PlayerTurn.DisplayName + " played the " + computerCard.name + "\n";
                        CribbageGameManager.PlayCard(cribbageGame, computerCard);
                        cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                        await Clients.All.SendAsync("Action", cribbageGameJson, message + cribbageGame.PlayerTurn.DisplayName + "'s turn");
                        CheckCompletedGame(cribbageGame);
                    }
                    else if (cribbageGame.WhatToDo == "go")
                    {
                        message = cribbageGame.PlayerTurn.DisplayName + " said go.\n";
                        CribbageGameManager.Go(cribbageGame);
                        cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                        await Clients.All.SendAsync("Action", cribbageGameJson, message + cribbageGame.PlayerTurn.DisplayName + "'s turn");
                        CheckCompletedGame(cribbageGame);
                    }
                }
                if (!cribbageGame.Complete && cribbageGame.WhatToDo == "counthands")
                {
                    message = "All cards played. Count Hands";
                    cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                    await Clients.All.SendAsync("RallyOver", cribbageGameJson, message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CountHands(string game)
        {
            string cribbageGameJson;

            try
            {
                CribbageGame cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(game);
                string message = "";

                CribbageGameManager.CountHands(cribbageGame);

                if(cribbageGame.Dealer == 1)
                {
                    message = cribbageGame.Player_2.DisplayName + "'s hand had " + cribbageGame.Player_2.HandPoints + " points\n";

                    if(!cribbageGame.Complete)
                    {
                        // Game not over, everything counted.
                        message += cribbageGame.Player_1.DisplayName + "'s hand had " + cribbageGame.Player_1.HandPoints + " points\n";
                        message += cribbageGame.Player_1.DisplayName + "'s crib had " + cribbageGame.Player_1.CribPoints + " points\n";
                    }
                    else if (cribbageGame.Complete && cribbageGame.Player_1.CribPoints != 0)
                    {
                        // Game is over after p1 counted crib.
                        message += cribbageGame.Player_1.DisplayName + "'s hand had " + cribbageGame.Player_1.HandPoints + " points\n";
                        message += cribbageGame.Player_1.DisplayName + "'s crib had " + cribbageGame.Player_1.CribPoints + " points\n";

                    }
                    else if (cribbageGame.Complete && cribbageGame.Player_1.HandPoints != 0 && cribbageGame.Player_1.CribPoints == 0)
                    {
                        // Game is ove after p1 counted their hand
                        message += cribbageGame.Player_1.DisplayName + "'s hand had " + cribbageGame.Player_1.HandPoints + " points\n";

                    }
                    // No messages added if p2 won the game after counting their hand.
                }
                else if (cribbageGame.Dealer != 1)
                {
                    message = cribbageGame.Player_1.DisplayName + "'s hand had " + cribbageGame.Player_1.HandPoints + " points\n";

                    if (!cribbageGame.Complete)
                    {
                        // Game not over, everything counted.
                        message += cribbageGame.Player_2.DisplayName + "'s hand had " + cribbageGame.Player_2.HandPoints + " points\n";
                        message += cribbageGame.Player_2.DisplayName + "'s crib had " + cribbageGame.Player_2.CribPoints + " points\n";
                    }
                    else if (cribbageGame.Complete && cribbageGame.Player_2.CribPoints != 0)
                    {
                        // Game is over after p2 counted crib.
                        message += cribbageGame.Player_2.DisplayName + "'s hand had " + cribbageGame.Player_2.HandPoints + " points\n";
                        message += cribbageGame.Player_2.DisplayName + "'s crib had " + cribbageGame.Player_2.CribPoints + " points\n";

                    }
                    else if (cribbageGame.Complete && cribbageGame.Player_2.HandPoints != 0 && cribbageGame.Player_2.CribPoints == 0)
                    {
                        // Game is ove after p2 counted their hand
                        message += cribbageGame.Player_2.DisplayName + "'s hand had " + cribbageGame.Player_2.HandPoints + " points\n";

                    }
                    // No messages added if p1 won the game after counting their hand.
                }

                cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                await Clients.All.SendAsync("HandsCounted", cribbageGameJson, message);
                CheckCompletedGame(cribbageGame);

            }
            catch (Exception)
            {

                throw;
            }

        }

        private async void CheckCompletedGame(CribbageGame cribbageGame)
        {
            if (cribbageGame.Complete)
            {
                string message;
                //cribbageGame.WhatToDo = "startnewgame";


            if (cribbageGame.Player_1.Score > cribbageGame.Player_2.Score)
            {
                cribbageGame.Winner = cribbageGame.Player_1.Id;
                message = "Game Over.\nWinner: " + cribbageGame.Player_1.DisplayName;
            }
            else
            {
                cribbageGame.Winner = cribbageGame.Player_2.Id;
                message = "Game Over.\nWinner: " + cribbageGame.Player_2.DisplayName;
            }

                cribbageGame.Team1_Score = cribbageGame.Player_1.Score;
                cribbageGame.Team2_Score = cribbageGame.Player_2.Score;
                new UserGameManager(options).Update(cribbageGame);
                new GameManager(options).Update(cribbageGame);

                string cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                await Clients.All.SendAsync("GameFinished", cribbageGameJson, message);
            }
        }

 // Pseudo Code for Player vs Player

        ////Purpose is to create a cribbage game in the state of waiting for someone else to join.

        //NewGameVsPlayer(string userJson)
        //	- create a new game with one player
        //	- create a group with await Groups.AddToGroupAsync(Context.ConectionId, "GameIDGUid")
        //	- send game back to caller.async("WaitingForPlayer", cribbagegamejson)


        ////Purpose is to get a list of all available games to join where a player is waiting for someone.

        //GetPlayerGames()
        //	-Load games from DB where only P1 is set and P2 is null. probably need to create a method on managercode
        //	-return msg to caller.async("GamesToJoin", json of List<Game> or List<CribbageGame> ??)
        //  Recommended change: -return msg to caller.async("JoinGame", cribbageGame) -- add them as Player2
        // - return only the first game and add them to that game (not joining friends, only joining random people currently)
        //      -Caller joins group with Groups.AddToGroupAsync(Context.ConectionId, "GameIDGUid")
        //		-Update game in DB with you as P2
        //		-Create new UserGames for both players in DB.
        //		-Update a CribbageGame with you as P2
        //		-Update CribbageGame Name P1.UserName Vs.P2.UserName
        //		-Update WhatToDO = "readytostart" or something similar we agree on. Purpose being both players should have to click a button on UI indicating they are ready because the host player might not be ready if waiting for awhile.


        //****Problem**** How do we delete games when someone quits the program or disconects before someone joins their game?
        //put them in another "waiting" state as player 1 or send them back to their "Home" page to try again
        //****Problem**** How do we get games that are resumed i.e they DB already has both playerId's stored
        //and then one of them wants to resume it later.



        ////Purpose is to join a game that someone is there and waiting for you to join.
        /// this would be if we had them join with a code to play with friends --> skip this for now, get random player vs random player working first

        //JoinGameVsPlayer(string game)
        //	-check DB if some one joined it before you or if the host left and the game was deleted already
        //	-if not there send back to caller.async("GameNotAvailable", "That game is no longer available.")
        //	-if available to join:
        //		-Caller joins group with Groups.AddToGroupAsync(Context.ConectionId, "GameIDGUid")
        //		-Update game in DB with you as P2
        //		-Create new UserGames for both players in DB.
        //		-Update a CribbageGame with you as P2
        //		-Update CribbageGame Name P1.UserName Vs.P2.UserName
        //		-Update WhatToDO = "readytostart" or something similar we agree on. Purpose being both players should have to click a button on UI indicating they are ready because the host player might not be ready if waiting for awhile.

        //	- make a string message something like "P2.displayName Has joined the game\n 

        //                        P1.displayName Vs. P2.displyname\n
        //                        Click Ready button to start"
        //	-return msg to Group(groupName which is the gameID).SendAsync("ReadyToStart", cribbageGameJson, message)


        //**Additional comments**

        //May have to add a property to cribbagegame class or Player class. Maybe a bool to indicate if they are ready for things like next hand or play another game. Next hand shouldn't be dealt unless both players are ready.
            //We can add a "Ready" button and wait until both click "Ready" to deal, etc.
        //UI's would have to add code to handle all new messages sent back to them and display widgets/controls properly
    }
}
