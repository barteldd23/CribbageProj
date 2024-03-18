using Cribbage.PL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cribbage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly ILogger<UserController> logger;
        private readonly DbContextOptions<CribbageEntities> options;

        public UserController( ILogger<UserController> logger, DbContextOptions<CribbageEntities> options)
        {
            this.logger = logger;
            this.options = options;
        }



        public IActionResult Index()
        {
            return View();
        }
    }
}
