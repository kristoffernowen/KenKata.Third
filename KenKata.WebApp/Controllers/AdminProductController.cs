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

        
        [Route("Products")]
        [HttpGet]
        public async Task<IActionResult> Products()
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
        //[HttpPost]
        //public async Task<IActionResult> Create(ProductModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var productExist = await _sqlContext.Products.AnyAsync(x => x.Name == model.Name); //find if a product with model name exist in Db
        //        if (!productExist) //if a product doesen't exist in database
        //        {
        //            var _productEntity = new ProductEntity
        //            {
        //                Name = model.Name,
        //                Description = model.Description,
        //                Price = model.Price,
        //            };
        //            _sqlContext.Products.Add(_productEntity);
        //            await _sqlContext.SaveChangesAsync();
        //            return RedirectToAction("Products", "AdminProduct");
        //        }
        //        else
        //        {
        //            model.ErrorM = "The name cant be the same as existing name";
        //            return View(model);
        //        }
        //    }
        //    else
        //    {
        //        model.ErrorM = "Pleace fill in the form";
        //        return View(model);
        //    }
        //}
        ////[Authorize(Roles = "admin")]
        //[HttpGet]
        //[Route("Edit/{id}")]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var product = await _sqlContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        //    if (product != null)
        //    {
        //        var Model = new ProductModel() { Name = product.Name, Description = product.Description, Price = product.Price };
        //        return View(Model);
        //    }
        //    else { return View(NotFound()); }




        //}
        ////[Authorize(Roles = "admin")]
        ////[ValidateAntiForgeryToken]
        //[Route("edit/{id}")]
        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, ProductModel model)
        //{
        //    var product = await _sqlContext.Products.FirstOrDefaultAsync(x => x.Id == id);

        //    if (product != null)
        //    {
        //        product.Name = model.Name;
        //        product.Description = model.Description;
        //        product.Price = model.Price;
        //        _sqlContext.Update(product);
        //        await _sqlContext.SaveChangesAsync();
        //        return RedirectToAction("Products", "AdminProduct");
        //    }
        //    else
        //        model.ErrorM = "Pleace fill in all fields";
        //    return View(model);
        //}
        ////[Authorize(Roles = "admin")]
        //[HttpGet]
        //public async Task<IActionResult> Details(int id)
        //{
        //    var product = await _sqlContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        //    if (product != null)
        //    {
        //        var Model = new ProductModel() { Id = product.Id, Name = product.Name, Description = product.Description, Price = product.Price };
        //        return View(Model);
        //    }
        //    else

        //        return View(NotFound());
        //}
        ////[Authorize(Roles = "admin")]
        //[HttpGet]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var product = await _sqlContext.Products.FirstOrDefaultAsync(x => x.Id == id);

        //    var Model = new ProductModel();
        //    if (product != null)
        //    {
        //        Model.Id = product.Id;
        //        Model.Name = product.Name;
        //        Model.Description = product.Description;
        //        Model.Price = product.Price;
        //    }
        //    return View(Model);

        //}
        ////[Authorize(Roles = "admin")]
        //[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteConfirmd(int id)
        //{
        //    var p = await _sqlContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        //    if (p != null)
        //    {
        //        _sqlContext.Remove(p);
        //        await _sqlContext.SaveChangesAsync();
        //    }
        //    return RedirectToAction("Products");

        //}
    }
}
