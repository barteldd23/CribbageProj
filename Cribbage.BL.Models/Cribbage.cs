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
            Random rnd = new Random();
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < Deck.Cards.Count(); i++)
                {
                    int randNum = rnd.Next(Deck.Cards.Count());
                    var value = Deck.Cards[randNum];
                    Deck.Cards[randNum] = Deck.Cards[i];
                    Deck.Cards[i] = value;
                }
            }
            
        }
        public bool PlayCard(Card card)
        {
            if (card.value + CurrentCount <= 31)
            {
                CurrentCount += card.value;
                PlayedCards.Add(card);
                Check15();
                CheckPair();
                CheckRun();
                PlayerTurn = PlayerTurn == Player_1 ? Player_2 : Player_1;
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private void CheckRun()
        {
            if (PlayedCards.Count() < 3) return;

            List<Card> cards = PlayedCards;

            cards.Sort();

        }

        private void CheckPair()
        {
            int pts_To_Add = 0;
            for(int i = PlayedCards.Count() - 1; i > 0; i--)
            {
                if (PlayedCards[i].face == PlayedCards[i-1].face)
                {
                    pts_To_Add += 2;
                }
                else
                {
                    break;
                }
            }
            PlayerTurn.Score += pts_To_Add;
            return;
        }

        private void Check15()
        {
            if(CurrentCount == 15)
            {
                PlayerTurn.Score += 2;
            }
        }
    }
}
