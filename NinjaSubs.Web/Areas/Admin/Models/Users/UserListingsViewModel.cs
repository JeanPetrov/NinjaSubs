namespace NinjaSubs.Web.Areas.Admin.Models.Users
{
    using NinjaSubs.Services.Admin.Models;
    using System;
    using System.Collections.Generic;

    using static Services.ServiceConstants;

    public class UserListingsViewModel
    {
        public IEnumerable<AdminUserWithRolesListingServiceModel> UsersWithRoles { get; set; }

        public int TotalUsers { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalUsers / UsersPageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}
