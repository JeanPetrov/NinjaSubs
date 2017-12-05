namespace NinjaSubs.Services.Admin
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserWithRolesListingServiceModel>> AllAsync();
    }
}
