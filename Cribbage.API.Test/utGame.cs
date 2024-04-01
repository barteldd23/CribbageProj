using Cribbage.BL.Models;
using Microsoft.SqlServer.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cribbage.API.Test
{
    [TestClass]
    public class utGame : utBase
    {

        [TestMethod]
        public async Task LoadTestAsync()
        {
            await base.LoadTestAsync<Game>();
        }

        [TestMethod]
        public async Task InsertTestAsync()
        {
            Game game = new Game { Winner = Guid.NewGuid(), Date = DateTime.Now, GameName = "utGame test", Complete = false };
            await base.InsertTestAsync<Game>(game);

        }

        [TestMethod]
        public async Task LoadByIdTestAsync()
        {
            await base.LoadByIdTestAsync<Game>(new KeyValuePair<string, string>("GameName", "Test2"));
        }

        [TestMethod]
        public async Task UpdateTestAsync()
        {
            Game game = new Game { Winner = Guid.NewGuid(), Date = DateTime.Now, GameName = "Other", Complete = false };
            await base.UpdateTestAsync<Game>(new KeyValuePair<string, string>("GameName", "Test5"), game);

        }

        [TestMethod]
        public async Task DeleteTestAsync()
        {
            await base.DeleteTestAsync1<Game>(new KeyValuePair<string, string>("GameName", "Test1"));
        }
    }

}