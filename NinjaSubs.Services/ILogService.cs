namespace NinjaSubs.Services
{
    using Data.Models.Enums;
    using System.Threading.Tasks;

    public interface ILogService
    {
        Task CreateLogAsync(string username, LogType type, string additionalInformation);
    }
}
