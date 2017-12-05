namespace NinjaSubs.Web.Areas.Admin.Models.Users
{
    using NinjaSubs.Services.Admin.Models;
    using System.Collections.Generic;

    public class UserListingsViewModel
    {
        public IEnumerable<AdminUserWithRolesListingServiceModel> UsersWithRoles { get; set; }
    }
}
