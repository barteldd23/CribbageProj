using Cribbage.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cribbage.API.Test
{
    [TestClass]
    public class utUserGame : utBase
    {


        [TestMethod]
        public async Task LoadTestAsync()
        {
            await base.LoadTestAsync<UserGame>();
        }

        [TestMethod]
        public async Task InsertTestAsync()
        {
            UserGame userGame = new UserGame { GameId = Guid.NewGuid(), PlayerId = Guid.NewGuid(), PlayerScore = 0};
            await base.InsertTestAsync<UserGame>(userGame);

        }

        [TestMethod]
        public async Task LoadByIdTestAsync()
        {
            await base.LoadByIdTestAsync<UserGame>(new KeyValuePair<string, string>("PlayerScore", 0.ToString()));
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            UserGame userGame = new UserGame { GameId = Guid.NewGuid(), PlayerId = Guid.NewGuid(), PlayerScore = 0 };
            await base.UpdateTestAsync<UserGame>(new KeyValuePair<string, string>("PlayerScore", 100.ToString()), userGame);

        }

        [TestMethod]
        public async Task DeleteTestAsync()
        {
            await base.DeleteTestAsync1<UserGame>(new KeyValuePair<string, string>("PlayerScore", 100.ToString()));
        }



    }
}