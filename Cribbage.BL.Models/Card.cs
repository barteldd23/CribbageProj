using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cribbage.BL.Models
{
    public enum Faces
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }

    public enum Suits
    {
        Hearts = 1,
        Diamonds = 2,
        Spades = 3,
        Clubs = 4
    }

    public class Card
    {
        public Faces face { get; set; }

        public Suits suit { get; set;}

        public int value { get { if (value < 10) return value; else return 10; } }
    }
}
