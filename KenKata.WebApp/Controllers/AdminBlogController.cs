using Microsoft.AspNetCore.Mvc;

namespace KenKata.WebApp.Controllers
{
    public class AdminBlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
