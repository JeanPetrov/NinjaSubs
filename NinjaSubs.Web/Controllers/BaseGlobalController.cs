namespace NinjaSubs.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NinjaSubs.Data.Models.Enums;
    using NinjaSubs.Services;
    using System.Threading.Tasks;

    public abstract class BaseGlobalController : Controller
    {
        private readonly ILogService logService;

        protected BaseGlobalController(ILogService logService)
        {
            this.logService = logService;
        }

        protected async Task CreateLogAsync(LogType type, string additionalInformation)
            => await this.logService.CreateLogAsync(
                User.Identity.Name,
                type,
                additionalInformation
                );
    }
}
