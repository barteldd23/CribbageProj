namespace Cribbage.PL.Test
{
    [TestClass]
    public class utGame : utBase<tblGame>
    {
        [TestMethod]
        public void LoadTest()
        {
            int expected = 5;
            var games = base.LoadTest();
            Assert.AreEqual(expected, games.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblGame newRow = new tblGame();
            newRow.Id = Guid.NewGuid();
            newRow.GameName = "InsertTest";
            newRow.Winner = dc.tblGames.FirstOrDefault().Winner;
            newRow.Date = DateTime.Now;
            newRow.Complete = true;

            dc.tblGames.Add(newRow);

            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
    }

        [TestMethod]
        public void UpdateTest()
        {
            tblGame row = base.LoadTest().FirstOrDefault();

            if (row != null)
            {
                row.Complete = !row.Complete;
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblGame row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                int rowsAffected = DeleteTest(row);
                Assert.IsTrue(rowsAffected == 1);
            }
        }
    }
}