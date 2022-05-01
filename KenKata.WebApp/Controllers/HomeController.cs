
using Microsoft.AspNetCore.Mvc;

namespace KenKata.WebApp.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

       
    }
}