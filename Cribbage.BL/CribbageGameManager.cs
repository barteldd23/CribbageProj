using Cribbage.BL.Models;
using System.Numerics;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cribbage.BL
{
    public static class CribbageGameManager
    { 
        #region Play CribbageGame Methods
        public static void NextDealer(CribbageGame cribbageGame)
        {
            if(cribbageGame.Dealer == 1)
            {
                cribbageGame.Dealer = 2;
            }
            else
            {
                cribbageGame.Dealer = 1;
            }
        }
        public static void NextPlayerAfterRally(CribbageGame cribbageGame)
        {
            Player otherPlayer;
            if (cribbageGame.LastPlayerPlayed.Id == cribbageGame.Player_1.Id)
            {
                otherPlayer = cribbageGame.Player_2;
            }
            else
            {
                otherPlayer = cribbageGame.Player_1;
            }

            cribbageGame.WhatToDo = "playcard";

            if (otherPlayer.Hand.Count > 0 && !cribbageGame.Complete)
            {
                cribbageGame.PlayerTurn = otherPlayer;
                
            }
        }
        public static void NextPlayer(CribbageGame cribbageGame)
        {
            // Someone played a card and it was not 31 or someone said go

            Player otherPlayer;
            if (cribbageGame.PlayerTurn.Id == cribbageGame.Player_1.Id)
            {
                otherPlayer = cribbageGame.Player_2;
                cribbageGame.PlayerTurn = cribbageGame.Player_1;
            }
            else
            {
                otherPlayer = cribbageGame.Player_1;
                cribbageGame.PlayerTurn = cribbageGame.Player_2;
            }
            // scenario p1 played a card, p2 has not said go and has cards. Needs to play a card or say go.
            if (!otherPlayer.SaidGo && otherPlayer.Hand.Count > 0)
            {
                cribbageGame.PlayerTurn = otherPlayer;
            }
            //scenario, p1 played a card, p2 already said go or has no cards, p1 needs to play a card but might need to say go.
            else if(!cribbageGame.PlayerTurn.SaidGo && cribbageGame.PlayerTurn.Hand.Count > 0)
            {
                // do nothing, playerTurn stays the same, p1 can play again, either playcard or go.
            }
            else if(cribbageGame.PlayerTurn.Hand.Count == 0)
            {
                EndCountingRally(cribbageGame);
            }
            //Scenario: Both players can not play, should go to the person after the last played player.
            //We dont know if p2 has no cards or just said go already.
            else if(otherPlayer.Hand.Count > 0)
            {
                cribbageGame.PlayerTurn = otherPlayer;
            }

            if (CanPlay(cribbageGame))
            {
                cribbageGame.WhatToDo = "playcard";
            }
            else
            {
                cribbageGame.WhatToDo = "go";
            }
        }
        public static bool CheckWinner(CribbageGame cribbageGame)
        {
            // Checks scores of both players. If there is a winner, changer Winner property to that player.
            // Change Complete to true if a winner.
            // Returns Complete bool value always.
            if (cribbageGame.Player_1.Score >= 121)
            {
                cribbageGame.Winner = cribbageGame.Player_1.Id;
                cribbageGame.Complete = true;
                cribbageGame.WhatToDo = "startnewgame";
            }
            else if (cribbageGame.Player_2.Score >= 121)
            {
                cribbageGame.Winner = cribbageGame.Player_2.Id;
                cribbageGame.Complete = true;
                cribbageGame.WhatToDo = "startnewgame";
            }
            return cribbageGame.Complete;
        }

        // Think about adding a AddPoints(int points, Player player) method

        #region "Starting the cribbageGame methods"
        public static void ShuffleDeck(CribbageGame cribbageGame)
        {
            // Go through each card twice. Randomly switch it with a different Index Location
            cribbageGame.Deck = new Deck(true);

            Random rnd = new Random();
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < cribbageGame.Deck.Cards.Count; i++)
                {
                    int randNum = rnd.Next(cribbageGame.Deck.Cards.Count);
                    var value = cribbageGame.Deck.Cards[randNum];
                    cribbageGame.Deck.Cards[randNum] = cribbageGame.Deck.Cards[i];
                    cribbageGame.Deck.Cards[i] = value;
                }
            }
        }
        public static void Deal(CribbageGame cribbageGame)
        {
            // Add the top card of the deck to the Players hand. Then remove that card from the Deck. 
            //EndCountingRally(cribbageGame);
            cribbageGame.GoCount = 0;
            cribbageGame.PlayedCards.Clear();
            cribbageGame.CurrentRally.Clear();
            cribbageGame.Player_1.Hand.Clear();
            cribbageGame.Player_2.Hand.Clear();
            cribbageGame.Player_1.PlayedCards.Clear();
            cribbageGame.Player_2.PlayedCards.Clear();
            cribbageGame.Player_1.SaidGo = false;
            cribbageGame.Player_2.SaidGo = false;
            cribbageGame.Player_1.CribPoints = 0;
            cribbageGame.Player_2.CribPoints = 0;
            cribbageGame.Crib.Clear();
            cribbageGame.CutCard = null;


            if (cribbageGame.Dealer == 1)
            {
                // Player 1 deals, Player 2 gets the first card.
                cribbageGame.Player_2.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);
                cribbageGame.Player_1.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);

                cribbageGame.Player_2.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);
                cribbageGame.Player_1.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);

                cribbageGame.Player_2.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);
                cribbageGame.Player_1.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);

                cribbageGame.Player_2.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);
                cribbageGame.Player_1.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);

                cribbageGame.Player_2.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);
                cribbageGame.Player_1.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);

                cribbageGame.Player_2.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);
                cribbageGame.Player_1.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);

                cribbageGame.PlayerTurn = cribbageGame.Player_2;
            }
            else
            {
                //Player 2 Deals. Player 1 gets the first card.
                cribbageGame.Player_1.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);
                cribbageGame.Player_2.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);

                cribbageGame.Player_1.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);
                cribbageGame.Player_2.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);

                cribbageGame.Player_1.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);
                cribbageGame.Player_2.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);

                cribbageGame.Player_1.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);
                cribbageGame.Player_2.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);

                cribbageGame.Player_1.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);
                cribbageGame.Player_2.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);

                cribbageGame.Player_1.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);
                cribbageGame.Player_2.Hand.Add(cribbageGame.Deck.Cards[0]);
                cribbageGame.Deck.Cards.RemoveAt(0);

                cribbageGame.PlayerTurn = cribbageGame.Player_1;
            }
        }

        public static void Cut(CribbageGame cribbageGame)
        {
            // choose a random index position to be the cut card.
            Random rnd = new Random();
            int index = rnd.Next(10, 31);
            cribbageGame.CutCard = cribbageGame.Deck.Cards[index];
            if (cribbageGame.CutCard.face == Faces.Jack)
            {
                if (cribbageGame.Dealer == 1) cribbageGame.Player_1.Score += 2;
                else cribbageGame.Player_2.Score += 2;

                CheckWinner(cribbageGame);
            }
        }

        public static void Cut(CribbageGame cribbageGame, int position)
        {
            if(position > 0 && position < 32)
            {
                cribbageGame.CutCard = cribbageGame.Deck.Cards[position - 1];
                if(cribbageGame.CutCard.face == Faces.Jack)
                {
                    cribbageGame.PlayerTurn.Score += 2;
                    CheckWinner(cribbageGame);
                }
            }
        }

        public static void Give_To_Crib(CribbageGame cribbageGame, List<Card> cards, Player player)
        {
            List<Card> notCribCards = new List<Card>();
            // Adds a List of cards to the crib and removes it from that players hand
            foreach (Card card in cards)
            {
                cribbageGame.Crib.Add(card);
                if (player.Id == cribbageGame.Player_1.Id)
                {
                    // .Remove(card) wasn't working properly
                    Card x = cribbageGame.Player_1.Hand.Where(x => x.name == card.name).FirstOrDefault();
                    cribbageGame.Player_1.Hand.Remove(x);
                }
                else
                {
                    Card x = cribbageGame.Player_2.Hand.Where(x => x.name == card.name).FirstOrDefault();
                    cribbageGame.Player_2.Hand.Remove(x);
                }
            }
            if(cribbageGame.PlayerTurn.Id == player.Id)
            {
                if(player.Id == cribbageGame.Player_1.Id)
                {
                    cribbageGame.PlayerTurn = cribbageGame.Player_1;
                }
                else
                {
                    cribbageGame.PlayerTurn = cribbageGame.Player_2;
                }
            }
            
        }

        public static List<Card> Pick_Cards_To_Crib(List<Card> hand)
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

        public static int Get_Points_of_4_Cards(List<Card> check_4_Cards)
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

        public static void Get_WhatToDO(CribbageGame cribbageGame)
        {
            if(CanPlay(cribbageGame))
            {
                cribbageGame.WhatToDo = "playcard";
            }
            else if(cribbageGame.PlayerTurn.Hand.Count > 0)
            {
                cribbageGame.WhatToDo = "go";
            }
            else if(cribbageGame.Player_1.Hand.Count == 0 && cribbageGame.Player_2.Hand.Count == 0)
            {
                cribbageGame.WhatToDo = "counthands";
            }
        }
        #endregion

        #region "Methods for counting rally"
        public static bool PlayCard(CribbageGame cribbageGame, Card card)
        {
            // Return true if the given card could be played. Return false if it can't be played.
            // It can be played if the card value added to the current count is <= 31.
            // Check if played card gets points with being 15 total, a pair, or a run.
            // add 2 points if total is 31.

            Card removed;

            if (card.value + cribbageGame.CurrentCount <= 31)
            {
                cribbageGame.PlayedCards.Add(card);
                cribbageGame.CurrentRally.Add(card);
                cribbageGame.LastPlayerPlayed = cribbageGame.PlayerTurn;
                Check15(cribbageGame);
                CheckPair(cribbageGame);
                CheckRun(cribbageGame);

                if (cribbageGame.Player_1.Id == cribbageGame.PlayerTurn.Id)
                {
                    removed = cribbageGame.Player_1.Hand.Where(c => c.name == card.name).FirstOrDefault();

                    cribbageGame.Player_1.Hand.Remove(removed);
                    cribbageGame.Player_1.PlayedCards.Add(removed);
                    cribbageGame.Player_1.Score = cribbageGame.PlayerTurn.Score;
                }
                else
                {
                    removed = cribbageGame.Player_2.Hand.Where(c => c.name == card.name).FirstOrDefault();

                    cribbageGame.Player_2.Hand.Remove(removed);
                    cribbageGame.Player_2.PlayedCards.Add(removed);
                    cribbageGame.Player_2.Score = cribbageGame.PlayerTurn.Score;
                }

                CheckWinner(cribbageGame);
                if (cribbageGame.CurrentCount == 31 && !cribbageGame.Complete)
                {
                    if(cribbageGame.PlayerTurn.Id == cribbageGame.Player_1.Id)
                    {
                        cribbageGame.Player_1.Score += 1;
                    }
                    else
                    {
                        cribbageGame.Player_2.Score += 1;
                    }

                    
                    EndCountingRally(cribbageGame);
                    
                    return true;

                }

                //cribbageGame.PlayerTurn.PlayedCards.Add(card);

                if (cribbageGame.Player_1.Hand.Count == 0 && cribbageGame.Player_2.Hand.Count == 0)
                {
                    EndCountingRally(cribbageGame);
                }
                else
                {
                    NextPlayer(cribbageGame);
                }
                CheckWinner(cribbageGame);
                return true;
            }
            else
            {
                return false;
            }

        }

        private static void EndCountingRally(CribbageGame cribbageGame)
        {
            //cribbageGame.LastPlayerPlayed.Score += 1;
            if(cribbageGame.LastPlayerPlayed.Id == cribbageGame.Player_1.Id)
            {
                cribbageGame.Player_1.Score += 1;
            }
            else
            {
                cribbageGame.Player_2.Score += 1;
            }

            cribbageGame.CurrentRally = null;
            cribbageGame.CurrentRally = new List<Card>();
            cribbageGame.GoCount = 0;
            cribbageGame.Player_1.SaidGo = false;
            cribbageGame.Player_2.SaidGo = false;
            CheckWinner(cribbageGame);
            if(cribbageGame.Player_1.Hand.Count == 0 && cribbageGame.Player_2.Hand.Count == 0 && !cribbageGame.Complete)
            {
                cribbageGame.WhatToDo = "counthands";
            }
            else
            {
                NextPlayerAfterRally(cribbageGame);
            }
            

        }

        private static bool CheckRun(CribbageGame cribbageGame)
        {
            // Can only check for a run if 3 or more cards have been played. End method if not.
            // Check by checking if all the cards make a run. Sort and check if faces are sequential. 
            // If all the cards don't make a run... then check for all but first played card and so on.

            //if (PlayedCards.Count < 3) return;

            // Need to test this. Also check that PlayedCards list is not changed.
            // make sure it is sorting by enum number instead of enum word.
            var cardsSorted = cribbageGame.CurrentRally.OrderBy(c => c.face).ToList();
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
                    cribbageGame.PlayerTurn.Score += cardsSorted.Count;

                    return true;
                }

                // Makes a new sorted list but after you remove the first card(s) played.
                cardsSorted = null;
                cardsSorted = cribbageGame.PlayedCards.GetRange(attempt, cribbageGame.CurrentRally.Count - attempt);
                cardsSorted = cardsSorted.OrderBy(c => c.face).ToList();
                attempt++;
            }

            return false;

        }

        private static bool CheckPair(CribbageGame cribbageGame)
        {
            // Check if Last 2 cards are a match. keep checking. When not a match, break out and add the points to players score.
            int pts_To_Add = 0;
            for (int i = 1; i < cribbageGame.CurrentRally.Count; i++)
            {
                if (cribbageGame.CurrentRally[cribbageGame.CurrentRally.Count - i].face == cribbageGame.CurrentRally[cribbageGame.CurrentRally.Count - i - 1].face)
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
                cribbageGame.PlayerTurn.Score += pts_To_Add;
                return true;
            }
            else
            {
                return false;
            }

        }

        private static bool Check15(CribbageGame cribbageGame)
        {

            // If current total is 15, add 2 points to current player score.

            if (cribbageGame.CurrentCount == 15)
            {
                cribbageGame.PlayerTurn.Score += 2;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Go(CribbageGame cribbageGame)
        {
            Player otherPlayer;
            if(cribbageGame.PlayerTurn.Id == cribbageGame.Player_1.Id)
            {
                otherPlayer = cribbageGame.Player_2;
                cribbageGame.PlayerTurn = cribbageGame.Player_1;
            }
            else
            {
                otherPlayer = cribbageGame.Player_1;
                cribbageGame.PlayerTurn = cribbageGame.Player_2;
            }

            if (cribbageGame.GoCount == 0 && otherPlayer.Hand.Count != 0)
            {
                cribbageGame.PlayerTurn.SaidGo = true;
                cribbageGame.GoCount++;
                NextPlayer(cribbageGame);
            }
            else
            {
                EndCountingRally(cribbageGame);
            }

        }

        public static bool CanPlay(CribbageGame cribbageGame)
        {
            // checks to see if the current player has any cards in their hand that can be played with out going over 31.
            // purpose to check if the 'GO' option should be available.

            if (cribbageGame.PlayerTurn.Hand.Count > 0)
            {
                foreach (Card card in cribbageGame.PlayerTurn.Hand)
                {
                    if (card.value + cribbageGame.CurrentCount <= 31) return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public static Card Pick_Card_To_Play(CribbageGame cribbageGame)
        {
            // Checkes the current PlayerTurn.Hand to see which card should be played in the ralley.
            // Returns the card, does not play or remove the card from their hand.
            // Returns a card that makes 15,31, or a pair in that order.
            // If not any of those, returns the highest value card. Random, if multiple cards the same value.

            Card pickedCard = new Card();
            List<Card> potentialCards = new List<Card>();

            if (cribbageGame.PlayedCards.Count > 0)
            {
                foreach (Card card in cribbageGame.PlayerTurn.Hand)
                {
                    if (card.value + cribbageGame.CurrentCount == 15 ||
                        card.value + cribbageGame.CurrentCount == 31 ||
                        (card.face == cribbageGame.PlayedCards.Last().face && card.value + cribbageGame.CurrentCount <= 31))
                    {
                        return card;
                    }
                    else
                    {
                        // Count is not 0, Some cards have been played.
                        // No pair, 15, or 31 possibilities.
                        // Need to check if a card can be played and stay <= 31
                        if (card.value + cribbageGame.CurrentCount <= 31)
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
                int maxValue = cribbageGame.PlayerTurn.Hand.Max(x => x.value);
                return cribbageGame.PlayerTurn.Hand.Where(x => x.value == maxValue).FirstOrDefault();
            }

        }
        #endregion

        #region "Count Hand Points"

        public static void CountHands(CribbageGame cribbageGame)
        {
            cribbageGame.Player_1.Hand = cribbageGame.Player_1.PlayedCards;
            cribbageGame.Player_2.Hand = cribbageGame.Player_2.PlayedCards;
            cribbageGame.Player_1.Hand.Add(cribbageGame.CutCard);
            cribbageGame.Player_2.Hand.Add(cribbageGame.CutCard);
            cribbageGame.Crib.Add(cribbageGame.CutCard);
            cribbageGame.WhatToDo = "startnewhand";

            if (cribbageGame.Dealer == 1)
            {
                cribbageGame.Player_2.HandPoints = CountHand(cribbageGame, cribbageGame.Player_2.Hand);
                cribbageGame.Player_2.Score += cribbageGame.Player_2.HandPoints;

                if (CheckWinner(cribbageGame))
                    return;

                cribbageGame.Player_1.HandPoints = CountHand(cribbageGame, cribbageGame.Player_1.Hand);
                cribbageGame.Player_1.Score += cribbageGame.Player_1.HandPoints;
                if (CheckWinner(cribbageGame))
                    return;

                cribbageGame.Player_1.CribPoints = CountHand(cribbageGame, cribbageGame.Crib, true);
                cribbageGame.Player_1.Score += cribbageGame.Player_1.CribPoints;
                CheckWinner(cribbageGame);
            }
            else if (cribbageGame.Dealer != 1)
            {
                cribbageGame.Player_1.HandPoints = CountHand(cribbageGame, cribbageGame.Player_1.Hand);
                cribbageGame.Player_1.Score += cribbageGame.Player_1.HandPoints;

                if (CheckWinner(cribbageGame))
                    return;

                cribbageGame.Player_2.HandPoints = CountHand(cribbageGame, cribbageGame.Player_2.Hand);
                cribbageGame.Player_2.Score += cribbageGame.Player_2.HandPoints;
                if (CheckWinner(cribbageGame))
                    return;

                cribbageGame.Player_2.CribPoints = CountHand(cribbageGame, cribbageGame.Crib, true);
                cribbageGame.Player_2.Score += cribbageGame.Player_2.CribPoints;
                CheckWinner(cribbageGame);
            }
        }
        public static int CountHand(CribbageGame cribbageGame, List<Card> hand, bool crib = false)
        {
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
            points_to_add += CountKnobs(cribbageGame, hand);

            return points_to_add;

        }

        public static int CountKnobs(CribbageGame cribbageGame, List<Card> hand)
        {
            int points_to_add = 0;
            if (cribbageGame.CutCard.face != Faces.Jack)
            {
                foreach (Card card in hand)
                {
                    if (card.face == Faces.Jack && card.suit == cribbageGame.CutCard.suit) points_to_add++;
                }
            }

            return points_to_add;
        }

        public static int CountFlush(List<Card> hand, bool crib = false)
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

        public static int Count15s(List<Card> hand)
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

        public static int CountPairs(List<Card> hand)
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

        public static int CountRuns(List<Card> hand)
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

    }

    #endregion
}