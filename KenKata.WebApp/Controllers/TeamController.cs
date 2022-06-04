using KenKata.Shared.Models;
using KenKata.Shared.Models.Entities;
using KenKata.WebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KenKata.WebApp.Controllers
{
    public class TeamController : Controller
    {
        private readonly SqlContext _sqlContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TeamController(SqlContext sqlContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment webHostEnvironment)
        {
            _sqlContext = sqlContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
        }



        public async Task<IActionResult> Index()
        {
            var teamProfiles = await _sqlContext.TeamMemberProfiles.ToListAsync();
            if (teamProfiles.Count > 8)
            {
                var profilesForView = teamProfiles.GetRange(0, 8);
                return View(profilesForView);
            }

            return View(teamProfiles);
        }



        [Authorize(Roles = "admin")]
        public IActionResult RegisterTeamMember()
        {
            return View();
        }



        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterTeamMember(RegisterTeamMemberViewModel model)
        {
            var teamMemberExists = await _sqlContext.Roles.FirstOrDefaultAsync(x => x.Name == "teamMember");

            if (teamMemberExists == null)
            {
                await _roleManager.CreateAsync(new IdentityRole("teamMember"));
            }

            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    Email = model.RegisterUserModel.Email,
                    UserName = model.RegisterUserModel.UserName
                };
                var result = await _userManager.CreateAsync(user, model.RegisterUserModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "teamMember");

                    var teamMemberProfile = new TeamMemberProfileEntity
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Title = model.Title,
                        UserId = user.Id
                    };

                    _sqlContext.TeamMemberProfiles.Add(teamMemberProfile);
                    await _sqlContext.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                return Conflict("Registration failed");
            }

            return View(model);
        }



        [Authorize(Roles = "teamMember")]
        public IActionResult AddProfilePhoto()
        {
            return View();
        }



        [HttpPost]
        [Authorize(Roles = "teamMember")]
        public async Task<IActionResult> AddProfilePhoto(PhotoUpload form)
        {
            var user = await _sqlContext.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity!.Name);


            if (!ModelState.IsValid)
                return View();

            var wwwrootPath = _webHostEnvironment.WebRootPath;

            var profile = await _sqlContext.TeamMemberProfiles.FirstOrDefaultAsync(x => x.UserId == user!.Id);



            profile!.ProfilePhotoFileName = $"{user!.Id}_{form.File.FileName}";

            var filePath = Path.Combine($"{wwwrootPath}/profilePhoto", profile.ProfilePhotoFileName);

            await using (var fs = new FileStream(filePath, FileMode.Create))
            {
                await form.File.CopyToAsync(fs);
            }

            _sqlContext.TeamMemberProfiles.Update(profile);
            await _sqlContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
