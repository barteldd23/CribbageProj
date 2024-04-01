using Cribbage.BL;
using Cribbage.BL.Models;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog.Ui.Web;

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
            //UserGame userGame = new UserGameManager(options).Load().FirstOrDefault();
            UserGame userGame = new UserGame { Id = Guid.NewGuid(), GameId = Guid.NewGuid(), PlayerId = Guid.NewGuid(), PlayerScore = 0 };
            await base.LoadByIdTestAsync<UserGame>(new KeyValuePair<string, string>("Id", userGame.Id.ToString()));
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            UserGame userGame = new UserGame { Id = Guid.NewGuid(), GameId = Guid.NewGuid(), PlayerId = Guid.NewGuid(), PlayerScore = 0 };
            await base.UpdateTestAsync<UserGame>(new KeyValuePair<string, string>("Id", userGame.Id.ToString()), userGame);

        }

        [TestMethod]
        public async Task DeleteTestAsync()
        {

            await base.DeleteTestAsync1<UserGame>(new KeyValuePair<string, string>("Id", 100.ToString()));
        }



    }
}