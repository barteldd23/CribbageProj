using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cribbage.BL.Models;

namespace Cribbage.BL.Test
{
    [TestClass]
    public class utCribbage
    {

        [TestMethod]
        public void cardValueTest()
        {
            Card card = new Card();
            card.face = Faces.Two;
            card.suit = Suits.Clubs;
            Assert.AreEqual(2, card.value);
        }


        [TestMethod]
        public void countPairsTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Deck deck = new Deck();
            List<Card> hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Nine).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault());

            int points = cribbage.CountPairs(hand);
            Assert.AreEqual(2, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Nine).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault());
            points = cribbage.CountPairs(hand);
            Assert.AreEqual(2, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault());
            points = cribbage.CountPairs(hand);
            Assert.AreEqual(4, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault());
            points = cribbage.CountPairs(hand);
            Assert.AreEqual(6, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault());
            points = cribbage.CountPairs(hand);
            Assert.AreEqual(8, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault());
            points = cribbage.CountPairs(hand);
            Assert.AreEqual(12, points);
        }

        [TestMethod]
        public void count15sTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Deck deck = new Deck();
            List<Card> hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Six).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Nine).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault());

            int points = cribbage.Count15s(hand);
            Assert.AreEqual(2, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Five).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault());
            points = cribbage.Count15s(hand);
            Assert.AreEqual(4, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Five).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault());
            points = cribbage.Count15s(hand);
            Assert.AreEqual(8, points);
        }

        [TestMethod]
        public void countRunsTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Deck deck = new Deck();
            List<Card> hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Four).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Nine).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault());

            int points = cribbage.CountRuns(hand);
            Assert.AreEqual(3, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Two).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Four).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault());
            points = cribbage.CountRuns(hand);
            Assert.AreEqual(5, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault());
            points = cribbage.CountRuns(hand);
            Assert.AreEqual(0, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Queen).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault());
            points = cribbage.CountRuns(hand);
            Assert.AreEqual(4, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Nine).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Eight).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Seven).FirstOrDefault());
            points = cribbage.CountRuns(hand);
            Assert.AreEqual(6, points);

            

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Four).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Two).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Four).FirstOrDefault());
            points = cribbage.CountRuns(hand);
            Assert.AreEqual(8, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Nine).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Eight).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Nine).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Nine).FirstOrDefault());
            points = cribbage.CountRuns(hand);
            Assert.AreEqual(9, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Four).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault());
            points = cribbage.CountRuns(hand);
            Assert.AreEqual(12, points);
        }

        [TestMethod]
        public void countFlushTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Deck deck = new Deck();
            List<Card> hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Four).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Nine).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault());

            int points = cribbage.CountFlush(hand);
            Assert.AreEqual(5, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Two).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault());
            points = cribbage.CountFlush(hand);
            Assert.AreEqual(0, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Two).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Four).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault());
            points = cribbage.CountFlush(hand);
            Assert.AreEqual(4, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Two).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Four).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault());
            points = cribbage.CountFlush(hand, true);
            Assert.AreEqual(0, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Two).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Four).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Five).FirstOrDefault());
            points = cribbage.CountFlush(hand, true);
            Assert.AreEqual(5, points);
        }

        [TestMethod]
        public void countKnobsTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Deck deck = new Deck();
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault();
            List<Card> hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Jack).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault());

            int points = cribbage.CountKnobs(hand);
            Assert.AreEqual(1, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Two).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Jack).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault());
            points = cribbage.CountKnobs(hand);
            Assert.AreEqual(0, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Two).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Ten).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault());
            points = cribbage.CountKnobs(hand);
            Assert.AreEqual(0, points);
        }

        [TestMethod]
        public void countHandTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Deck deck = new Deck();
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault();
            List<Card> hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Jack).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Five).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Seven).FirstOrDefault());
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Two).FirstOrDefault();

            int points = cribbage.CountHand(hand);
            Assert.AreEqual(5, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Six).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Eight).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Seven).FirstOrDefault());
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Three).FirstOrDefault();
            points = cribbage.CountHand(hand);
            Assert.AreEqual(7, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Three).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault());
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault();
            points = cribbage.CountHand(hand);
            Assert.AreEqual(17, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Five).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Five).FirstOrDefault());
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault();
            points = cribbage.CountHand(hand);
            Assert.AreEqual(29, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Six).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Nine).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Eight).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Seven).FirstOrDefault());
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Two).FirstOrDefault();
            points = cribbage.CountHand(hand);
            Assert.AreEqual(15, points);

            hand = null;
            hand = new List<Card>();
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Two).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Eight).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ten).FirstOrDefault());
            hand.Add(deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Ace).FirstOrDefault());
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault();
            points = cribbage.CountHand(hand);
            Assert.AreEqual(0, points);
        }
    }
}
