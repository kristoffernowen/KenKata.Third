using Microsoft.AspNetCore.Mvc;

namespace KenKata.WebApp.Controllers
{
    public class AdminCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
