using KenKata.Shared.Models;
using KenKata.WebApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace KenKata.WebApp.Controllers
{
    public class AdminProductController : Controller
    {
        
        private readonly IProductService _productService;

        public AdminProductController(IProductService productService)
        {
            _productService = productService;
        }



        //[Authorize(Roles = "admin")]
        //[Route("Products")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var _products = await _productService.GetAll();

            return View(_products);
        }
        //[Authorize(Roles = "admin")]
        [Route("Create")]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new ProductModelForm();
            return View(model);
        }

        ////[Authorize(Roles = "admin")]
        //[Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductModelForm model)
        {
            if (ModelState.IsValid)
            {
                var productResult = await _productService.Create(model);

                if (productResult.Success)
                {
                    return RedirectToAction("Index", "AdminProduct");
                }
                else
                {
                    model.ErrorM = "The name cant be the same as existing name";
                    return View(model);
                }
            }
            else
            {
                model.ErrorM = "Pleace fill in the form";
                return View(model);
            }
        }
        ////[Authorize(Roles = "admin")]
        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.Get(id);

            var Model = new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
            return View(Model);


        }
        ////[Authorize(Roles = "admin")]
        ////[ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductModelForm model)
        {
            var result = await _productService.Update(id, model);

            if (result.Success)
            {
                return RedirectToAction("Index", "AdminProduct");
            }
            else
                model.ErrorM = "Pleace fill in all fields";
            return View(model);
        }
        ////[Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.Get(id);
            if (product != null)
            {
                return View(product);
            }
            else
                return View(NotFound());
        }
    }
}
