using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage.BL.Models
{
     public class Deck
    {
        public List<Card> Cards { get; set; } = new List<Card>();

        public Deck() 
        {
            for (int i = 1; i<= 4; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    Card card = new Card();
                    card.face = (Faces)j;
                    card.suit = (Suits)i;
                    Cards.Add(card);
                }
            }
        }
    }
}
