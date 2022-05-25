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


        public async Task<IActionResult> GetAll(int id = -0)
        {
            var categorys = await _categoryService.GetAll();
            var colorList = await _productService.GetAllColors();
            if (id == -0)
            {
                 var products = await _productService.GetAll();
                    var model = new GetAllViewModel()
                    {
                        category = categorys,
                        product = products,
                        colors = colorList
                    };
  
                return View("GetAll", model);                  
            }else
            {
                var productByCategory = await _productService.GetProductByCategory(id);
                var model = new GetAllViewModel()
                {
                    category = categorys,
                    product = productByCategory,
                    colors = colorList,
                    SelectedCategoryId = id,
                    SelectedColor = ""
                };
                return View("GetAll", model);
            }
        }

        public async Task<IActionResult> GetAllColor(string value)
        {
            var colorList = await _productService.GetAllColors();
            var categorys = await _categoryService.GetAll();

            var productByColor = await _productService.GetProdúctByColor(value);
            var model = new GetAllViewModel()
            {
                category = categorys,
                product = productByColor,
                colors = colorList,
                SelectedColor = value,
                SelectedCategoryId = -0
            };
            return View("GetAll",model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.Get(id);

            return View(product);
        }

    }
}
