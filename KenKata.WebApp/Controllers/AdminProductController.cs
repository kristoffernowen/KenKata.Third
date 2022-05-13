using Microsoft.AspNetCore.Mvc;

namespace KenKata.WebApp.Controllers
{
    public class AdminProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
