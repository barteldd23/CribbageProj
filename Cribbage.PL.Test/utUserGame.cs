using Castle.Components.DictionaryAdapter.Xml;
using System;

namespace Cribbage.PL.Test
{
    [TestClass]
    public class utUserGame : utBase<tblUserGame>
    {
        [TestMethod]
        public void LoadTest()
        {
            int expected = 10;
            var games = base.LoadTest();
            Assert.AreEqual(expected, games.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblUserGame newRow = new tblUserGame();
            newRow.Id = Guid.NewGuid();
            newRow.PlayerId = dc.tblUsers.FirstOrDefault().Id;
            newRow.GameId = dc.tblGames.FirstOrDefault().Id;
            newRow.PlayerScore = 15;

            dc.tblUserGames.Add(newRow);

            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblUserGame row = base.LoadTest().FirstOrDefault();

            if (row != null)
            {
                row.PlayerScore = 0;
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblUserGame row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                int rowsAffected = DeleteTest(row);
                Assert.IsTrue(rowsAffected == 1);
            }
        }
    }
}