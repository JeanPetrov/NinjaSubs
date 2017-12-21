namespace NinjaSubs.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using NinjaSubs.Data;
    using NinjaSubs.Data.Models;
    using NinjaSubs.Services.Admin.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static ServiceConstants;

    public class AdminUserService : IAdminUserService
    {
        private readonly NinjaSubsDbContext db;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public AdminUserService(NinjaSubsDbContext db, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.db = db;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<AdminUserWithRolesListingServiceModel>> AllAsync(int page = 1)
        {
            var users = await this.db
                .Users
                .ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();

            var allRoles = await this.roleManager
                .Roles
                .Select(r => r.Name)
                .ToListAsync();

            var result = new List<AdminUserWithRolesListingServiceModel>();

            foreach (var user in users)
            {
                var userId = await userManager.FindByIdAsync(user.Id);
                var roles = await userManager.GetRolesAsync(userId);

                var rolesNotIn = allRoles.Except(roles);

                result.Add(new AdminUserWithRolesListingServiceModel
                {
                    User = user,
                    Roles = roles.Select(r => new SelectListItem
                    {
                        Text = r,
                        Value = r
                    }),
                    RolesNotIn = rolesNotIn.Select(r => new SelectListItem
                    {
                        Text = r,
                        Value = r
                    })
                });
            }
            
            return result.Skip((page - 1) * UsersPageSize).Take(UsersPageSize);
        }

        public async Task<int> TotalAsync()
            => await this.db.Users.CountAsync();
    }
}
