
using KenKata.WebApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenKata.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SqlContext _sqlContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(SqlContext sqlContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _sqlContext = sqlContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {

            /* This a hardcoded admin, if there is none in the system already. If there already is an admin this section can be commented out, only leave return view(); */


            var adminExists = await _sqlContext.Roles.FirstOrDefaultAsync(x => x.Name == "admin");

            if (adminExists == null)
            {
                await _roleManager.CreateAsync(new IdentityRole("admin"));

                var user = new IdentityUser
                {
                    Email = "admin@admin.se",
                    UserName = "adminTeam"
                };

                var result = await _userManager.CreateAsync(user, "Admin123&");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "admin");
                }
                else
                {
                    return Conflict("Registration of hardcoded admin failed");
                }
            }
            else
            {
                var userAsAdmin = await _sqlContext.UserRoles.FirstOrDefaultAsync(x => x.RoleId == adminExists.Id);

                if (userAsAdmin == null)
                {
                    var user = new IdentityUser
                    {
                        Email = "admin@admin.se",
                        UserName = "adminTeam"
                    };

                    var result = await _userManager.CreateAsync(user, "Admin123&");
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "admin");
                    }
                    else
                    {
                        return Conflict("Registration of hardcoded admin failed");
                    }
                }
            }


            return View();
        }

       
    }
}