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
                string roomName = cribbageGame.Id.ToString();
                
                // Add Game to DB.
                result = new GameManager(options).Insert(cribbageGame);

                await Groups.AddToGroupAsync(Context.ConnectionId, cribbageGame.Id.ToString());

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
                await Clients.Group(roomName).SendAsync("StartGame", cribbageGame.GameName + "\nSelect Crib Cards", cribbageGameJson);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task NewHand(string game, string userJson)
        {
            string cribbageGameJson;

            try
            {
                CribbageGame cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(game);
                User user = JsonConvert.DeserializeObject<User>(userJson);

                cribbageGame.Team1_Score = cribbageGame.Player_1.Score;
                cribbageGame.Team2_Score = cribbageGame.Player_2.Score;
                string roomName = cribbageGame.Id.ToString();

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
                    await Clients.Group(roomName).SendAsync("StartNewHand", cribbageGame.GameName + "\nSelect Crib Cards", cribbageGameJson);
                }
                else
                {
                    //need to figure out who called this and change their ready state.
                    if (user.Id == cribbageGame.Player_1.Id)
                    {
                        cribbageGame.Player_1.Ready = true;
                    }
                    else
                    {
                        cribbageGame.Player_2.Ready = true;
                    }

                    //need to wait for 2nd player / only update screen after they are all set 
                    if (cribbageGame.Player_1.Ready && cribbageGame.Player_2.Ready)
                    {
                        cribbageGame.Player_2.Ready = false;
                        cribbageGame.Player_1.Ready = false;

                        // Initialize Game, shuffle and deal,
                        CribbageGameManager.NextDealer(cribbageGame);
                        CribbageGameManager.ShuffleDeck(cribbageGame);
                        CribbageGameManager.Deal(cribbageGame);
                        cribbageGame.WhatToDo = "SelectCribCards";

                        // Serialize CribbageGame into Json
                        cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);

                        // Send CribbageGame back to only that person.
                        await Clients.Group(roomName).SendAsync("StartNewHand", cribbageGame.GameName + "\nSelect Crib Cards", cribbageGameJson);
                    }
                    else
                    {

                        cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                        string message = user.DisplayName + " is waiting to start the next hand.";
                        await Clients.Group(roomName).SendAsync("WaitingForPlayer", cribbageGameJson, message);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task CardsToCrib(string game, string cards, string userJson)
        {
            string cribbageGameJson;
            try
            {
                CribbageGame cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(game);

                List<Card> cardsToCrib = JsonConvert.DeserializeObject<List<Card>>(cards);
                User user = JsonConvert.DeserializeObject<User>(userJson);
               // User user = new User();
                string message;

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
                            message = cribbageGame.PlayerTurn.DisplayName + " played the " + card.name + "\n";
                            CribbageGameManager.PlayCard(cribbageGame, card);
                            cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                            await Clients.Group(cribbageGame.Id.ToString()).SendAsync("Action", cribbageGameJson, message + cribbageGame.PlayerTurn.DisplayName + "'s turn");
                        }
                    }
                    else
                    {
                        // The users Turn and they need to cut a card
                        cribbageGame.WhatToDo = "cutdeck";
                        cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                        await Clients.Group(cribbageGame.Id.ToString()).SendAsync("CutCard", cribbageGameJson, cribbageGame.PlayerTurn.DisplayName + " cut the deck.");
                    }
                }
                // Playing vs another person
                // only send to crib if they sent 2 cards to hub
                // check if the other person already sent cards.
                // if they did, send message to both players to cut the card.
                
                else if (!cribbageGame.Computer && cardsToCrib.Count == 2)
                {
                    string roomName = cribbageGame.Id.ToString();
                    if (user.Id == cribbageGame.Player_1.Id)
                    {
                        CribbageGameManager.Give_To_Crib(cribbageGame, cardsToCrib, cribbageGame.Player_1);
                        message = cribbageGame.Player_1.DisplayName + " sent cards to crib.";
                    }
                    else
                    {
                        CribbageGameManager.Give_To_Crib(cribbageGame, cardsToCrib, cribbageGame.Player_2);
                        message = cribbageGame.Player_2.DisplayName + " sent cards to crib.";
                    }

                    cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                    await Clients.Group(cribbageGame.Id.ToString()).SendAsync("CardsSentToCrib", cribbageGameJson, message);

                    if (cribbageGame.Crib.Count == 4)
                    {
                        cribbageGame.WhatToDo = "cutdeck";
                        cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                        await Clients.Group(roomName).SendAsync("CutCard", cribbageGameJson, cribbageGame.PlayerTurn.DisplayName + " cut the deck.");
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
                string roomName = cribbageGame.Id.ToString();
                CribbageGameManager.Cut(cribbageGame);
                cribbageGame.WhatToDo = "playcard";

                cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                await Clients.Group(roomName).SendAsync("CardWasCut", cribbageGameJson, cribbageGame.PlayerTurn.DisplayName + " cut the " + cribbageGame.CutCard.name + "\n" + cribbageGame.PlayerTurn.DisplayName + "'s turn");
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
                string roomName = cribbageGame.Id.ToString();

                if (CribbageGameManager.PlayCard(cribbageGame, pickedCard))
                {
                    cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                    message1 += cribbageGame.PlayerTurn.DisplayName + "'s turn";
                    await Clients.Group(roomName).SendAsync("Action", cribbageGameJson, message1);
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
                            Clients.Group(roomName).SendAsync("Action", cribbageGameJson, message + cribbageGame.PlayerTurn.DisplayName + "'s turn");
                            CheckCompletedGame(cribbageGame);
                        }
                        else if (cribbageGame.WhatToDo == "go")
                        {
                            string message = cribbageGame.PlayerTurn.DisplayName + " said go.\n";
                            CribbageGameManager.Go(cribbageGame);

                            cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                            await Clients.Group(roomName).SendAsync("Action", cribbageGameJson, message + cribbageGame.PlayerTurn.DisplayName + "'s turn");
                            CheckCompletedGame(cribbageGame);
                        }
                    }

                    if (!cribbageGame.Complete && cribbageGame.WhatToDo == "counthands")
                    {
                        string message = "All cards played. Count Hands";
                        cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                        await Clients.Group(roomName).SendAsync("RallyOver", cribbageGameJson, message);
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
                string roomName = cribbageGame.Id.ToString();

                message = cribbageGame.PlayerTurn.DisplayName + " said go.\n";
                CribbageGameManager.Go(cribbageGame);

                cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                await Clients.Group(cribbageGame.Id.ToString()).SendAsync("Action", cribbageGameJson, message + cribbageGame.PlayerTurn.DisplayName + "'s turn");
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
                        await Clients.Group(cribbageGame.Id.ToString()).SendAsync("Action", cribbageGameJson, message + cribbageGame.PlayerTurn.DisplayName + "'s turn");
                        CheckCompletedGame(cribbageGame);
                    }
                    else if (cribbageGame.WhatToDo == "go")
                    {
                        message = cribbageGame.PlayerTurn.DisplayName + " said go.\n";
                        CribbageGameManager.Go(cribbageGame);
                        cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                        await Clients.Group(cribbageGame.Id.ToString()).SendAsync("Action", cribbageGameJson, message + cribbageGame.PlayerTurn.DisplayName + "'s turn");
                        CheckCompletedGame(cribbageGame);
                    }
                }
                if (!cribbageGame.Complete && cribbageGame.WhatToDo == "counthands")
                {
                    message = "All cards played. Count Hands";
                    cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                    await Clients.Group(roomName).SendAsync("RallyOver", cribbageGameJson, message);
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
                string roomName = cribbageGame.Id.ToString();

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
                await Clients.Group(roomName).SendAsync("HandsCounted", cribbageGameJson, message);
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
                string roomName = cribbageGame.Id.ToString();
                string message;

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
                await Clients.Group(roomName).SendAsync("GameFinished", cribbageGameJson, message);
            }
        }

        public async Task NewGameVsPlayer(string userJson)
        {
            // Send back List of all available games to join to All connected users.
            string cribbageGameJson = "";
            int result;
            Game game;
            string message = "";
            string roomName;

            try
            {
                User user = JsonConvert.DeserializeObject<User>(userJson);

                game = new GameManager(options).GetAvailableGame();
                if(game == null)
                {
                    // Create a Game.
                    CribbageGame cribbageGame = new CribbageGame(user);
                    cribbageGame.Computer = false;
                    cribbageGame.WhatToDo = "waitingforplayer2";

                    // Add Game to DB.
                    result = new GameManager(options).Insert(cribbageGame);

                    // Serialize CribbageGame into Json
                    cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);

                    // Make a hub group
                    roomName = cribbageGame.Id.ToString();
                    await Groups.AddToGroupAsync(Context.ConnectionId, cribbageGame.Id.ToString());
                    message = user.DisplayName + " joined the game.\nWaiting for another player.";
                    await Clients.Group(roomName).SendAsync("WaitingForPlayer", cribbageGameJson, message);
                }
                else
                {
                    Guid player1Id = Guid.Parse(game.GameName);
                    User player_1 = new UserManager(options).LoadById(player1Id);
                    CribbageGame cribbageGame = new CribbageGame(game.Id,player_1, user);

                    int results = new GameManager(options).Update(cribbageGame);
                    UserGame userGame1 = new UserGame(cribbageGame.Id, player_1.Id, 0);
                    UserGame userGame2 = new UserGame(cribbageGame.Id, user.Id, 0);
                    results = new UserGameManager(options).Insert(userGame1);
                    results = new UserGameManager(options).Insert(userGame2);
                    cribbageGame.WhatToDo = "readytostart";

                    roomName = cribbageGame.Id.ToString();
                    await Groups.AddToGroupAsync(Context.ConnectionId, cribbageGame.Id.ToString());
                    cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                    message = user.DisplayName + " has joined the game.\n" + cribbageGame.GameName +"\nClick 'Ready' to begin the game.";
                    await Clients.Group(roomName).SendAsync("ReadyToStart", cribbageGameJson, message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task ReadyToPlay(string game, string userJson)
        {
            CribbageGame cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(game);
            User user = JsonConvert.DeserializeObject<User>(userJson);
            string message = "";
            string cribbageGameJson;
            string roomName = cribbageGame.Id.ToString();

            try
            {
                if (user.Id == cribbageGame.Player_1.Id)
                {
                    cribbageGame.Player_1.Ready = true;
                }
                else
                {
                    cribbageGame.Player_2.Ready = true;
                }
                if (cribbageGame.Player_1.Ready && cribbageGame.Player_2.Ready)
                {
                    cribbageGame.Player_1.GamesStarted += 1;
                    cribbageGame.Player_2.GamesStarted += 1;
                    new UserManager(options).Update(cribbageGame.Player_1);
                    new UserManager(options).Update(cribbageGame.Player_2);

                    // Initialize Game, shuffle, and deal
                    CribbageGameManager.ShuffleDeck(cribbageGame);
                    CribbageGameManager.Deal(cribbageGame);
                    cribbageGame.WhatToDo = "SelectCribCards";
                    cribbageGame.Player_1.Ready = false;
                    cribbageGame.Player_2.Ready = false;

                    // Serialize CribbageGame into Json
                    cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);

                    // Send CribbageGame back to only that person.
                    await Clients.Group(roomName).SendAsync("StartGame", cribbageGame.GameName + "\nSelect Crib Cards", cribbageGameJson);
                }
                else
                {
                    // UI side should check if you are the one that was ready, and hide the button. 
                    message = user.DisplayName + " is ready. Waiting for all players to be ready.";
                    cribbageGameJson = JsonConvert.SerializeObject(cribbageGame);
                    await Clients.Group(roomName).SendAsync("WaitingForConfirmation", cribbageGameJson, message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Note: Games and Usergames are saved after someone hits countCards button. Do not need to update games when closing
        public async Task QuitGame(string game, string user)
        {
            CribbageGame cribbageGame = JsonConvert.DeserializeObject<CribbageGame>(game);
            User loggedInUser = JsonConvert.DeserializeObject<User>(user);
            string message = "";

            string roomName = cribbageGame.Id.ToString();

            try
            {
                if (!cribbageGame.Computer)
                {
                    //check if the game should be deleted from the DB
                    // if player 1 leaves BEFORE player 2 joined
                    if (cribbageGame.WhatToDo == "waitingforplayer2")
                    {
                        //remove from hub group
                        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);

                        //delete from database
                        new GameManager(options).Delete(cribbageGame.Id);

                        //send message to close window
                        message = "Quit Game";
                        await Clients.Caller.SendAsync("QuitGame", message);
                    }
                    // if a player leaves the game
                    else
                    {
                        //remove from hub group
                        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);

                        //send message to hub group saying player 2 left
                        await Clients.Group(roomName).SendAsync("PlayerLeft", $"{loggedInUser.DisplayName} has left the game.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //**Additional comments**
        //SignalR groups: https://learn.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/working-with-groups
        //https://learn.microsoft.com/en-us/aspnet/core/signalr/groups?view=aspnetcore-8.0
    }
}
