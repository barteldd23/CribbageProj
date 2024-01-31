using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage.BL.Models
{
    public class Cribbage
    {
        public Guid Id { get; set; }
        public Player Player_1 { get; set; }
        public Player Player_2 { get; set; }
        public Player Winner { get; set; } 
        public DateTime Date { get; set; } 
        public int Dealer { get; set; } = 1;
        public Player PlayerTurn { get; set; }
        public int GoCount { get; set; } = 0;
        public Player LastPlayerPlayed { get; set; }
        public Deck Deck { get; set; } = new Deck();
        public List<Card> Crib { get; set; } = new List<Card>();
        public Card CutCard { get; set; }
        public List<Card> PlayedCards { get; set; } = new List<Card>();
        public int CurrentCount { get; set; } = 0;

        [DisplayName("Team 1 Score")] 
        public int Team1_Score { get; set; }
        [DisplayName("Team 2 Score")]
        public int Team2_Score { get; set;}

        public void ShuffleDeck()
        {
            // Go through each card twice. Randomaly switch it with a different Index Location

            Random rnd = new Random();
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < Deck.Cards.Count; i++)
                {
                    int randNum = rnd.Next(Deck.Cards.Count);
                    var value = Deck.Cards[randNum];
                    Deck.Cards[randNum] = Deck.Cards[i];
                    Deck.Cards[i] = value;
                }
            }
        }
        public bool PlayCard(Card card)
        {
            // Return true if the given card could be played. Return false if it cant be played.
            // It can be played if the card value added to the current count is <= 31.
            // Check if played card gets points with being 15 total, a pair, or a run.
            // add 2 points if total is 31.

            if (card.value + CurrentCount <= 31)
            {
                CurrentCount += card.value;
                PlayedCards.Add(card);
                Check15();
                CheckPair();
                CheckRun();
                if(CurrentCount == 31)
                {
                    PlayerTurn.Score += 2;
                    EndCountingRally();

                }
                LastPlayerPlayed = PlayerTurn;
                PlayerTurn = PlayerTurn == Player_1 ? Player_2 : Player_1;
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private void EndCountingRally()
        {
            CurrentCount = 0;
            PlayedCards = null;
            PlayedCards = new List<Card>();

        }

        private bool CheckRun()
        {
            // Can only check for a run if 3 or more cards have been played. End method if not.
            // Check by checking if all the cards make a run. Sort and check if faces are sequential. 
            // if all the cards dont make a run... then check for all but first played card and so on.

            //if (PlayedCards.Count < 3) return;

            // Need to test this. Also check that PlayedCards list is not changed.
            // make sure it is sorting by enum number instead of enum word.
            var cardsSorted = PlayedCards.OrderBy(c => c.face).ToList();
            
            while( cardsSorted.Count >= 3)
            {
                // checks for the maximum length run possible first then continues looping until the minimum lenght run possible.

                int attempt = 1;
                bool run = true;
                
                for(int i = 0; i < cardsSorted.Count -1; i++) 
                {
                    if (cardsSorted[i].face +1 != cardsSorted[i+1].face)
                    {
                        run = false; break;
                    }
                }
                if (run)
                {
                    PlayerTurn.Score += cardsSorted.Count;
                    return true;
                }

                // Makes a new sorted list but after you remove the first card(s) played.
                cardsSorted = null;
                cardsSorted = PlayedCards.GetRange(attempt, PlayedCards.Count - attempt);
                cardsSorted = cardsSorted.OrderBy(c => c.face).ToList();
                attempt++; 
            }

            return false;

        }

        private bool CheckPair()
        {
            // Check if Last 2 cards are a match. keep checking. When not a match, break out and add the points to players score.
            int pts_To_Add = 0;
            for(int i = PlayedCards.Count - 1; i > 0; i--)
            {
                if (PlayedCards[i].face == PlayedCards[i-1].face)
                {
                    // First match, add 2 points.
                    // Second match(3 of a kind or 3pairs) add 4 more points
                    // Third match(4 of a kind or 6pairs) add 6 more points
                    pts_To_Add += 2*i;
                }
                else
                {
                    break;
                }
            }
            if( pts_To_Add > 0)
            {
                PlayerTurn.Score += pts_To_Add;
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private bool Check15()
        {

            // If current total is 15, add 2 points to current players score.

            if(CurrentCount == 15)
            {
                PlayerTurn.Score += 2;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Deal()
        {
            // Add the top card of the deck to the Players hand. Then Remove that card from the Deck. 

            if(Dealer == 1)
            {
                // Player 1 deals, Player 2 gets the first card.
                Player_2.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);
                Player_1.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);

                Player_2.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);
                Player_1.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);

                Player_2.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);
                Player_1.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);

                Player_2.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);
                Player_1.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);

                Player_2.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);
                Player_1.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);
            }
            else
            {
                //Player 2 Deals. Player 1 gets the first card.
                Player_1.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);
                Player_2.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);

                Player_1.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);
                Player_2.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);

                Player_1.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);
                Player_2.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);

                Player_1.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);
                Player_2.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);

                Player_1.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);
                Player_2.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);
            }
        }

        public void Go()
        {
            
            if (GoCount == 0)
            {
                GoCount++;
            }
            else
            {
                LastPlayerPlayed.Score += 1;
                EndCountingRally();
                GoCount = 0;
            }

            PlayerTurn = PlayerTurn == Player_1 ? Player_2 : Player_1;

        }

        public bool CanPlay()
        {
            // checks to see if the current player has any cards in their hand that can be played with out going over 31.
            // purpose to check if the 'GO' option should be available.

            if(PlayerTurn.Hand.Count > 0) 
            {
                foreach (Card card in PlayerTurn.Hand)
                {
                    if( card.value + CurrentCount <= 31) return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public void Cut()
        {
            // choose a random index position to be the cut card.
            Random rnd = new Random();
            int index = rnd.Next(10, 31);
            CutCard = Deck.Cards[index];
        }

        public int CountHand(List<Card> hand, bool crib = false)
        {
            hand.Add(CutCard);
            int points_to_add = 0;

            //Check for pairs and 15's with 2 cards
            for (int i = 0; i < hand.Count - 1; i++)
            {
                for (int j = i + 1; j < hand.Count; j++)
                {
                    if (hand[i].face == hand[j].face) points_to_add += 2;
                    if (hand[i].value + hand[j].value == 15) points_to_add += 2;
                }
            }

            // check for 15's with 3 cards
            for (int i = 0; i < hand.Count - 2; i++)
            {
                for (int j = i+1; j < hand.Count - 1; j++)
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
                    for (int k = j + 1; k < hand.Count -1; k++)
                    {
                        for (int l = k + 1; l < hand.Count; l++)
                        {
                            if (hand[i].value + hand[j].value + hand[k].value + hand[l].value == 15) points_to_add += 2;
                        }
                        
                    }
                }
            }

            // check for 15's with 5 cards
            if (hand[0].value + hand[1].value + hand[2].value + hand[3].value + hand[4].value == 15) points_to_add += 2;

            // Check for flush
            // If a crib hand. all 5 cards need to be same suit.
            // if not crib hand. 4 of your original cards same suit or all 5 same suit.
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

            // Check for Runs
            var cardsSorted = hand.OrderBy(c => c.face).ToList();

            while (cardsSorted.Count >= 3)
            {
                // checks for the maximum length run possible first then continues looping until the minimum lenght run possible.

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
                    points_to_add += cardsSorted.Count;
                    break;
                }

                // Makes a new sorted list but after you remove the first card(s) played.
                cardsSorted.RemoveAt(0);
            }

            // check for knobs
            if(CutCard.face != Faces.Jack)
            {
                foreach( Card card in hand)
                {
                    if (card.face == Faces.Jack) points_to_add++;
                }
            }

            return points_to_add;

        }
    }
}
