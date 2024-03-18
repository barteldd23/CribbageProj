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


        [HttpGet]
        public IEnumerable<Game> Get()
        {
            return new GameManager(options).Load();
        }

        [HttpGet("{id}")]
        public Game Get(Guid id)
        {
            return new GameManager(options).LoadById(id);
        }

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
