namespace Cribbage.BL.Test
{
    [TestClass]
    public class utUserGame : utBase
    {
        [TestMethod]
        public void InsertTest()
        {
            UserGame userGame = new UserGameManager(options).Load().FirstOrDefault();
            int results = new UserGameManager(options).Insert(userGame, true);
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
            Guid playerId = new UserManager(options).Load().FirstOrDefault().Id;
            Assert.AreEqual(2, new UserGameManager(options).GetGames(playerId).Count());
        }

        [TestMethod]
        public void GetGamesVsComputer()
        {
            //Guid playerId = new UserManager(options).Load().FirstOrDefault().Id;

            Guid playerId = new Guid("e3ba195c-68e8-44ae-8c35-1175c3803ba6");
            Assert.IsTrue(new UserGameManager(options).GetGamesVsComputer(playerId).Count() > 0);
        }

        [TestMethod]
        public void UpdateCribbageGameTest()
        {

        }
    }
}