namespace NinjaSubs.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Web.Controllers;

    using static WebConstants;

    [Area(AdminArea)]
    [Authorize(Roles = AdminRole)]
    public abstract class BaseAdminController : BaseGlobalController
    {
        private static ILogService logService;

        protected BaseAdminController() : base(logService)
        {
        }
    }
}
