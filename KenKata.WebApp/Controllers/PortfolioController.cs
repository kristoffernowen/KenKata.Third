using Microsoft.AspNetCore.Mvc;

namespace KenKata.WebApp.Controllers
{
    public class PortfolioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WomensFashion()
        {
            return View();
        }
    }


}
