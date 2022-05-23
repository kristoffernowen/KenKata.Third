using Microsoft.AspNetCore.Mvc;

namespace KenKata.WebApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
