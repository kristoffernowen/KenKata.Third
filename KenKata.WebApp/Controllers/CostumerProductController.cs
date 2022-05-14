using KenKata.Shared.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KenKata.WebApp.Controllers
{
    public class CostumerProductController : Controller
    {
        public async Task<IActionResult> GetAll()
        {
            
            return View();
        }

        public async Task<IActionResult> Details()
        {

            return View();
        }
    }
}
