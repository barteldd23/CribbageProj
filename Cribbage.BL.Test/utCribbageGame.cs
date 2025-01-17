﻿using Microsoft.Extensions.Options;

namespace Cribbage.BL.Test
{
    [TestClass]
    public class utCribbageGame : utBase
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
            List<Card> hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Nine).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault(),
            ];
            
            int points = CribbageGameManager.CountPairs(hand);
            Assert.AreEqual(2, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Nine).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountPairs(hand);
            Assert.AreEqual(2, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountPairs(hand);
            Assert.AreEqual(4, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountPairs(hand);
            Assert.AreEqual(6, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountPairs(hand);
            Assert.AreEqual(8, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountPairs(hand);
            Assert.AreEqual(12, points);
        }

        [TestMethod]
        public void count15sTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Deck deck = new Deck();
            List<Card> hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Six).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Nine).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault(),
            ];

            int points = CribbageGameManager.Count15s(hand);
            Assert.AreEqual(2, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Five).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
            ];
            points = CribbageGameManager.Count15s(hand);
            Assert.AreEqual(4, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Five).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
            ];
            points = CribbageGameManager.Count15s(hand);
            Assert.AreEqual(8, points);
        }

        [TestMethod]
        public void countRunsTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Deck deck = new Deck();
            List<Card> hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Nine).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
            ];

            int points = CribbageGameManager.CountRuns(hand);
            Assert.AreEqual(3, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Two).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountRuns(hand);
            Assert.AreEqual(5, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountRuns(hand);
            Assert.AreEqual(0, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Queen).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountRuns(hand);
            Assert.AreEqual(4, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Nine).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Eight).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Seven).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountRuns(hand);
            Assert.AreEqual(6, points);

            

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Two).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Four).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountRuns(hand);
            Assert.AreEqual(8, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Nine).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Eight).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Nine).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Nine).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountRuns(hand);
            Assert.AreEqual(9, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountRuns(hand);
            Assert.AreEqual(12, points);
        }

        [TestMethod]
        public void countFlushTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Deck deck = new Deck();
            List<Card> hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Nine).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
            ];

            int points = CribbageGameManager.CountFlush(hand);
            Assert.AreEqual(5, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Two).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountFlush(hand);
            Assert.AreEqual(0, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Two).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountFlush(hand);
            Assert.AreEqual(4, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Two).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountFlush(hand, true);
            Assert.AreEqual(0, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Two).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Five).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountFlush(hand, true);
            Assert.AreEqual(5, points);
        }

        [TestMethod]
        public void countKnobsTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Deck deck = new Deck();
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault();
            List<Card> hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Jack).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
            ];

            int points = CribbageGameManager.CountKnobs(cribbage, hand);
            Assert.AreEqual(1, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Two).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Jack).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountKnobs(cribbage, hand);
            Assert.AreEqual(0, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Two).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Ten).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault(),
            ];
            points = CribbageGameManager.CountKnobs(cribbage, hand);
            Assert.AreEqual(0, points);
        }

        [TestMethod]
        public void countHandTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Deck deck = new Deck();
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault();
            List<Card> hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Jack).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Five).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Seven).FirstOrDefault(),
            ];
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Two).FirstOrDefault();

            int points = CribbageGameManager.CountHand(cribbage, hand);
            Assert.AreEqual(5, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Six).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Eight).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Seven).FirstOrDefault(),
            ];
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Three).FirstOrDefault();
            points = CribbageGameManager.CountHand(cribbage, hand);
            Assert.AreEqual(7, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault(),
            ];
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault();
            points = CribbageGameManager.CountHand(cribbage, hand);
            Assert.AreEqual(17, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Five).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Five).FirstOrDefault(),
            ];
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault();
            points = CribbageGameManager.CountHand(cribbage, hand);
            Assert.AreEqual(29, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Six).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Nine).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Eight).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Seven).FirstOrDefault(),
            ];
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Two).FirstOrDefault();
            points = CribbageGameManager.CountHand(cribbage, hand);
            Assert.AreEqual(15, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Two).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Eight).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ten).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Ace).FirstOrDefault(),
            ];
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault();
            points = CribbageGameManager.CountHand(cribbage, hand);
            Assert.AreEqual(0, points);
        }

        [TestMethod]
        public void startGameTest()
        {
            // Test Deal() hands out cards from a shuffled deck
            // Test Cut() initializes a cut card
            // test Give_To_Crib() puts cards in the crib property

            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            CribbageGameManager.ShuffleDeck(cribbage);
            CribbageGameManager.Deal(cribbage);

            Assert.AreEqual(6, cribbage.Player_1.Hand.Count);
            Assert.AreEqual(6, cribbage.Player_2.Hand.Count);
            Assert.AreEqual(40, cribbage.Deck.Cards.Count);

            CribbageGameManager.Cut(cribbage);
            Assert.IsTrue(cribbage.CutCard != null);

            List<Card> cards = [cribbage.Player_1.Hand[5], cribbage.Player_1.Hand[4]];
            CribbageGameManager.Give_To_Crib(cribbage, cards, cribbage.Player_1);
            cards = null;
            cards = [cribbage.Player_2.Hand[5], cribbage.Player_2.Hand[4]];
            CribbageGameManager.Give_To_Crib(cribbage, cards, cribbage.Player_2);

            Assert.AreEqual(4, cribbage.Crib.Count);
        }

        [TestMethod]
        public void playCardTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            player1.DisplayName = "P1";
            player2.DisplayName = "P2";
            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;

            CribbageGameManager.ShuffleDeck(cribbage);
            CribbageGameManager.Deal(cribbage);
            cribbage.PlayerTurn = cribbage.Player_1;

            List<Card> cards = [cribbage.Player_1.Hand[5], cribbage.Player_1.Hand[4]];
            CribbageGameManager.Give_To_Crib(cribbage, cards, cribbage.Player_1);
            cards = null;
            cards = [cribbage.Player_2.Hand[5], cribbage.Player_2.Hand[4]];
            CribbageGameManager.Give_To_Crib(cribbage, cards, cribbage.Player_2);

            CribbageGameManager.PlayCard(cribbage, cribbage.PlayerTurn.Hand[0]);
            Assert.IsTrue(cribbage.PlayedCards.Count == 1);
            //Assert.IsTrue(cribbage.PlayerTurn == cribbage.Player_2);
        }

        [TestMethod]
        public void playCardTest2()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.PlayerTurn = cribbage.Player_1;

            List<Card> cards = [cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault()];
            cribbage.PlayedCards = cards; ;

            Card card = cribbage.Deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.King).FirstOrDefault();

            CribbageGameManager.PlayCard(cribbage, card);
            Assert.IsTrue(cribbage.PlayedCards.Count == 2);
            Assert.AreEqual(2, cribbage.Player_1.Score);
            Assert.AreEqual(20, cribbage.CurrentCount);
        }

        [TestMethod]
        public void playCardTest3()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.PlayerTurn = cribbage.Player_1;

            List<Card> cards = [cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault()];
            cribbage.PlayedCards = cards; 

            Card card = new Card();
            card = cribbage.Deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Eight).FirstOrDefault();


            CribbageGameManager.PlayCard(cribbage, card);

            Assert.IsTrue(cribbage.PlayedCards.Count == 2);
            Assert.AreEqual(2, cribbage.Player_1.Score);
            Assert.AreEqual(15, cribbage.CurrentCount);

        }

        [TestMethod]
        public void playCardTest4()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.PlayerTurn = cribbage.Player_1;

            List<Card> cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
            ];
            cribbage.PlayedCards = cards;

            Card card = new Card();
            card = cribbage.Deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Jack).FirstOrDefault();

            CribbageGameManager.PlayCard(cribbage, card);

            Assert.AreEqual(3, cribbage.PlayedCards.Count);
            Assert.AreEqual(3, cribbage.Player_1.Score);
            Assert.AreEqual(30, cribbage.CurrentCount);
        }

        [TestMethod]
        public void playCardTest5()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.PlayerTurn = cribbage.Player_1;

            List<Card> cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
            ];
            cribbage.CurrentRally = cards;

            Card card = new Card();
            card = cribbage.Deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault();

            CribbageGameManager.PlayCard(cribbage, card);

            Assert.AreEqual(0, cribbage.CurrentRally.Count);
            Assert.AreEqual(2, cribbage.Player_1.Score);
            Assert.AreEqual(0, cribbage.CurrentCount);
            Assert.AreEqual(cribbage.Player_2, cribbage.PlayerTurn);
        }

        [TestMethod]
        public void playCardTest6()
        {
            CribbageGame cribbage = new CribbageGame(true);
            Player player1 = new Player();
            Player player2 = new Player();
            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;

            List<Card> p1Cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Six).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Two).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
            ];

            List<Card> p2Cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
            ];

            cribbage.Player_1.Hand = p1Cards;
            cribbage.Player_2.Hand = p2Cards;
            cribbage.PlayerTurn = cribbage.Player_1;



            List<Card> cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Two).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault(),
            ];
            cribbage.PlayedCards = cards;
            cribbage.CurrentRally.Add(cards[0]);
            cribbage.CurrentRally.Add(cards[1]);
            cribbage.CurrentRally.Add(cards[2]);

            Card card = new Card();
            card = cribbage.Deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Six).FirstOrDefault();

            CribbageGameManager.PlayCard(cribbage, cribbage.Player_1.Hand[0]);

            Assert.AreEqual(4, cribbage.PlayedCards.Count);
            Assert.AreEqual(3, cribbage.Player_1.Score);
            Assert.AreEqual(20, cribbage.CurrentCount);
            Assert.AreEqual(cribbage.Player_2, cribbage.PlayerTurn);
        }

        [TestMethod]
        public void playCardTest7()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.PlayerTurn = cribbage.Player_1;

            List<Card> cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Two).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
            ];
            cribbage.PlayedCards = cards;

            Card card = new Card();
            card = cribbage.Deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Five).FirstOrDefault();

            CribbageGameManager.PlayCard(cribbage, card);

            Assert.AreEqual(4, cribbage.PlayedCards.Count);
            Assert.AreEqual(6, cribbage.Player_1.Score);
            Assert.AreEqual(17, cribbage.CurrentCount);
            Assert.AreEqual(cribbage.Player_2, cribbage.PlayerTurn);
        }

        [TestMethod]
        public void playCardTest8()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.PlayerTurn = cribbage.Player_1;

            List<Card> cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault(),
            ];
            cribbage.PlayedCards = cards;

            Card card = new Card();
            card = cribbage.Deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Eight).FirstOrDefault();

            CribbageGameManager.PlayCard(cribbage, card);

            CribbageGameManager.Go(cribbage);
            CribbageGameManager.Go(cribbage);
            Assert.AreEqual(0, cribbage.CurrentRally.Count);
            Assert.AreEqual(1, cribbage.Player_1.Score);
            Assert.AreEqual(0, cribbage.CurrentCount);
            Assert.AreEqual(cribbage.Player_2, cribbage.PlayerTurn);
        }

        [TestMethod]
        public void getPointsof4Test()
        {
            CribbageGame cribbage = new CribbageGame();
            Deck deck = new Deck();
            cribbage.CutCard = deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault();
            List<Card> hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Jack).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Five).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Seven).FirstOrDefault(),
            ];

            int points = CribbageGameManager.Get_Points_of_4_Cards(hand);
            Assert.AreEqual(4, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Six).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Eight).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Seven).FirstOrDefault(),
            ];
            points = CribbageGameManager.Get_Points_of_4_Cards(hand);
            Assert.AreEqual(5, points);

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Six).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Five).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault(),
            ];
            points = CribbageGameManager.Get_Points_of_4_Cards(hand);
            Assert.AreEqual(12, points);
        }

        [TestMethod]
        public void getCribCardsTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Deck deck = new Deck();
            List<Card> hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Jack).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Five).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Seven).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Two).FirstOrDefault()
            ];

            List<Card> cribCards = CribbageGameManager.Pick_Cards_To_Crib(hand);
            Assert.AreEqual(2, cribCards.Count);

            hand = null;
            cribCards = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Ten).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ten).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault()
            ];

            cribCards = CribbageGameManager.Pick_Cards_To_Crib(hand);
            Assert.AreEqual(2, cribCards.Count);

            hand = null;
            cribCards = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Queen).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Two).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Four).FirstOrDefault()
            ];

            cribCards = CribbageGameManager.Pick_Cards_To_Crib(hand);
            Assert.AreEqual(2, cribCards.Count);

            hand = null;
            cribCards = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Ten).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Eight).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Seven).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Six).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Five).FirstOrDefault()
            ];

            cribCards = CribbageGameManager.Pick_Cards_To_Crib(hand);
            Assert.AreEqual(2, cribCards.Count);

        }

        [TestMethod]
        public void getCardToPlayTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Deck deck = new Deck();
            cribbage.PlayerTurn = new Player();

            List<Card> playedCards =
                [
                    deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault()
                ];
            cribbage.PlayedCards = playedCards;

            List<Card> hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Jack).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Five).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Eight).FirstOrDefault(),
            ];

            cribbage.PlayerTurn.Hand = new List<Card>();
            cribbage.PlayerTurn.Hand = hand;
            Card card = CribbageGameManager.Pick_Card_To_Play(cribbage);
            Assert.AreEqual(card, hand[3]);

            cribbage.PlayerTurn.Hand = null;
            cribbage.PlayerTurn.Hand = new List<Card>();
            cribbage.PlayedCards = null;
            cribbage.PlayedCards = new List<Card>();

            playedCards =
                [
                    deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                    deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault(),
                    deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault(),
                ];
            cribbage.PlayedCards = playedCards;

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Six).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Eight).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Seven).FirstOrDefault(),
            ];

            cribbage.PlayerTurn.Hand = new List<Card>();
            cribbage.PlayerTurn.Hand = hand;
            card = CribbageGameManager.Pick_Card_To_Play(cribbage);
            Assert.AreEqual(card, hand[2]);

            cribbage.PlayerTurn.Hand = null;
            cribbage.PlayerTurn.Hand = new List<Card>();
            cribbage.PlayedCards = null;
            cribbage.PlayedCards = new List<Card>();

            playedCards =
                [
                    deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                    deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault(),
                    deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Two).FirstOrDefault(),
                ];
            cribbage.PlayedCards = playedCards;

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Six).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Eight).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Seven).FirstOrDefault(),
            ];

            cribbage.PlayerTurn.Hand = new List<Card>();
            cribbage.PlayerTurn.Hand = hand;
            card = CribbageGameManager.Pick_Card_To_Play(cribbage);
            Assert.AreEqual(card, hand[1]);

            cribbage.PlayerTurn.Hand = null;
            cribbage.PlayerTurn.Hand = new List<Card>();
            cribbage.PlayedCards = null;
            cribbage.PlayedCards = new List<Card>();

            //playedCards =
            //    [
            //        deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
            //        deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault(),
            //        deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault(),
            //    ];
            //cribbage.PlayedCards = playedCards;

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Six).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Eight).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Four).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Jack).FirstOrDefault(),
            ];

            cribbage.PlayerTurn.Hand = new List<Card>();
            cribbage.PlayerTurn.Hand = hand;
            card = CribbageGameManager.Pick_Card_To_Play(cribbage);
            Assert.AreEqual(card, hand[3]);


            cribbage.PlayerTurn.Hand = null;
            cribbage.PlayerTurn.Hand = new List<Card>();
            cribbage.PlayedCards = null;
            cribbage.PlayedCards = new List<Card>();

            playedCards =
                [
                    deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Nine).FirstOrDefault(),
                    deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault()
                ];
            cribbage.PlayedCards = playedCards;

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Nine).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Eight).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.King).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Jack).FirstOrDefault(),
            ];

            cribbage.PlayerTurn.Hand = new List<Card>();
            cribbage.PlayerTurn.Hand = hand;
            card = CribbageGameManager.Pick_Card_To_Play(cribbage);
            Assert.AreEqual(card, hand[2]);

            cribbage.PlayerTurn.Hand = null;
            cribbage.PlayerTurn.Hand = new List<Card>();
            cribbage.PlayedCards = null;
            cribbage.PlayedCards = new List<Card>();

            playedCards =
                [
                    deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault(),
                    deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault()
                ];
            cribbage.PlayedCards = playedCards;

            hand = null;
            hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Two).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault(),
            ];

            cribbage.PlayerTurn.Hand = new List<Card>();
            cribbage.PlayerTurn.Hand = hand;
            card = CribbageGameManager.Pick_Card_To_Play(cribbage);
            Assert.AreEqual(Faces.Five, card.face);
        }

        [TestMethod]
        public void PlayCardNextCanPlayTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            player1.DisplayName = "p1";
            player2.DisplayName = "p2";

            List<Card> p1cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Eight).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault(),
            ];

            List<Card> p2cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Eight).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Two).FirstOrDefault(),
            ];

            List<Card> playedCards = new List<Card>();


            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.Player_1.Hand = p1cards;
            cribbage.Player_2.Hand = p2cards;
            cribbage.PlayerTurn = cribbage.Player_1;
            
            cribbage.PlayedCards = playedCards;
            cribbage.CurrentRally = playedCards;

            CribbageGameManager.PlayCard(cribbage, cribbage.PlayerTurn.Hand[0]);
            Assert.AreEqual(cribbage.PlayerTurn, cribbage.Player_2);
            Assert.AreEqual(cribbage.WhatToDo, "playcard");
            Assert.AreEqual(cribbage.Player_1.Hand.Count, 3);
            Assert.AreEqual(cribbage.PlayedCards.Count, 1);
            Assert.AreEqual(cribbage.CurrentRally.Count, 1);
            Assert.AreEqual(cribbage.CurrentCount, 10);
        }
        [TestMethod]
        public void PlayCardNextCanGoTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            player1.DisplayName = "p1";
            player2.DisplayName = "p2";

            List<Card> p1cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Eight).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault(),
            ];

            List<Card> p2cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
            ];

            List<Card> playedCards = new List<Card>();
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Eight).FirstOrDefault());


            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.Player_1.Hand = p1cards;
            cribbage.Player_2.Hand = p2cards;
            cribbage.PlayerTurn = cribbage.Player_1;

            cribbage.PlayedCards = playedCards;
            cribbage.CurrentRally.Add(playedCards[0]);
            cribbage.CurrentRally.Add(playedCards[1]);



            CribbageGameManager.PlayCard(cribbage, cribbage.PlayerTurn.Hand[0]);
            Assert.AreEqual(cribbage.PlayerTurn, cribbage.Player_2);
            Assert.AreEqual(cribbage.WhatToDo, "go");
            Assert.AreEqual(cribbage.Player_1.Hand.Count, 2);
            Assert.AreEqual(3, cribbage.PlayedCards.Count);
            Assert.AreEqual(cribbage.CurrentRally.Count, 3);
            Assert.AreEqual(cribbage.CurrentCount, 28);
        }

        [TestMethod]
        public void PlayCardNextSaidGoIPlayTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            player1.DisplayName = "p1";
            player2.DisplayName = "p2";

            List<Card> p1cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault(),
            ];

            List<Card> p2cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
            ];

            List<Card> playedCards = new List<Card>();
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Eight).FirstOrDefault());


            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.Player_1.Hand = p1cards;
            cribbage.Player_2.Hand = p2cards;
            cribbage.PlayerTurn = cribbage.Player_1;

            cribbage.PlayedCards = playedCards;
            cribbage.CurrentRally.Add(playedCards[0]);
            cribbage.CurrentRally.Add(playedCards[1]);

            cribbage.GoCount = 1;
            cribbage.Player_2.SaidGo = true;

            CribbageGameManager.PlayCard(cribbage, cribbage.PlayerTurn.Hand[0]);
            Assert.AreEqual(cribbage.PlayerTurn, cribbage.Player_1);
            Assert.AreEqual(cribbage.WhatToDo, "playcard");
            Assert.AreEqual(cribbage.Player_1.Hand.Count, 2);
            Assert.AreEqual(3, cribbage.PlayedCards.Count);
            Assert.AreEqual(cribbage.CurrentRally.Count, 3);
            Assert.AreEqual(cribbage.CurrentCount, 28);
        }
        [TestMethod]
        public void PlayCardNextSaidGoIGoTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            player1.DisplayName = "p1";
            player2.DisplayName = "p2";

            List<Card> p1cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Nine).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault(),
            ];

            List<Card> p2cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
            ];

            List<Card> playedCards = new List<Card>();
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Eight).FirstOrDefault());


            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.Player_1.Hand = p1cards;
            cribbage.Player_2.Hand = p2cards;
            cribbage.PlayerTurn = cribbage.Player_1;

            cribbage.PlayedCards = playedCards;
            cribbage.CurrentRally.Add(playedCards[0]);
            cribbage.CurrentRally.Add(playedCards[1]);

            cribbage.GoCount = 1;
            cribbage.Player_2.SaidGo = true;

            CribbageGameManager.PlayCard(cribbage, cribbage.PlayerTurn.Hand[0]);
            Assert.AreEqual(cribbage.PlayerTurn, cribbage.Player_1);
            Assert.AreEqual(cribbage.WhatToDo, "go");
            Assert.AreEqual(cribbage.Player_1.Hand.Count, 2);
            Assert.AreEqual(3, cribbage.PlayedCards.Count);
            Assert.AreEqual(cribbage.CurrentRally.Count, 3);
            Assert.AreEqual(cribbage.CurrentCount, 28);
        }

        [TestMethod]
        public void PlayCardNext0CardsIPlayTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            player1.DisplayName = "p1";
            player2.DisplayName = "p2";

            List<Card> p1cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Two).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
            ];

            List<Card> p2cards = new List<Card>();
            //[
            //    cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
            //    cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
            //    cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
            //];

            List<Card> playedCards = new List<Card>();
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Eight).FirstOrDefault());


            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.Player_1.Hand = p1cards;
            cribbage.Player_2.Hand = p2cards;
            cribbage.PlayerTurn = cribbage.Player_1;

            cribbage.PlayedCards = playedCards;
            cribbage.CurrentRally.Add(playedCards[0]);
            cribbage.CurrentRally.Add(playedCards[1]);

            cribbage.GoCount = 1;
            cribbage.Player_2.SaidGo = false;

            CribbageGameManager.PlayCard(cribbage, cribbage.PlayerTurn.Hand[0]);
            Assert.AreEqual(cribbage.PlayerTurn, cribbage.Player_1);
            Assert.AreEqual(cribbage.WhatToDo, "playcard");
            Assert.AreEqual(cribbage.Player_1.Hand.Count, 2);
            Assert.AreEqual(3, cribbage.PlayedCards.Count);
            Assert.AreEqual(cribbage.CurrentRally.Count, 3);
            Assert.AreEqual(cribbage.CurrentCount, 19);
        }
        [TestMethod]
        public void PlayCardNext0CardsIGoTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            player1.DisplayName = "p1";
            player2.DisplayName = "p2";

            List<Card> p1cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Nine).FirstOrDefault(),
            ];

            List<Card> p2cards = new List<Card>();
            //[
            //    cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
            //    cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
            //    cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
            //];

            List<Card> playedCards = new List<Card>();
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Eight).FirstOrDefault());


            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.Player_1.Hand = p1cards;
            cribbage.Player_2.Hand = p2cards;
            cribbage.PlayerTurn = cribbage.Player_1;

            cribbage.PlayedCards = playedCards;
            cribbage.CurrentRally.Add(playedCards[0]);
            cribbage.CurrentRally.Add(playedCards[1]);

            cribbage.GoCount = 1;
            cribbage.Player_2.SaidGo = false;

            CribbageGameManager.PlayCard(cribbage, cribbage.PlayerTurn.Hand[0]);
            Assert.AreEqual(cribbage.PlayerTurn, cribbage.Player_1);
            Assert.AreEqual(cribbage.WhatToDo, "go");
            Assert.AreEqual(cribbage.Player_1.Hand.Count, 2);
            Assert.AreEqual(3, cribbage.PlayedCards.Count);
            Assert.AreEqual(cribbage.CurrentRally.Count, 3);
            Assert.AreEqual(cribbage.CurrentCount, 28);
        }

        [TestMethod]
        public void PlayCardNext0CardsI0CardsTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            player1.DisplayName = "p1";
            player2.DisplayName = "p2";

            List<Card> p1cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
            ];

            List<Card> p2cards = new List<Card>();
            //[
            //    cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
            //    cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
            //    cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
            //];

            List<Card> playedCards = new List<Card>();
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Eight).FirstOrDefault());


            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.Player_1.Hand = p1cards;
            cribbage.Player_2.Hand = p2cards;
            cribbage.PlayerTurn = cribbage.Player_1;

            cribbage.PlayedCards = playedCards;
            cribbage.CurrentRally.Add(playedCards[0]);
            cribbage.CurrentRally.Add(playedCards[1]);

            cribbage.GoCount = 1;
            cribbage.Player_2.SaidGo = false;

            CribbageGameManager.PlayCard(cribbage, cribbage.PlayerTurn.Hand[0]);
            Assert.AreEqual(cribbage.PlayerTurn, cribbage.Player_1);
            Assert.AreEqual(cribbage.WhatToDo, "counthands");
            Assert.AreEqual(cribbage.Player_1.Hand.Count, 0);
            Assert.AreEqual(3, cribbage.PlayedCards.Count);
            Assert.AreEqual(0, cribbage.CurrentRally.Count);
            Assert.AreEqual(cribbage.CurrentCount, 0);
        }

        [TestMethod]
        public void IGoNextCanPlayTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            player1.DisplayName = "p1";
            player2.DisplayName = "p2";

            List<Card> p1cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
            ];

            List<Card> p2cards = 
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Two).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
            ] ;

            List<Card> playedCards = new List<Card>();
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Eight).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault());


            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.Player_1.Hand = p1cards;
            cribbage.Player_2.Hand = p2cards;
            cribbage.PlayerTurn = cribbage.Player_1;

            cribbage.PlayedCards = playedCards;
            cribbage.CurrentRally.Add(playedCards[0]);
            cribbage.CurrentRally.Add(playedCards[1]);
            cribbage.CurrentRally.Add(playedCards[2]);

            cribbage.GoCount = 0;
            cribbage.Player_2.SaidGo = false;

            CribbageGameManager.Go(cribbage);
            Assert.AreEqual(cribbage.PlayerTurn, cribbage.Player_2);
            Assert.AreEqual(cribbage.WhatToDo, "playcard");
            Assert.AreEqual(cribbage.Player_1.Hand.Count, 1);
            Assert.AreEqual(3, cribbage.PlayedCards.Count);
            Assert.AreEqual(3, cribbage.CurrentRally.Count);
            Assert.AreEqual(cribbage.CurrentCount, 23);
            Assert.AreEqual(1, cribbage.GoCount);
            Assert.AreEqual(true, cribbage.Player_1.SaidGo);
        }

        [TestMethod]
        public void IGoNextCanGoTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            player1.DisplayName = "p1";
            player2.DisplayName = "p2";

            List<Card> p1cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
            ];

            List<Card> p2cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
            ];

            List<Card> playedCards = new List<Card>();
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Eight).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault());


            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.Player_1.Hand = p1cards;
            cribbage.Player_2.Hand = p2cards;
            cribbage.PlayerTurn = cribbage.Player_1;

            cribbage.PlayedCards = playedCards;
            cribbage.CurrentRally.Add(playedCards[0]);
            cribbage.CurrentRally.Add(playedCards[1]);
            cribbage.CurrentRally.Add(playedCards[2]);

            cribbage.GoCount = 0;
            cribbage.Player_2.SaidGo = false;

            CribbageGameManager.Go(cribbage);
            Assert.AreEqual(cribbage.PlayerTurn, cribbage.Player_2);
            Assert.AreEqual(cribbage.WhatToDo, "go");
            Assert.AreEqual(cribbage.Player_1.Hand.Count, 1);
            Assert.AreEqual(3, cribbage.PlayedCards.Count);
            Assert.AreEqual(3, cribbage.CurrentRally.Count);
            Assert.AreEqual(cribbage.CurrentCount, 23);
            Assert.AreEqual(1, cribbage.GoCount);
            Assert.AreEqual(true, cribbage.Player_1.SaidGo);
        }

        [TestMethod]
        public void IGoNextAlreadySaidGoTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            player1.DisplayName = "p1";
            player2.DisplayName = "p2";

            List<Card> p1cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
            ];

            List<Card> p2cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
            ];

            List<Card> playedCards = new List<Card>();
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Eight).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault());


            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.Player_1.Hand = p1cards;
            cribbage.Player_2.Hand = p2cards;
            cribbage.PlayerTurn = cribbage.Player_1;

            cribbage.PlayedCards = playedCards;
            cribbage.CurrentRally.Add(playedCards[0]);
            cribbage.CurrentRally.Add(playedCards[1]);
            cribbage.CurrentRally.Add(playedCards[2]);

            cribbage.GoCount = 1;
            cribbage.Player_2.SaidGo = true;
            cribbage.LastPlayerPlayed = cribbage.Player_1;

            CribbageGameManager.Go(cribbage);
            Assert.AreEqual(cribbage.PlayerTurn, cribbage.Player_2);
            Assert.AreEqual(cribbage.WhatToDo, "playcard");
            Assert.AreEqual(cribbage.Player_1.Hand.Count, 1);
            Assert.AreEqual(3, cribbage.PlayedCards.Count);
            Assert.AreEqual(0, cribbage.CurrentRally.Count);
            Assert.AreEqual(cribbage.CurrentCount, 0);
            Assert.AreEqual(0, cribbage.GoCount);
            Assert.AreEqual(false, cribbage.Player_1.SaidGo);
            Assert.AreEqual(false, cribbage.Player_2.SaidGo);
        }
        [TestMethod]
        public void IGoNext0CardsTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            player1.DisplayName = "p1";
            player2.DisplayName = "p2";

            List<Card> p1cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
            ];

            List<Card> p2cards = new List<Card>();
            //[
            //    cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.King).FirstOrDefault(),
            //    cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
            //];

            List<Card> playedCards = new List<Card>();
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Eight).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Diamonds && c.face == Faces.Five).FirstOrDefault());


            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.Player_1.Hand = p1cards;
            cribbage.Player_2.Hand = p2cards;
            cribbage.PlayerTurn = cribbage.Player_1;

            cribbage.PlayedCards = playedCards;
            cribbage.CurrentRally.Add(playedCards[0]);
            cribbage.CurrentRally.Add(playedCards[1]);
            cribbage.CurrentRally.Add(playedCards[2]);

            cribbage.GoCount = 1;
            cribbage.Player_2.SaidGo = false;
            cribbage.LastPlayerPlayed = cribbage.Player_2;

            CribbageGameManager.Go(cribbage);
            Assert.AreEqual(cribbage.PlayerTurn, cribbage.Player_1);
            Assert.AreEqual(cribbage.WhatToDo, "playcard");
            Assert.AreEqual(cribbage.Player_1.Hand.Count, 1);
            Assert.AreEqual(3, cribbage.PlayedCards.Count);
            Assert.AreEqual(0, cribbage.CurrentRally.Count);
            Assert.AreEqual(cribbage.CurrentCount, 0);
            Assert.AreEqual(0, cribbage.GoCount);
            Assert.AreEqual(false, cribbage.Player_1.SaidGo);
            Assert.AreEqual(false, cribbage.Player_2.SaidGo);
        }
        [TestMethod]
        public void PlayCardNexAlreadySaidGoI0Cards()
        {
            CribbageGame cribbage = new CribbageGame(true);
            Player player1 = new Player();
            Player player2 = new Player();
            player1.DisplayName = "p1";
            player2.DisplayName = "p2";

            List<Card> p1cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
            ];

            List<Card> p2cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Jack).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
            ];

            List<Card> playedCards = new List<Card>();
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Jack).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Four).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Queen).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Two).FirstOrDefault());
            playedCards.Add(cribbage.Deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Two).FirstOrDefault());


            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.Player_1.Hand.Add(p1cards[0]);
            cribbage.Player_2.Hand.Add(p2cards[0]);
            cribbage.Player_2.Hand.Add(p2cards[1]);
            cribbage.PlayerTurn = cribbage.Player_1;

            cribbage.PlayedCards.Add(playedCards[0]);
            cribbage.PlayedCards.Add(playedCards[1]);
            cribbage.PlayedCards.Add(playedCards[2]);
            cribbage.PlayedCards.Add(playedCards[3]);
            cribbage.PlayedCards.Add(playedCards[4]);

            cribbage.CurrentRally.Add(playedCards[0]);
            cribbage.CurrentRally.Add(playedCards[1]);
            cribbage.CurrentRally.Add(playedCards[2]);
            cribbage.CurrentRally.Add(playedCards[3]);
            cribbage.CurrentRally.Add(playedCards[4]);

            cribbage.GoCount = 1;
            cribbage.Player_2.SaidGo = true;
            cribbage.LastPlayerPlayed = cribbage.Player_1;

            CribbageGameManager.PlayCard(cribbage, cribbage.Player_1.Hand[0]);
            Assert.AreEqual(cribbage.PlayerTurn, cribbage.Player_2);
            Assert.AreEqual(cribbage.WhatToDo, "playcard");
            Assert.AreEqual(cribbage.Player_1.Hand.Count, 0);
            Assert.AreEqual(6, cribbage.PlayedCards.Count);
            Assert.AreEqual(0, cribbage.CurrentRally.Count);
            Assert.AreEqual(cribbage.CurrentCount, 0);
            Assert.AreEqual(0, cribbage.GoCount);
            Assert.AreEqual(false, cribbage.Player_1.SaidGo);
            Assert.AreEqual(false, cribbage.Player_2.SaidGo);
        }

        [TestMethod]
        public void SendToCribTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            player1.DisplayName = "p1";
            player2.DisplayName = "p2";

            List<Card> p1cards =
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Queen).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Two).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Five).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ten).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Eight).FirstOrDefault(),
            ];

            List<Card> p2cards = 
            [
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.King).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Three).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Seven).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Jack).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Nine).FirstOrDefault(),
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Clubs && c.face == Faces.Ten).FirstOrDefault(),
            ];


            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.Player_1.Hand = p1cards;
            cribbage.Player_2.Hand = p2cards;

            List<Card> cardsToSend = CribbageGameManager.Pick_Cards_To_Crib(cribbage.Player_2.Hand);
            CribbageGameManager.Give_To_Crib(cribbage, cardsToSend, cribbage.Player_2);
            Assert.AreEqual(4, cribbage.Player_2.Hand.Count);
            Assert.AreEqual(2, cribbage.Crib.Count);

            List<Card> p1CardsToSend = [cribbage.Player_1.Hand[2], cribbage.Player_1.Hand[4]];
            CribbageGameManager.Give_To_Crib(cribbage, p1CardsToSend, cribbage.Player_1);
            Assert.AreEqual(4, cribbage.Player_1.Hand.Count);
            Assert.AreEqual(4, cribbage.Crib.Count);
        }

        //[TestMethod]
        //public void CardImgPathTest()
        //{
        //    Deck deck = new Deck();

        //    Assert.IsTrue("adaf" == deck.Cards[0].imgPath);
        //}
    }
}