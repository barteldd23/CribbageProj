namespace Cribbage.BL.Test
{
    [TestClass]
    public class utGame : utBase
    {
        [TestMethod]
        public void LoadTest()
        {
            List<Game> games = new GameManager(options).Load();
            int expected = 5;

            Assert.AreEqual(expected, games.Count);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            Game game = new GameManager(options).Load().FirstOrDefault();
            Assert.AreEqual(new GameManager(options).LoadById(game.Id).Id, game.Id);
        }

        [TestMethod]
        public void InsertTest()
        {
            Game game = new Game
            {
                Id = Guid.NewGuid(),
                Winner = new UserManager(options).Load().FirstOrDefault().Id,
                Date = DateTime.Now,
                GameName = "Insert Test",
                Complete = false
            };

            int result = new GameManager(options).Insert(game, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Game game = new GameManager(options).Load().FirstOrDefault();
            game.Complete = !game.Complete;

            Assert.IsTrue(new GameManager(options).Update(game, true) > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Game game = new GameManager(options).Load().FirstOrDefault();

            Assert.IsTrue(new GameManager(options).Delete(game.Id, true) > 0);
        }
    }