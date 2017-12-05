namespace NinjaSubs.Services.Admin.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class AdminUserWithRolesListingServiceModel
    {
        public AdminUserListingServiceModel User { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

        public IEnumerable<SelectListItem> RolesNotIn { get; set; }
    }
}
