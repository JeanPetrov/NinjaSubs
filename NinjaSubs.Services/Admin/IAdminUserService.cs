namespace NinjaSubs.Services.Admin
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserWithRolesListingServiceModel>> AllAsync(int page = 1);

        Task<int> TotalAsync();
    }
}
