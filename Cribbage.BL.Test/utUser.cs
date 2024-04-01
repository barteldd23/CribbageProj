using iText.Forms.Xfdf;
using iText.StyledXmlParser.Jsoup.Nodes;
using Reporting;

namespace Cribbage.BL.Test
{
    [TestClass]
    public class utUser : utBase
    {
        [TestMethod]
        public void ReportTest()
        {
            var users = new UserManager(options).Load();
            string[] columns = { "DisplayName", "GamesStarted", "GamesWon", "GamesLost", "WinStreak", "AvgPtsPerGame" };
            var data = UserManager.ConvertData<User>(users, columns);
            Excel.Export("userStats.xlsx", data);
        }

        [TestMethod]
        public void LoadTest()
        {
            List<User> users = new UserManager(options).Load();
            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        public void LoadSPTest()
        {
            var users = new UserManager(options).LoadSPGetMostWins();
            int expected = 3;
            Assert.AreEqual(expected, users.Count);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            User user = new UserManager(options).Load().FirstOrDefault();
            Assert.AreEqual(new UserManager(options).LoadById(user.Id).Id, user.Id);
        }

        [TestMethod]
        public void LoadByEmailTest()
        {
            User user = new UserManager(options).Load().FirstOrDefault();
            Assert.AreEqual(new UserManager(options).LoadByEmail(user.Email).Id, user.Id);
        }

        [TestMethod]
        public void InsertTest()
        {
            User user = new User { Id = Guid.NewGuid(), Email = "Insert@Test.com", DisplayName = "TestName", FirstName = "Bob", LastName = "Bob", Password = "test", GamesStarted = 0, GamesWon = 0, GamesLost = 0, WinStreak = 0, AvgPtsPerGame = 0 };
            int result = new UserManager(options).Insert(user, true);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            User user = new UserManager(options).Load().FirstOrDefault();
            user.Password = "updatetesttesting";

            Assert.IsTrue(new UserManager(options).Update(user, true) > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            User user = new UserManager(options).Load().FirstOrDefault();
            Assert.IsTrue(new UserManager(options).Delete(user.Id, true) > 0);
        }

        [TestMethod]
        public void LoginSuccess()
        {
            User user = new User { FirstName = "Joe", LastName = "Smith", Email = "cribbage@game.com", Password = "maple" };
            bool result = new UserManager(options).Login(user);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LoginFail ()
        {
            try
            {
                User user = new User { FirstName = "Joe", LastName = "Smith", Email = "cribbage@game.com", Password = "mapled" };
                bool result = new UserManager(options).Login(user);
                Assert.Fail();
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}