namespace NinjaSubs.Services
{
    using Data.Models.Enums;
    using NinjaSubs.Services.Admin.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILogService
    {
        Task CreateLogAsync(string username, LogType type, string additionalInformation);

        Task<IEnumerable<LogServiceModel>> AllAsync();
    }
}
