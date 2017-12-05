namespace NinjaSubs.Services.Implementations
{
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using System.Threading.Tasks;

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
    }
}
