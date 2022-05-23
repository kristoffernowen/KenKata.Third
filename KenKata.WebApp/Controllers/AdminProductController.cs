using KenKata.Shared.Models;
using KenKata.Shared.Models.Entities;
using KenKata.WebApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace KenKata.WebApp.Controllers
{
    public class AdminProductController : Controller
    {
        
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;


        public AdminProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }



        //[Authorize(Roles = "admin")]
        [Route("admin/products")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var _products = await _productService.GetAll();
            

            return View(_products);
        }


        //[Authorize(Roles = "admin")]
        [Route("admin/Product/Create")]
        [HttpGet]
        public async Task <IActionResult> Create()
        {

            var model = new ProductModelForm();

            var list = new List<CategoryEntity>();
            var categoryList = await _categoryService.GetAll();

            foreach (var category in categoryList)
            {
                list.Add(category);
            }
            model.categoryList = list;
            
                
            return View(model);
        }

        ////[Authorize(Roles = "admin")]
        [Route("admin/Product/Create")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductModelForm model,int CategorySelected)
        {
            if (ModelState.IsValid)
            {
                var productResult = await _productService.Create(model, CategorySelected);

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
            var list = new List<CategoryEntity>();
            var categoryList = await _categoryService.GetAll();

            foreach (var category in categoryList)
            {
                list.Add(category);
            }

            var product = await _productService.Get(id);

            var Model = new ProductModelForm()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Color = product.Color,
                ImgUrl = product.ImgUrl,
                categoryList=list
                
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

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.Get(id);
            return View(product);

        }
        //[Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmd(int id)
        {
            var result = await _productService.Delete(id);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            return View(NotFound());
            

        }
    }
}
