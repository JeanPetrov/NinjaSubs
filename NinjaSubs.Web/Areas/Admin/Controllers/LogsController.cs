namespace NinjaSubs.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NinjaSubs.Services;
    using NinjaSubs.Web.Areas.Admin.Models.Logs;
    using System.Threading.Tasks;

    public class LogsController : BaseAdminController
    {
        private readonly ILogService logService;

        public LogsController(ILogService logService)
        {
            this.logService = logService;
        }

        public async Task<IActionResult> Index()
        {
            var logs = await this.logService.AllAsync();

            return View(new LogListingsViewModel
            {
                Logs = logs
            });
        }
    }
}
