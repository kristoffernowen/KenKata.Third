using Microsoft.AspNetCore.Mvc;

namespace KenKata.WebApp.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Post()
        {
            return View();
        }
    }
}
