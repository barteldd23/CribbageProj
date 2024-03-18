using Cribbage.BL;
using Cribbage.BL.Models;
using Cribbage.PL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cribbage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> logger;
        private readonly DbContextOptions<CribbageEntities> options;

        public UserController(ILogger<UserController> logger, DbContextOptions<CribbageEntities> options)
        {
            this.logger = logger;
            this.options = options;
        }


        [HttpGet]
        public IEnumerable<User> Get()
        {
            return new UserManager(options).Load();
        }

        [HttpGet("{id}")]
        public User Get(Guid id)
        {
            return new UserManager(options).LoadById(id);
        }

        [HttpPost("{rollback?}")]
        public int Post([FromBody] User user, bool rollback = false)
        {

            try
            {
                return new UserManager(options).Insert(user, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{id}/{rollback?}")]
        public int Put(Guid id, [FromBody] User user, bool rollback = false)
        {
            try
            {
                return new UserManager(options).Update(user, rollback);
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
                return new UserManager(options).Delete(id, rollback);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
