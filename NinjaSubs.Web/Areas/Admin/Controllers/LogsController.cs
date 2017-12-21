namespace NinjaSubs.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NinjaSubs.Services;
    using NinjaSubs.Web.Areas.Admin.Models.Logs;
    using System;
    using System.Threading.Tasks;

    using static Services.ServiceConstants;

    public class LogsController : BaseAdminController
    {
        private readonly ILogService logService;

        public LogsController(ILogService logService)
        {
            this.logService = logService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var totalLogs = await this.logService.TotalAsync();

            if (page > (int)Math.Ceiling((double)totalLogs / LogsPageSize) || page < 1)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(new LogListingsViewModel
            {
                Logs = await this.logService.AllAsync(page),
                TotalLogs = totalLogs,
                CurrentPage = page
            });
        }
    }
}
