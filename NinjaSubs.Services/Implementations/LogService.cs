namespace NinjaSubs.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using NinjaSubs.Services.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static ServiceConstants;

    public class LogService : ILogService
    {
        private readonly NinjaSubsDbContext db;

        public LogService(NinjaSubsDbContext db)
        {
            this.db = db;
        }

        public async Task CreateLogAsync(string username, LogType type, string additionalInformation)
        {
            var log = new Log
            {
                UserName = username,
                Type = type,
                AdditionalInformation = additionalInformation
            };

            this.db.Add(log);
            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<LogServiceModel>> AllAsync(int page = 1)
            => await this.db
                .Logs
                .OrderByDescending(l => l.Id)
                .Skip((page - 1) * LogsPageSize)
                .Take(LogsPageSize)
                .ProjectTo<LogServiceModel>()
                .ToListAsync();

        public async Task<int> TotalAsync()
            => await this.db.Logs.CountAsync();
    }
}
