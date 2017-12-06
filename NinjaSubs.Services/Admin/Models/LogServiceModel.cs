namespace NinjaSubs.Services.Admin.Models
{
    using NinjaSubs.Common.AutoMapper;
    using NinjaSubs.Data.Models;
    using NinjaSubs.Data.Models.Enums;
    using System;

    public class LogServiceModel : IMapFrom<Log>
    {
        public string UserName { get; set; }

        public LogType Type { get; set; }

        public string AdditionalInformation { get; set; }

        public override string ToString()
        {
            string message = null;

            switch (this.Type)
            {
                case LogType.AddToRole:
                    message = $"added the {this.AdditionalInformation.Split(' ')[0]} to a {this.AdditionalInformation.Split(' ')[1]} role.";
                    break;
                case LogType.RemoveFromRole:
                    message = $"remove the {this.AdditionalInformation.Split(' ')[0]} from a {this.AdditionalInformation.Split(' ')[1]} role.";
                    break;
                default:
                    throw new InvalidOperationException($"Invalid log type: {this.Type}.");
            }

            return $"{this.UserName} {message}";
        }
    }
}
