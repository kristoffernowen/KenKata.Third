using KenKata.Shared.Models;
using KenKata.Shared.Models.Entities;
using KenKata.WebApp.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KenKata.WebApp.Controllers
{
    public class CostumerProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public CostumerProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }




        //function that will return alla products if no id is passed in.
        //if category id is passed in return product in the category
        public async Task<IActionResult> GetAll(int id = -0)
        {
            
            if (id == -0)
            {
                var categorys = await _categoryService.GetAll();
                var products = await _productService.GetAll();

                var model = new GetAllViewModel()
                {
                    category = categorys,
                    product = products,
                };

                return View(model);
            }
            else
            {
                var categorys = await _categoryService.GetAll();
                var productByCategory = await _productService.GetProductByCategory(id);
                var model = new GetAllViewModel()
                {
                    category = categorys,
                    product = productByCategory,
                };
                return View(model);
            }
        }

        

       

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.Get(id);

            return View(product);
        }

    }
}
