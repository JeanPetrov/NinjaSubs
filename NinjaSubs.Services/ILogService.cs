namespace NinjaSubs.Services
{
    using Data.Models.Enums;
    using NinjaSubs.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILogService
    {
        Task CreateLogAsync(string username, LogType type, string additionalInformation);

        Task<IEnumerable<LogServiceModel>> AllAsync(int page = 1);

        Task<int> TotalAsync();
    }
}
