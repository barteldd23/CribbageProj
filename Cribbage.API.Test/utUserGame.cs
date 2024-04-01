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
            await base.LoadByIdTestAsync<UserGame>(new KeyValuePair<string, string>("Id", "554f727e-0a21-4236-9092-014dc1bd5ebb"));
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            UserGame userGame = new UserGame { GameId = new Guid("20112213-9177-4a42-9543-15a22b66ba26"), PlayerId = new Guid("a988fc08-9f27-46d0-a4fa-89769251fde3"), PlayerScore = 0 };
            await base.UpdateTestAsync<UserGame>(new KeyValuePair<string, string>("Id", "a327a126-4fb0-4326-a70c-e527082cf2c3"), userGame);

        }

        [TestMethod]
        public async Task DeleteTestAsync()
        {
            await base.DeleteTestAsync1<UserGame>(new KeyValuePair<string, string>("Id", "b20d5802-93c1-4560-946e-85678b076f01"));
        }



    }
}