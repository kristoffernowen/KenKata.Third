using KenKata.Shared.Models;
using KenKata.WebApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenKata.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly SqlContext _sqlContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(SqlContext sqlContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _sqlContext = sqlContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RegisterUser()
        {
            var model = new RegisterUserModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserModel model)
        {
            var roles = _sqlContext.Roles.Any();

            if (roles == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("admin"));
                await _roleManager.CreateAsync(new IdentityRole("customer"));
            }


            if (ModelState.IsValid)
            {
                    var user = new IdentityUser
                    {
                        Email = model.Email,
                        UserName = model.UserName
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "customer");

                        return RedirectToAction("Index");
                    }

                    return Conflict("Registration failed");

            }

            return RedirectToAction("Index");
        }
    }
}
