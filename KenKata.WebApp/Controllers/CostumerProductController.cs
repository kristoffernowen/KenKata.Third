using KenKata.Shared.Models.Entities;
using KenKata.WebApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace KenKata.WebApp.Controllers
{
    public class CostumerProductController : Controller
    {
        private readonly IProductService _productService;

        public CostumerProductController(IProductService productService)
        {
            _productService = productService;
        }        
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            
            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.Get(id);

            return View(product);
        }
    }
}
