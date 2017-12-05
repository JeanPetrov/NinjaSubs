namespace NinjaSubs.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NinjaSubs.Services.Admin;
    using NinjaSubs.Web.Areas.Admin.Models.Users;
    using System.Threading.Tasks;

    public class UsersController : BaseAdminController
    {
        private readonly IAdminUserService adminUserService;

        public UsersController(IAdminUserService adminUserService)
        {
            this.adminUserService = adminUserService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.adminUserService.AllAsync();


            return View(new UserListingsViewModel
            {
                UsersWithRoles = users
            });
        }
    }
}
