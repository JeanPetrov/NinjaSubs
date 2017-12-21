namespace NinjaSubs.Web.Areas.Admin.Models.Logs
{
    using NinjaSubs.Services.Models;
    using System;
    using System.Collections.Generic;

    using static Services.ServiceConstants;

    public class LogListingsViewModel
    {
        public IEnumerable<LogServiceModel> Logs { get; set; }
        
        public int TotalLogs { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalLogs / LogsPageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}
