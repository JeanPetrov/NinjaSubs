namespace NinjaSubs.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using NinjaSubs.Data.Models;
    using NinjaSubs.Data.Models.Enums;
    using NinjaSubs.Services;
    using NinjaSubs.Services.Admin;
    using NinjaSubs.Web.Areas.Admin.Models.Users;
    using NinjaSubs.Web.Infrastructure.Extensions;
    using System;
    using System.Threading.Tasks;

    using static Services.ServiceConstants;

    public class UsersController : BaseAdminController
    {
        private readonly IAdminUserService adminUserService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly ILogService logService;

        public UsersController(IAdminUserService adminUserService, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager, 
            ILogService logService)
        {
            this.adminUserService = adminUserService;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logService = logService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var totalUsers = await this.adminUserService.TotalAsync();

            if (page > (int)Math.Ceiling((double)totalUsers / UsersPageSize) || page < 1)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(new UserListingsViewModel
            {
                UsersWithRoles = await this.adminUserService.AllAsync(page),
                CurrentPage = page,
                TotalUsers = totalUsers
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

            var additionalInformation = user.UserName + " " + model.Role;
            await this.logService.CreateLogAsync(User.Identity.Name, LogType.AddToRole, additionalInformation);

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

            var additionalInformation = user.UserName + " " + model.Role;
            await this.logService.CreateLogAsync(User.Identity.Name, LogType.RemoveFromRole, additionalInformation);

            return RedirectToAction(nameof(Index));
        }
    }
}
