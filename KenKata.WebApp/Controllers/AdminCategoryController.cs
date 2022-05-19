using KenKata.Shared.Models;
using KenKata.Shared.Models.Entities;
using KenKata.WebApp.Data;
using KenKata.WebApp.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenKata.WebApp.Controllers
{
    public class AdminCategoryController : Controller
    {

        private readonly ICategoryService _categoryService;

        public AdminCategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        //[Authorize(Roles = "admin")]
        [Route("admin/Category")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categorys = await _categoryService.GetAll();

            return View(categorys);
        }
        //[Authorize(Roles = "admin")]
        [Route("admin/Category/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CategoryModelForm();
            return View(model);
        }

        ////[Authorize(Roles = "admin")]
        [Route("admin/Category/Create")]
        [HttpPost]
        public async Task<IActionResult> Create(CategoryModelForm model)
        {
            if (ModelState.IsValid)
            {
                var categoryResult = await _categoryService.Create(model);
                if (categoryResult.Success)
                {
                    return RedirectToAction("Index", "AdminCategory");
                }
                else
                    model.ErrorM = "The name cant be the same as existing name";
                return View(model); 

            }else
            {
                model.ErrorM = "Pleace fill in the form";
                return View(model);
            }
        }
        ////[Authorize(Roles = "admin")]
        [HttpGet]
        [Route("category/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.Get(id);

            var Model = new CategoryModelForm()
            {
                Id = category.Id,
                Name = category.Name,


            };
            return View(Model);


        }
        ////[Authorize(Roles = "admin")]
        ////[ValidateAntiForgeryToken]
        [Route("category/edit/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryModelForm model)
        {
            var result = await _categoryService.Update(id, model);

            if (result.Success)
            {
                return RedirectToAction("Index", "AdminCategory");
            }
            else
                model.ErrorM = "Pleace fill in all fields";
            return View(model);
        }
        ////[Authorize(Roles = "admin")]
        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.Get(id);
            return View(category);

        }
        //[Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmd(int id)
        {
            var result = await _categoryService.Delete(id);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }
            return View(NotFound());


        }
    }
}
