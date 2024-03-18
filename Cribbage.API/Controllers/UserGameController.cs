using Cribbage.BL;
using Cribbage.BL.Models;
using Cribbage.PL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cribbage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGameController : ControllerBase
    {

        private readonly ILogger<UserGameController> logger;
        private readonly DbContextOptions<CribbageEntities> options;

        public UserGameController(ILogger<UserGameController> logger, DbContextOptions<CribbageEntities> options)
        {
            this.logger = logger;
            this.options = options;
        }


        [HttpGet]
        public IEnumerable<UserGame> Get()
        {
            return new UserGameManager(options).Load();
        }

        [HttpGet("{id}")]
        public UserGame Get(Guid id)
        {
            return new UserGameManager(options).LoadById(id);
        }

        [HttpPost("{gameId}/{userId}/{score}/{rollback?}")]
        public int Post(Guid gameId, Guid userId, int score, bool rollback = false)
        {

            try
            {
                return new UserGameManager(options).Insert(gameId, userId, score, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{id}/{rollback?}")]
        public int Put(Guid id, [FromBody] UserGame userGame, bool rollback = false)
        {
            try
            {
                return new UserGameManager(options).Update(userGame, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id}/{rollback?}")]
        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                return new UserGameManager(options).Delete(id, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
