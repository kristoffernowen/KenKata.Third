using Microsoft.AspNetCore.Mvc;

namespace KenKata.WebApp.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RegisterTeamMember()
        {

            /* Reg user
             *
             * assign role
             *
             * reg profile attach to user
             *
             *
             */



            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterTeamMember<T>(T model)
        {

            /* Reg user
             *
             * assign role
             *
             * reg profile attach to user
             *
             *
             */



            return View();
        }
    }
}
