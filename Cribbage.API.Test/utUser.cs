using Cribbage.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cribbage.API.Test
{
    [TestClass]
    public class utUser : utBase
    {


        [TestMethod]
        public async Task LoadTestAsync()
        {
            await base.LoadTestAsync<User>();
        }

        [TestMethod]
        public async Task InsertTestAsync()
        {
            User user = new User { Email = "utUser@utBase.com", DisplayName = "testing", FirstName = "utUser", LastName = "test", Password = "testing", GamesStarted = 0, GamesWon = 0, GamesLost = 0, WinStreak = 0, AvgPtsPerGame = 0 };
            await base.InsertTestAsync<User>(user);

        }

        [TestMethod]
        public async Task LoadByIdTestAsync()
        {
            await base.LoadByIdTestAsync<User>(new KeyValuePair<string, string>("Email", "cribbage@game.com"));
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            User user = new User { Email = "utUser@utBase.com", DisplayName = "testing", FirstName = "utUser", LastName = "test", Password = "testing", GamesStarted = 0, GamesWon = 0, GamesLost = 0, WinStreak = 0, AvgPtsPerGame = 0 };
            await base.UpdateTestAsync<User>(new KeyValuePair<string, string>("Email", "cribbage@game.com"), user);

        }

        [TestMethod]
        public async Task DeleteTestAsync()
        {
            await base.DeleteTestAsync1<User>(new KeyValuePair<string, string>("Email", "cribbage@game.com"));
        }



    }
}