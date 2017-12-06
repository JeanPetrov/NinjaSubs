namespace NinjaSubs.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NinjaSubs.Data.Models;
    using NinjaSubs.Services.Admin;
    using NinjaSubs.Web.Areas.Admin.Models.Users;
    using NinjaSubs.Web.Infrastructure.Extensions;
    using System.Threading.Tasks;

    public class UsersController : BaseAdminController
    {
        private readonly IAdminUserService adminUserService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(IAdminUserService adminUserService, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.adminUserService = adminUserService;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.adminUserService.AllAsync();


            return View(new UserListingsViewModel
            {
                UsersWithRoles = users
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(RemoveOrAddUserToRoleFormModel model)
        {
            if (model.Role == null)
            {
                TempData.AddErrorMessage("You need to select some role!");
                return RedirectToAction(nameof(Index));
            }

            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            await this.userManager.AddToRoleAsync(user, model.Role);

            TempData.AddSuccessMessage($"User {user.UserName} successfully added to the {model.Role} role.");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromRole(RemoveOrAddUserToRoleFormModel model)
        {
            if (model.Role == null)
            {
                TempData.AddErrorMessage("You need to select some role!");
                return RedirectToAction(nameof(Index));
            }

            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            await this.userManager.RemoveFromRoleAsync(user, model.Role);

            TempData.AddSuccessMessage($"User {user.UserName} successfully removed from the {model.Role} role.");

            return RedirectToAction(nameof(Index));
        }
    }
}
