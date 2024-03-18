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

        /// <summary>
        /// Return a list of user games.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<UserGame> Get()
        {
            return new UserGameManager(options).Load();
        }

        /// <summary>
        /// Get a particular user game by id.
        /// </summary>
        /// <param name="id">UserGame Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public UserGame Get(Guid id)
        {
            return new UserGameManager(options).LoadById(id);
        }

        /// <summary>
        /// Insert a user game.
        /// </summary>
        /// <param name="userGame"></param>
        /// <param name="rollback">Should we rollback the insert?</param>
        /// <returns>New Guid</returns>
        [HttpPost("{rollback?}")]
        public int Post([FromBody] UserGame userGame, bool rollback = false)
        {

            try
            {
                return new UserGameManager(options).Insert(userGame, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Update a user game.
        /// </summary>
        /// <param name="id">UserGame Id</param>
        /// <param name="userGame"></param>
        /// <param name="rollback">Should we rollback the update?</param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete a user game.
        /// </summary>
        /// <param name="id">UserGame Id</param>
        /// <param name="rollback">Should we rollback the delete?</param>
        /// <returns></returns>
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
