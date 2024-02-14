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
            List<Card> hand =
            [
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Three).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Nine).FirstOrDefault(),
                deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault(),
            ];

            int points = cribbage.CountPairs(hand);
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
            points = cribbage.CountPairs(hand);
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
            points = cribbage.CountPairs(hand);
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
            points = cribbage.CountPairs(hand);
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
            points = cribbage.CountPairs(hand);
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
            points = cribbage.CountPairs(hand);
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

            int points = cribbage.Count15s(hand);
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
            points = cribbage.Count15s(hand);
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
            points = cribbage.Count15s(hand);
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

            int points = cribbage.CountRuns(hand);
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
            points = cribbage.CountRuns(hand);
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
            points = cribbage.CountRuns(hand);
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
            points = cribbage.CountRuns(hand);
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
            points = cribbage.CountRuns(hand);
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
            points = cribbage.CountRuns(hand);
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
            points = cribbage.CountRuns(hand);
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
            points = cribbage.CountRuns(hand);
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

            int points = cribbage.CountFlush(hand);
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
            points = cribbage.CountFlush(hand);
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
            points = cribbage.CountFlush(hand);
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
            points = cribbage.CountFlush(hand, true);
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
            points = cribbage.CountFlush(hand, true);
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

            int points = cribbage.CountKnobs(hand);
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
            points = cribbage.CountKnobs(hand);
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
            points = cribbage.CountKnobs(hand);
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

            int points = cribbage.CountHand(hand);
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
            points = cribbage.CountHand(hand);
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
            points = cribbage.CountHand(hand);
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
            points = cribbage.CountHand(hand);
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
            points = cribbage.CountHand(hand);
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
            points = cribbage.CountHand(hand);
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
            cribbage.ShuffleDeck();
            cribbage.Deal();

            Assert.AreEqual(6, cribbage.Player_1.Hand.Count);
            Assert.AreEqual(6, cribbage.Player_2.Hand.Count);
            Assert.AreEqual(40, cribbage.Deck.Cards.Count);

            cribbage.Cut();
            Assert.IsTrue(cribbage.CutCard != null);

            List<Card> cards = [cribbage.Player_1.Hand[5], cribbage.Player_1.Hand[4]];
            cribbage.Give_To_Crib(cards);
            cards = null;
            cards = [cribbage.Player_2.Hand[5], cribbage.Player_2.Hand[4]];
            cribbage.Give_To_Crib(cards);

            Assert.AreEqual(4, cribbage.Crib.Count);
        }

        [TestMethod]
        public void playCardTest()
        {
            CribbageGame cribbage = new CribbageGame();
            Player player1 = new Player();
            Player player2 = new Player();
            cribbage.Player_1 = player1;
            cribbage.Player_2 = player2;
            cribbage.PlayerTurn = cribbage.Player_1;
            cribbage.ShuffleDeck();
            cribbage.Deal();

            List<Card> cards = [cribbage.Player_1.Hand[5], cribbage.Player_1.Hand[4]];
            cribbage.Give_To_Crib(cards);
            cards = null;
            cards = [cribbage.Player_2.Hand[5], cribbage.Player_2.Hand[4]];
            cribbage.Give_To_Crib(cards);

            cribbage.PlayCard(cribbage.PlayerTurn.Hand[0]);
            Assert.IsTrue(cribbage.PlayedCards.Count == 1);
            Assert.IsTrue(cribbage.PlayerTurn == cribbage.Player_2);
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

            cribbage.PlayCard(card);
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


            cribbage.PlayCard(card);

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

            cribbage.PlayCard(card);

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
            cribbage.PlayedCards = cards;

            Card card = new Card();
            card = cribbage.Deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Ace).FirstOrDefault();

            cribbage.PlayCard(card);

            Assert.AreEqual(0, cribbage.PlayedCards.Count);
            Assert.AreEqual(2, cribbage.Player_1.Score);
            Assert.AreEqual(0, cribbage.CurrentCount);
            Assert.AreEqual(cribbage.Player_2, cribbage.PlayerTurn);
        }

        [TestMethod]
        public void playCardTest6()
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
                cribbage.Deck.Cards.Where(c => c.suit == Suits.Hearts && c.face == Faces.Seven).FirstOrDefault(),
            ];
            cribbage.PlayedCards = cards;

            Card card = new Card();
            card = cribbage.Deck.Cards.Where(c => c.suit == Suits.Spades && c.face == Faces.Six).FirstOrDefault();

            cribbage.PlayCard(card);

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

            cribbage.PlayCard(card);

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

            cribbage.PlayCard(card);

            cribbage.Go();
            cribbage.Go();
            Assert.AreEqual(0, cribbage.PlayedCards.Count);
            Assert.AreEqual(1, cribbage.Player_1.Score);
            Assert.AreEqual(0, cribbage.CurrentCount);
            Assert.AreEqual(cribbage.Player_2, cribbage.PlayerTurn);
        }
    }
}