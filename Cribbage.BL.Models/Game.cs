using System.ComponentModel;

namespace Cribbage.BL.Models
{
    public class Game
    {
        #region "Properties"
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
        public int CurrentCount { get { return getCount(); } } 

        [DisplayName("Team 1 Score")] 
        public int Team1_Score { get; set; }
        [DisplayName("Team 2 Score")]
        public int Team2_Score { get; set;}

        public int getCount()
        {
            int count = 0;
            if (PlayedCards.Count > 0)
            {
                foreach(Card card in PlayedCards)
                {
                    count += card.value;
                }
            }
            return count;
        }
        #endregion

        #region "Starting the game methods"
        public void ShuffleDeck()
        {
            // Go through each card twice. Randomly switch it with a different Index Location

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
        public void Deal()
        {
            // Add the top card of the deck to the Players hand. Then remove that card from the Deck. 

            if (Dealer == 1)
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

                Player_1.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);
                Player_2.Hand.Add(Deck.Cards[0]);
                Deck.Cards.RemoveAt(0);
            }
        }

        public void Cut()
        {
            // choose a random index position to be the cut card.
            Random rnd = new Random();
            int index = rnd.Next(10, 31);
            CutCard = Deck.Cards[index];
            if(CutCard.face == Faces.Jack)
            {
                if (Dealer == 1) Player_1.Score += 2;
                else Player_2.Score += 2;
            }
        }
        
        public void Give_To_Crib(List<Card> cards)
        {
            foreach (Card card in cards)
            {
                Crib.Add(card);
            }
        }
        #endregion

        #region "Methods for counting rally"
        public bool PlayCard(Card card)
        {
            // Return true if the given card could be played. Return false if it can't be played.
            // It can be played if the card value added to the current count is <= 31.
            // Check if played card gets points with being 15 total, a pair, or a run.
            // add 2 points if total is 31.

            if (card.value + CurrentCount <= 31)
            {
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
            PlayedCards = null;
            PlayedCards = new List<Card>();

        }

        private bool CheckRun()
        {
            // Can only check for a run if 3 or more cards have been played. End method if not.
            // Check by checking if all the cards make a run. Sort and check if faces are sequential. 
            // If all the cards don't make a run... then check for all but first played card and so on.

            //if (PlayedCards.Count < 3) return;

            // Need to test this. Also check that PlayedCards list is not changed.
            // make sure it is sorting by enum number instead of enum word.
            var cardsSorted = PlayedCards.OrderBy(c => c.face).ToList();
            int attempt = 1;
            while ( cardsSorted.Count >= 3)
            {
                

                // checks for the maximum length run possible first then continues looping until the minimum length run possible.

                
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
            for(int i = 1; i < PlayedCards.Count; i++)
            {
                if (PlayedCards[PlayedCards.Count - i].face == PlayedCards[PlayedCards.Count - i - 1].face)
                {
                    // First match, add 2 points.
                    // Second match (3 of a kind or 3 pairs) add 4 more points
                    // Third match (4 of a kind or 6 pairs) add 6 more points
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

            // If current total is 15, add 2 points to current player score.

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

            if (PlayerTurn.Hand.Count > 0)
            {
                foreach (Card card in PlayerTurn.Hand)
                {
                    if (card.value + CurrentCount <= 31) return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        #endregion
               
        #region "Count Hand Points"
        public int CountHand(List<Card> hand, bool crib = false)
        {
            hand.Add(CutCard);
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
            points_to_add += CountKnobs(hand);

            return points_to_add;

        }

        public int CountKnobs(List<Card> hand)
        {
            int points_to_add = 0;
            if (CutCard.face != Faces.Jack)
            {
                foreach (Card card in hand)
                {
                    if (card.face == Faces.Jack && card.suit == CutCard.suit) points_to_add++;
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
            if (hand[0].value + hand[1].value + hand[2].value + hand[3].value + hand[4].value == 15) points_to_add += 2;
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
                            cardsSorted[j].face == cardsSorted[k].face - 1 )
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
}
