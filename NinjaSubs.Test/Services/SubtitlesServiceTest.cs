namespace NinjaSubs.Test.Services
{
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using FluentAssertions;
    using NinjaSubs.Services.Subtitles.Implementations;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class SubtitlesServiceTest
    {
        [Fact]
        public async Task AddNewSubtitlesSavesToDatabase()
        {
            // Arrange
            TestStartUp startUp = new TestStartUp();
            var db = startUp.GetDbContext();
            var subtitlesService = new SubtitlesService(db);

            // Act
            await subtitlesService.CreateAsync("Title", "Description", DateTime.UtcNow, LanguageType.Bulgarian, null,"1", "genres");

            // Assert
            Assert.True(db.Subtitless.Any(s => s.Title == "Title"));
        }

        [Fact]
        public async Task DeleteSubtitlesFromDatabase()
        {
            // Arrange
            TestStartUp startUp = new TestStartUp();
            var db = startUp.GetDbContext();
            this.PopulateDb(db);
            var subtitlesService = new SubtitlesService(db);

            // Act
            await subtitlesService.DeleteSubtitlesAsync(1);

            // Assert
            Assert.True(!db.Subtitless.Any(s => s.Id == 1));
        }

        [Fact]
        public async Task GetSubtitlesById()
        {
            // Arrange
            TestStartUp startUp = new TestStartUp();
            TestStartUp.InitializeMapper();
            var db = startUp.GetDbContext();
            this.PopulateDb(db);
            var subtitlesService = new SubtitlesService(db);

            // Act
            var result = await subtitlesService.ById(1);

            // Assert
            Assert.True(result.Id == 1);
        }

        [Fact]
        public async Task FindAsyncShouldReturnCorrectResultsWithFilterAndOrder()
        {
            // Arrange
            TestStartUp startUp = new TestStartUp();
            TestStartUp.InitializeMapper();
            var db = startUp.GetDbContext();

            PopulateDb(db);

            var subtitlesService = new SubtitlesService(db);

            // Act
            var result = await subtitlesService.FindeAsync("s", LanguageType.Bulgarian);

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 2 && r.ElementAt(1).Id == 1)
                .And
                .HaveCount(2);
        }

        [Fact]
        public async Task TopDownloadSubstitlesShouldReturnCorrectResultsWithOrder()
        {
            // Arrange
            TestStartUp startUp = new TestStartUp();
            TestStartUp.InitializeMapper();
            var db = startUp.GetDbContext();

            PopulateDb(db);

            var subtitlesService = new SubtitlesService(db);

            // Act
            var result = await subtitlesService.Top10DownloadSubs();

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 3 && r.ElementAt(1).Id == 2 && r.ElementAt(2).Id == 1)
                .And
                .HaveCount(3);
        }

        [Fact]
        public async Task TotalSubstitlesShouldReturnCorrectResult()
        {
            // Arrange
            TestStartUp startUp = new TestStartUp();
            var db = startUp.GetDbContext();

            PopulateDb(db);

            var subtitlesService = new SubtitlesService(db);

            // Act
            var result = await subtitlesService.TotalAsync();

            // Assert
            Assert.True(result == 3);
        }

        private void PopulateDb(NinjaSubsDbContext db)
        {
            db.Subtitless.Add(new Subtitles
            {
                Id = 1,
                Title = "Subs1",
                Description = "Description1",
                AuthorId = "Author1",
                PublishDate = DateTime.UtcNow,
                Language = LanguageType.Bulgarian,
                DownloadCount = 10,
                File = new byte[10],
                Author = new User
                {
                    Id = "Author1",
                    UserName = "Author1",
                    Email = "user@user.com"
                }
            });

            db.Subtitless.Add(new Subtitles
            {
                Id = 2,
                Title = "Subs2",
                Description = "Description2",
                AuthorId = "Author2",
                PublishDate = DateTime.UtcNow,
                Language = LanguageType.Bulgarian,
                DownloadCount = 20,
                File = new byte[20],
                Author = new User
                {
                    Id = "Author2",
                    UserName = "Author2",
                    Email = "user@user2.com"
                }
            });

            db.Subtitless.Add(new Subtitles
            {
                Id = 3,
                Title = "New3",
                Description = "Description3",
                AuthorId = "Author3",
                PublishDate = DateTime.UtcNow,
                Language = LanguageType.Bulgarian,
                DownloadCount = 30,
                File = new byte[30],
                Author = new User
                {
                    Id = "Author3",
                    UserName = "Author3",
                    Email = "user@user3.com"
                }
            });

            db.SaveChanges();
        }
    }
}
