using Cribbage.BL;
using Cribbage.BL.Models;
using Cribbage.PL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cribbage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {

        private readonly ILogger<GameController> logger;
        private readonly DbContextOptions<CribbageEntities> options;

        public GameController(ILogger<GameController> logger, DbContextOptions<CribbageEntities> options)
        {
            this.logger = logger;
            this.options = options;
        }

        /// <summary>
        /// Return a list of games.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Game> Get()
        {
            return new GameManager(options).Load();
        }

        /// <summary>
        /// Get a particular game by id.
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Game Get(Guid id)
        {
            return new GameManager(options).LoadById(id);
        }

        /// <summary>
        /// Insert a game.
        /// </summary>
        /// <param name="game">Game Id</param>
        /// <param name="rollback">Should we rollback the insert?</param>
        /// <returns>New Guid</returns>
        [HttpPost("{rollback?}")]
        public int Post([FromBody] Game game, bool rollback = false)
        {

            try
            {
                return new GameManager(options).Insert(game, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Update a game.
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <param name="game"></param>
        /// <param name="rollback">Should we rollback the update?</param>
        /// <returns></returns>
        [HttpPut("{id}/{rollback?}")]
        public int Put(Guid id, [FromBody] Game game, bool rollback = false)
        {
            try
            {
                return new GameManager(options).Update(game, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Delete a game.
        /// </summary>
        /// <param name="id">Game Id</param>
        /// <param name="rollback">Should we rollback the delete?</param>
        /// <returns></returns>
        [HttpDelete("{id}/{rollback?}")]
        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                return new GameManager(options).Delete(id, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
