
using Microsoft.AspNetCore.Mvc;

namespace KenKata.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewData["MyDemo"] = _configuration["TestValue"];
            ViewData["MySql"] = _configuration["Sql"];

            return View();
        }

       
    }
}