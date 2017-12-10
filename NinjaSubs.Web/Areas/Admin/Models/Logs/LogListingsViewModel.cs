namespace NinjaSubs.Web.Areas.Admin.Models.Logs
{
    using NinjaSubs.Services.Models;
    using System.Collections.Generic;

    public class LogListingsViewModel
    {
        public IEnumerable<LogServiceModel> Logs { get; set; }
    }
}
