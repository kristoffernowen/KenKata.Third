using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KenKata.WebApp.Controllers;
using KenKata.WebApp.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace KenKata.WebApp.Tests
{
    public class TeamController_UnitTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDatabaseFixture _fixture;

        public TeamController_UnitTests(TestDatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task TeamController_Index_Should_Return_ViewResult_With_A_List_Of_TeamMemberProfileEntities()
        {
            // Arrange

            await using var context = _fixture.CreateContext();

            var store = new Mock<IUserStore<IdentityUser>>();
            store.Setup(x =>
                    x.CreateAsync(
                        new IdentityUser
                            {UserName = "kalle@kalle.se", Email = "kalle@kalle.se", PasswordHash = "Kalle"},
                        CancellationToken.None))
                .ReturnsAsync(IdentityResult.Success);
            var userManager =
                new UserManager<IdentityUser>(store.Object, null, null, null, null, null, null, null, null);


            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            roleStore.Setup(x => x.CreateAsync(new IdentityRole("testRole"), CancellationToken.None))
                .ReturnsAsync(IdentityResult.Success);
            var roleManager = new RoleManager<IdentityRole>(roleStore.Object, null, null, null, null);

            var mockIWeb = new Mock<IWebHostEnvironment>();

            var teamService = new Mock<ITeamService>();
            teamService.Setup(x => x.GetAllAsync()).ReturnsAsync(TeamMemberFixture.GetAllTeamMembers());

            var sut = new TeamController(context, userManager, roleManager, mockIWeb.Object, teamService.Object);

            // Action

            var result = await sut.Index();

            // Assert

            var isViewResult = Assert.IsType<ViewResult>(result);
        }
    }
}
