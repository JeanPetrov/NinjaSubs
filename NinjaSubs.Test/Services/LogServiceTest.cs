namespace NinjaSubs.Test.Services
{
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using FluentAssertions;
    using NinjaSubs.Services.Implementations;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class LogServiceTest
    {
        [Fact]
        public async Task CreateLogSavesToDatabase()
        {
            // Arrange
            TestStartUp startUp = new TestStartUp();
            var db = startUp.GetDbContext();
            var logService = new LogService(db);

            // Act
            await logService.CreateLogAsync("username", LogType.AddNewSubtitles, "additionalinformation");

            // Assert
            Assert.True(db.Logs.Any(l => l.UserName == "username"
                                    && l.Type == LogType.AddNewSubtitles
                                    && l.AdditionalInformation == "additionalinformation"));
        }

        [Fact]
        public async Task AllLogsShouldReturnCorrectResult()
        {
            // Arrange
            TestStartUp startUp = new TestStartUp();
            TestStartUp.InitializeMapper();
            var db = startUp.GetDbContext();
            this.PopulateDb(db);
            var logService = new LogService(db);

            // Act
            var result = await logService.AllAsync();

            // Assert
            result
                .Should()
                .Match(l => l.ElementAt(0).UserName == "user3" && l.ElementAt(1).UserName == "user2" && l.ElementAt(2).UserName == "user1")
                .And
                .HaveCount(3);
        }

        [Fact]
        public async Task TotalLogsShouldReturnCorrectResult()
        {
            // Arrange
            TestStartUp startUp = new TestStartUp();
            var db = startUp.GetDbContext();

            PopulateDb(db);

            var logService = new LogService(db);

            // Act
            var result = await logService.TotalAsync();

            // Assert
            Assert.True(result == 3);
        }

        private void PopulateDb(NinjaSubsDbContext db)
        {
            db.Logs.Add(new Log
            {
                Id = 1,
                UserName = "user1",
                Type = LogType.AddNewSubtitles,
                AdditionalInformation = "info1"
            });

            db.Logs.Add(new Log
            {
                Id = 2,
                UserName = "user2",
                Type = LogType.AddNewSubtitles,
                AdditionalInformation = "info2"
            });

            db.Logs.Add(new Log
            {
                Id = 3,
                UserName = "user3",
                Type = LogType.AddNewSubtitles,
                AdditionalInformation = "info3"
            });

            db.SaveChanges();
        }
    }
}
