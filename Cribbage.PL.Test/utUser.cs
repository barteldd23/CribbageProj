using System;

namespace Cribbage.PL.Test
{
    [TestClass]
    public class utUser : utBase<tblUser>
    {
        [TestMethod]
        public void LoadTest()
        {
            int expected = 5;
            var users = base.LoadTest();
            Assert.AreEqual(expected, users.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            tblUser newRow = new tblUser();
            newRow.Id = Guid.NewGuid();
            newRow.Email = "test@test.com";
            newRow.Password = "password";
            newRow.FirstName = "TestFirstName";
            newRow.LastName = "TestLastName";
            newRow.DisplayName = "TestingCribbage";
            newRow.GamesStarted = 0;
            newRow.GamesWon = 0;
            newRow.GamesLost = 0;
            newRow.AvgPtsPerGame = 0;
            newRow.WinStreak = 0;

            dc.tblUsers.Add(newRow);

            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblUser row = base.LoadTest().FirstOrDefault();

            if (row != null)
            {
                row.Email = "test@email.org";
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblUser row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                int rowsAffected = DeleteTest(row);
                Assert.IsTrue(rowsAffected == 1);
            }
        }
    }
}