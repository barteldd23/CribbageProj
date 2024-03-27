using Cribbage.BL.Models;
using System.Numerics;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Cribbage.BL
{
    public class GameManager : GenericManager<tblGame>
    {
        public GameManager(DbContextOptions<CribbageEntities> options) : base(options) { }
        public GameManager(ILogger logger, DbContextOptions<CribbageEntities> options) : base(logger, options) { }
        #region DB Methods
        public int Insert(Game game, bool rollback = false)
        {
            try
            {
                int results;

                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGame newRow = new tblGame();

                    newRow.Id = Guid.NewGuid();
                    newRow.Winner = game.Winner;
                    newRow.Date = game.Date;
                    newRow.GameName = game.GameName;
                    newRow.Complete = game.Complete;

                    game.Id = newRow.Id;

                    dc.tblGames.Add(newRow);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public int Update(Game game, bool rollback = false)
        {
            try
            {
                int results;

                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGame updateRow = dc.tblGames.FirstOrDefault(g => g.Id == game.Id);

                    if (updateRow != null)
                    {
                        updateRow.Winner = game.Winner;
                        updateRow.Date = game.Date;
                        updateRow.GameName = game.GameName;
                        updateRow.Complete = game.Complete;

                        dc.tblGames.Update(updateRow);

                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row not found.");
                    }
                }
                return results;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                int results;

                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGame deleteRow = dc.tblGames.FirstOrDefault(g => g.Id == id);

                    if (deleteRow != null)
                    {
                        // remove the row from tblUserGame
                        var userGame = dc.tblUserGames.Where(g => g.GameId == id);
                        dc.tblUserGames.RemoveRange(userGame);

                        // remove the game from tblGame
                        dc.tblGames.Remove(deleteRow);

                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row not found.");
                    }
                }
                return results;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Game> Load()
        {
            try
            {
                List<Game> games = new List<Game>();

                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    games = (from g in dc.tblGames
                             select new Game
                             {
                                 Id = g.Id,
                                 Winner = g.Winner,
                                 Date = g.Date,
                                 GameName = g.GameName,
                                 Complete = g.Complete
                             }
                             )
                             .OrderBy(g => g.GameName)
                             .ToList();
                }
                return games;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Game LoadById(Guid id)
        {
            try
            {
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    tblGame row = dc.tblGames.FirstOrDefault(g => g.Id == id);

                    if (row != null)
                    {
                        Game game = new Game
                        {
                            Id = row.Id,
                            Winner = row.Winner,
                            Date = row.Date,
                            GameName = row.GameName,
                            Complete = row.Complete
                        };
                        return game;
                    }
                    else
                    {
                        throw new Exception("Row not found");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion


        #region Play Game Methods
        public bool CheckWinner(Game game)
        {
            // Checks scores of both players. If there is a winner, changer Winner property to that player.
            // Change Complete to true if a winner.
            // Returns Complete bool value always.
            if (game.Player_1.Score >= 121)
            {
                game.Winner = game.Player_1.Id;
                game.Complete = true;
            }
            else if (game.Player_2.Score >= 121)
            {
                game.Winner = game.Player_2.Id;
                game.Complete = true;
            }
            return game.Complete;
        }

        // Think about adding a AddPoints(int points, Player plaer) method

        #region "Starting the game methods"
        public void ShuffleDeck(Game game)
        {
            // Go through each card twice. Randomly switch it with a different Index Location
            game.Deck = new Deck();

            Random rnd = new Random();
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < game.Deck.Cards.Count; i++)
                {
                    int randNum = rnd.Next(game.Deck.Cards.Count);
                    var value = game.Deck.Cards[randNum];
                    game.Deck.Cards[randNum] = game.Deck.Cards[i];
                    game.Deck.Cards[i] = value;
                }
            }
        }
        public void Deal(Game game)
        {
            // Add the top card of the deck to the Players hand. Then remove that card from the Deck. 
            EndCountingRally(game);
            game.Player_1.Hand.Clear();
            game.Player_2.Hand.Clear();
            game.Player_1.PlayedCards.Clear();
            game.Player_2.PlayedCards.Clear();
            game.Crib.Clear();
            game.CutCard = null;


            if (game.Dealer == 1)
            {
                // Player 1 deals, Player 2 gets the first card.
                game.Player_2.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);
                game.Player_1.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);

                game.Player_2.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);
                game.Player_1.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);

                game.Player_2.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);
                game.Player_1.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);

                game.Player_2.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);
                game.Player_1.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);

                game.Player_2.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);
                game.Player_1.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);

                game.Player_2.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);
                game.Player_1.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);

                game.PlayerTurn = game.Player_2;
            }
            else
            {
                //Player 2 Deals. Player 1 gets the first card.
                game.Player_1.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);
                game.Player_2.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);

                game.Player_1.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);
                game.Player_2.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);

                game.Player_1.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);
                game.Player_2.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);

                game.Player_1.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);
                game.Player_2.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);

                game.Player_1.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);
                game.Player_2.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);

                game.Player_1.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);
                game.Player_2.Hand.Add(game.Deck.Cards[0]);
                game.Deck.Cards.RemoveAt(0);

                game.PlayerTurn = game.Player_1;
            }
        }

        public void Cut(Game game)
        {
            // choose a random index position to be the cut card.
            Random rnd = new Random();
            int index = rnd.Next(10, 31);
            game.CutCard = game.Deck.Cards[index];
            if (game.CutCard.face == Faces.Jack)
            {
                if (game.Dealer == 1) game.Player_1.Score += 2;
                else game.Player_2.Score += 2;

                CheckWinner(game);
            }
        }

        public void Give_To_Crib(Game game, List<Card> cards, Player player)
        {
            // Adds a List of cards to the crib and removes it from that players hand
            foreach (Card card in cards)
            {
                game.Crib.Add(card);
                if (player == game.Player_1) game.Player_1.Hand.Remove(card);
                else game.Player_2.Hand.Remove(card);
            }
        }

        public List<Card> Pick_Cards_To_Crib(List<Card> hand)
        {
            // Checks the all possible combinations of 4 cards of your 6 card hand
            // Keeps track of the 4 card hand with the highest score
            // returns a List<Card> of the 2 cards not used in the 4 card hand
            // If two or more combinations have the same highest score,
            // there is a 50% chance to choose the next hand in iteration over the previous hand

            List<Card> cards_To_Crib = new List<Card>();
            List<Card> cards_To_Keep = new List<Card>();
            int handPoints = 0;
            List<Card> check_4_Cards = new List<Card>();
            for (int i = 0; i < hand.Count - 3; i++)
            {
                for (int j = i + 1; j < hand.Count - 2; j++)
                {
                    for (int k = j + 1; k < hand.Count - 1; k++)
                    {
                        for (int l = k + 1; l < hand.Count; l++)
                        {
                            check_4_Cards.Add(hand[i]);
                            check_4_Cards.Add(hand[j]);
                            check_4_Cards.Add(hand[k]);
                            check_4_Cards.Add(hand[l]);
                            int check_Cards_Points = Get_Points_of_4_Cards(check_4_Cards);

                            if (handPoints <= check_Cards_Points)
                            {
                                if (handPoints == check_Cards_Points)
                                {
                                    Random rnd = new Random();
                                    if (rnd.NextSingle() > .5)
                                    {
                                        cards_To_Keep = check_4_Cards;
                                    }
                                }
                                else
                                {
                                    cards_To_Keep = check_4_Cards;
                                    handPoints = check_Cards_Points;
                                }

                            }
                            check_4_Cards = null;
                            check_4_Cards = new List<Card>();
                        }
                    }
                }
            }

            foreach (Card card in hand)
            {
                if (!cards_To_Keep.Contains(card))
                {
                    cards_To_Crib.Add(card);
                }
            }


            return cards_To_Crib;
        }

        public int Get_Points_of_4_Cards(List<Card> check_4_Cards)
        {
            int points = 0;

            points += CountPairs(check_4_Cards);
            points += Count15s(check_4_Cards);
            if (check_4_Cards[0].suit == check_4_Cards[1].suit &&
                check_4_Cards[0].suit == check_4_Cards[2].suit &&
                check_4_Cards[0].suit == check_4_Cards[3].suit) { points += 4; }

            check_4_Cards = check_4_Cards.OrderBy(c => c.face).ToList();

            if (check_4_Cards[0].face == check_4_Cards[1].face - 1 &&
                check_4_Cards[1].face == check_4_Cards[2].face - 1 &&
                check_4_Cards[2].face == check_4_Cards[3].face - 1)
            {
                points += 4;
            }
            else
            {
                // check for runs with 3 cards
                for (int i = 0; i < check_4_Cards.Count - 2; i++)
                {
                    for (int j = i + 1; j < check_4_Cards.Count - 1; j++)
                    {
                        for (int k = j + 1; k < check_4_Cards.Count; k++)
                        {
                            if (check_4_Cards[i].face == check_4_Cards[j].face - 1 &&
                                check_4_Cards[j].face == check_4_Cards[k].face - 1)
                            {
                                points += 3;
                            }
                        }
                    }
                }
            }

            return points;
        }
        #endregion

        #region "Methods for counting rally"
        public bool PlayCard(Game game, Card card)
        {
            // Return true if the given card could be played. Return false if it can't be played.
            // It can be played if the card value added to the current count is <= 31.
            // Check if played card gets points with being 15 total, a pair, or a run.
            // add 2 points if total is 31.

            if (card.value + game.CurrentCount <= 31)
            {
                game.PlayedCards.Add(card);
                game.CurrentRally.Add(card);
                Check15(game);
                CheckPair(game);
                CheckRun(game);
                if (game.CurrentCount == 31)
                {
                    game.PlayerTurn.Score += 2;
                    EndCountingRally(game);

                }
                CheckWinner(game);
                game.LastPlayerPlayed = game.PlayerTurn;
                game.PlayerTurn.Hand.Remove(card);
                game.PlayerTurn.PlayedCards.Add(card);
                
                // Probably needs more checking to see if the player has said GO.
                game.PlayerTurn = game.PlayerTurn == game.Player_1 ? game.Player_2 : game.Player_1;
                return true;
            }
            else
            {
                return false;
            }

        }

        private void EndCountingRally(Game game)
        {
            game.CurrentRally = null;
            game.CurrentRally = new List<Card>();
            game.Player_1.SaidGo = false;
            game.Player_2.SaidGo = false;

        }

        private bool CheckRun(Game game)
        {
            // Can only check for a run if 3 or more cards have been played. End method if not.
            // Check by checking if all the cards make a run. Sort and check if faces are sequential. 
            // If all the cards don't make a run... then check for all but first played card and so on.

            //if (PlayedCards.Count < 3) return;

            // Need to test this. Also check that PlayedCards list is not changed.
            // make sure it is sorting by enum number instead of enum word.
            var cardsSorted = game.PlayedCards.OrderBy(c => c.face).ToList();
            int attempt = 1;
            while (cardsSorted.Count >= 3)
            {


                // checks for the maximum length run possible first then continues looping until the minimum length run possible.


                bool run = true;

                for (int i = 0; i < cardsSorted.Count - 1; i++)
                {
                    if (cardsSorted[i].face + 1 != cardsSorted[i + 1].face)
                    {
                        run = false; break;
                    }
                }
                if (run)
                {
                    game.PlayerTurn.Score += cardsSorted.Count;

                    return true;
                }

                // Makes a new sorted list but after you remove the first card(s) played.
                cardsSorted = null;
                cardsSorted = game.PlayedCards.GetRange(attempt, game.PlayedCards.Count - attempt);
                cardsSorted = cardsSorted.OrderBy(c => c.face).ToList();
                attempt++;
            }

            return false;

        }

        private bool CheckPair(Game game)
        {
            // Check if Last 2 cards are a match. keep checking. When not a match, break out and add the points to players score.
            int pts_To_Add = 0;
            for (int i = 1; i < game.PlayedCards.Count; i++)
            {
                if (game.PlayedCards[game.PlayedCards.Count - i].face == game.PlayedCards[game.PlayedCards.Count - i - 1].face)
                {
                    // First match, add 2 points.
                    // Second match (3 of a kind or 3 pairs) add 4 more points
                    // Third match (4 of a kind or 6 pairs) add 6 more points
                    pts_To_Add += 2 * i;
                }
                else
                {
                    break;
                }
            }
            if (pts_To_Add > 0)
            {
                game.PlayerTurn.Score += pts_To_Add;
                return true;
            }
            else
            {
                return false;
            }

        }

        private bool Check15(Game game)
        {

            // If current total is 15, add 2 points to current player score.

            if (game.CurrentCount == 15)
            {
                game.PlayerTurn.Score += 2;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Go(Game game)
        {

            if (game.GoCount == 0)
            {
                game.PlayerTurn.SaidGo = true;
                game.GoCount++;
            }
            else
            {
                game.LastPlayerPlayed.Score += 1;
                EndCountingRally(game);
                game.GoCount = 0;
            }

            game.PlayerTurn = game.PlayerTurn == game.Player_1 ? game.Player_2 : game.Player_1;

        }

        public bool CanPlay(Game game)
        {
            // checks to see if the current player has any cards in their hand that can be played with out going over 31.
            // purpose to check if the 'GO' option should be available.

            if (game.PlayerTurn.Hand.Count > 0)
            {
                foreach (Card card in game.PlayerTurn.Hand)
                {
                    if (card.value + game.CurrentCount <= 31) return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public Card Pick_Card_To_Play(Game game)
        {
            // Checkes the current PlayerTurn.Hand to see which card should be played in the ralley.
            // Returns the card, does not play or remove the card from their hand.
            // Returns a card that makes 15,31, or a pair in that order.
            // If not any of those, returns the highest value card. Random, if multiple cards the same value.

            Card pickedCard = new Card();
            List<Card> potentialCards = new List<Card>();

            if (game.PlayedCards.Count > 0)
            {
                foreach (Card card in game.PlayerTurn.Hand)
                {
                    if (card.value + game.CurrentCount == 15 ||
                        card.value + game.CurrentCount == 31 ||
                        card.face == game.PlayedCards.Last().face)
                    {
                        return card;
                    }
                    else
                    {
                        // Count is not 0, Some cards have been played.
                        // No pair, 15, or 31 possibilities.
                        // Need to check if a card can be played and stay <= 31
                        if (card.value + game.CurrentCount <= 31)
                        {
                            potentialCards.Add(card);
                        }
                    }
                }
                // There are no pairs, 15s, 31.
                // Play the highest value card.
                int maxValue = potentialCards.Max(x => x.value);
                return potentialCards.Where(x => x.value == maxValue).FirstOrDefault();
            }
            else
            {
                // Count is 0. No Cards have been played yet, Choose the highest value one.
                int maxValue = game.PlayerTurn.Hand.Max(x => x.value);
                return game.PlayerTurn.Hand.Where(x => x.value == maxValue).FirstOrDefault();
            }

        }
        #endregion

        #region "Count Hand Points"
        public int CountHand(Game game, List<Card> hand, bool crib = false)
        {
            hand.Add(game.CutCard);
            int points_to_add = 0;

            //Check for pairs with 2 cards
            points_to_add += CountPairs(hand);

            //Check for 15s
            points_to_add += Count15s(hand);

            // Check for a Flush
            points_to_add += CountFlush(hand, crib);

            // Check for Runs
            points_to_add += CountRuns(hand);

            // check for knobs
            points_to_add += CountKnobs(game, hand);

            return points_to_add;

        }

        public int CountKnobs(Game game, List<Card> hand)
        {
            int points_to_add = 0;
            if (game.CutCard.face != Faces.Jack)
            {
                foreach (Card card in hand)
                {
                    if (card.face == Faces.Jack && card.suit == game.CutCard.suit) points_to_add++;
                }
            }

            return points_to_add;
        }

        public int CountFlush(List<Card> hand, bool crib = false)
        {
            // Check for flush
            // If a crib hand. all 5 cards need to be same suit.
            // if not crib hand. 4 of your original cards same suit or all 5 same suit.
            int points_to_add = 0;
            if (crib)
            {
                if (hand[0].suit == hand[1].suit &&
                    hand[0].suit == hand[2].suit &&
                    hand[0].suit == hand[3].suit &&
                    hand[0].suit == hand[4].suit) points_to_add += 5;
            }
            else
            {
                if (hand[0].suit == hand[1].suit &&
                    hand[0].suit == hand[2].suit &&
                    hand[0].suit == hand[3].suit &&
                    hand[0].suit == hand[4].suit) points_to_add += 5;
                else if (hand[0].suit == hand[1].suit &&
                          hand[0].suit == hand[2].suit &&
                          hand[0].suit == hand[3].suit) points_to_add += 4;
            }

            return points_to_add;
        }

        public int Count15s(List<Card> hand)
        {
            int points_to_add = 0;
            //Check for 15s with 2 cards
            for (int i = 0; i < hand.Count - 1; i++)
            {
                for (int j = i + 1; j < hand.Count; j++)
                {
                    if (hand[i].value + hand[j].value == 15) points_to_add += 2;
                }
            }

            // check for 15's with 3 cards
            for (int i = 0; i < hand.Count - 2; i++)
            {
                for (int j = i + 1; j < hand.Count - 1; j++)
                {
                    for (int k = j + 1; k < hand.Count; k++)
                    {
                        if (hand[i].value + hand[j].value + hand[k].value == 15) points_to_add += 2;
                    }
                }
            }

            // check for 15's with 4 cards
            for (int i = 0; i < hand.Count - 3; i++)
            {
                for (int j = i + 1; j < hand.Count - 2; j++)
                {
                    for (int k = j + 1; k < hand.Count - 1; k++)
                    {
                        for (int l = k + 1; l < hand.Count; l++)
                        {
                            if (hand[i].value + hand[j].value + hand[k].value + hand[l].value == 15) points_to_add += 2;
                        }

                    }
                }
            }

            // check for 15's with 5 cards
            if (hand.Count == 5)
            {
                if (hand[0].value + hand[1].value + hand[2].value + hand[3].value + hand[4].value == 15) points_to_add += 2;
            }

            return points_to_add;
        }

        public int CountPairs(List<Card> hand)
        {
            int points_to_add = 0;
            for (int i = 0; i < hand.Count - 1; i++)
            {
                for (int j = i + 1; j < hand.Count; j++)
                {
                    if (hand[i].face == hand[j].face) points_to_add += 2;
                }
            }

            return points_to_add;
        }

        public int CountRuns(List<Card> hand)
        {
            int runPoints = 0;
            bool hasRun = true;

            var cardsSorted = hand.OrderBy(c => c.face).ToList();

            // Check for a run of 5. if run of 5. Do not need to check for more runs. return 5 points.
            for (int i = 0; i < cardsSorted.Count - 1; i++)
            {
                if (cardsSorted[i].face + 1 != cardsSorted[i + 1].face)
                {
                    hasRun = false; break;
                }
            }

            if (hasRun)
            {
                runPoints = 5;
                return runPoints;
            }

            // runs with 4 cards. Start with hasRun = false to know if there is a run of 4
            // if run of 4, return the points because we dont want to count any runs of 3.
            hasRun = false;
            for (int i = 0; i < cardsSorted.Count - 3; i++)
            {
                for (int j = i + 1; j < cardsSorted.Count - 2; j++)
                {
                    for (int k = j + 1; k < cardsSorted.Count - 1; k++)
                    {
                        for (int l = k + 1; l < cardsSorted.Count; l++)
                        {
                            if (cardsSorted[i].face + 1 == cardsSorted[j].face &&
                                cardsSorted[j].face + 1 == cardsSorted[k].face &&
                                cardsSorted[k].face + 1 == cardsSorted[l].face)
                            {
                                hasRun = true;
                                runPoints += 4;
                            }
                        }

                    }
                }
            }
            if (hasRun)
            {
                return runPoints;
            }

            // check for runs with 3 cards
            for (int i = 0; i < cardsSorted.Count - 2; i++)
            {
                for (int j = i + 1; j < cardsSorted.Count - 1; j++)
                {
                    for (int k = j + 1; k < cardsSorted.Count; k++)
                    {
                        if (cardsSorted[i].face == cardsSorted[j].face - 1 &&
                            cardsSorted[j].face == cardsSorted[k].face - 1)
                        {
                            hasRun = true;
                            runPoints += 3;
                        }
                    }
                }
            }

            // Can not be any more runs. Return 0,3,6, or 9
            return runPoints;
        }
        #endregion



        #endregion
    }
}