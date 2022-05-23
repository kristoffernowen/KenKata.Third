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
        

        //function that will return alla products if no id is passed in.
        //if category id is passed in return product in the category
        public async Task<IActionResult> GetAll(int id = -0)
        {
            if (id == -0)
            {
                var products = await _productService.GetAll();

                return View(products);
            }
            else
            {
                var product = await _productService.GetProductByCategory(id);
                return View(product);
            }
                     
        }

    }
}
