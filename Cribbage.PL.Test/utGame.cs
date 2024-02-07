using System;

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
            newRow.Player_1_Id = dc.tblGames.FirstOrDefault().Player_1_Id;
            newRow.Player_2_Id = dc.tblGames.FirstOrDefault().Player_2_Id;
            newRow.Player_1_Score = 121;
            newRow.Player_2_Score = 20;
            newRow.Winner = newRow.Player_1_Id;
            newRow.Date = DateTime.Now;

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
                row.Player_1_Score = 0;
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