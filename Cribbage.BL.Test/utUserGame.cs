namespace Cribbage.BL.Test
{
    [TestClass]
    public class utUserGame : utBase
    {
        [TestMethod]
        public void InsertTest()
        {
            Guid gameId = new GameManager(options).Load().FirstOrDefault().Id;
            Guid playerId = new UserManager(options).Load().FirstOrDefault().Id;
            int playerScore = 50;
            int results = new UserGameManager(options).Insert(gameId, playerId, playerScore, true);
            Assert.IsTrue(results > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            UserGame userGame = new UserGameManager(options).Load().FirstOrDefault();
            userGame.PlayerScore = -10;
            int results = new UserGameManager(options).Update(userGame, true);
            Assert.IsTrue(results > 0);

        }

        [TestMethod]
        public void LoadByIdTest()
        {
            UserGame userGame = new UserGameManager(options).Load().FirstOrDefault();

            Assert.AreEqual(new UserGameManager(options).LoadById(userGame.Id).Id, userGame.Id);
        }

        [TestMethod]
        public void LoadTest()
        {
            List<UserGame> userGames = new UserGameManager(options).Load();
            int expected = 10;

            Assert.AreEqual(expected, userGames.Count); 
        }

        [TestMethod]
        public void DeleteTest()
        {
            UserGame userGame = new UserGameManager(options).Load().FirstOrDefault();

            Assert.IsTrue(new UserGameManager(options).Delete(userGame.Id, true) > 0);
        }

        [TestMethod]
        public void GetGamesTest()
        {
            Guid playerId = new UserManager(options).Load().LastOrDefault().Id;
            Assert.AreEqual(2, new UserGameManager(options).GetGames(playerId).Count());
        }
    }
}