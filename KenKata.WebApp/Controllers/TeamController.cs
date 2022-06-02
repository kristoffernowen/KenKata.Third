using Microsoft.AspNetCore.Mvc;

namespace KenKata.WebApp.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
