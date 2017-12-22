namespace NinjaSubs.Test.Services
{
    using Data;
    using Data.Models;
    using FluentAssertions;
    using NinjaSubs.Services.Blog.Implementations;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class BlogArticleServiceTest
    {
        [Fact]
        public async Task CreateArticleSavesToDatabase()
        {
            // Arrange
            TestStartUp startUp = new TestStartUp();
            var db = startUp.GetDbContext();
            var articleService = new BlogArticleService(db);

            // Act
            await articleService.CreateAsync("Title", "Content", "1");

            // Assert
            Assert.True(db.Articles.Any(a => a.Title == "Title"));
        }

        [Fact]
        public async Task DeleteArticleFromDatabase()
        {
            // Arrange
            TestStartUp startUp = new TestStartUp();
            var db = startUp.GetDbContext();
            this.PopulateDb(db);
            var articleService = new BlogArticleService(db);

            // Act
            await articleService.DeleteArticleAsync(2);

            // Assert
            Assert.True(!db.Articles.Any(a => a.Id == 2));
        }

        [Fact]
        public async Task GetArticleById()
        {
            // Arrange
            TestStartUp startUp = new TestStartUp();
            TestStartUp.InitializeMapper();
            var db = startUp.GetDbContext();
            this.PopulateDb(db);
            var articleService = new BlogArticleService(db);

            // Act
            var result = await articleService.ById(2);

            // Assert
            Assert.True(result.Id == 2);
        }

        [Fact]
        public async Task UpdateArticleSaveToDatabase()
        {
            // Arrange
            TestStartUp startUp = new TestStartUp();
            TestStartUp.InitializeMapper();
            var db = startUp.GetDbContext();
            this.PopulateDb(db);
            var articleService = new BlogArticleService(db);

            // Act
            var article = await articleService.ById(2);
            await articleService.UpdateArticleAsync(article.Id, "New Title", article.Content);

            // Assert
            Assert.True(db.Articles.Any(a => a.Id == article.Id && a.Title == "New Title"));
        }

        [Fact]
        public async Task AllArticlesShouldReturnCorrectResult()
        {
            // Arrange
            TestStartUp startUp = new TestStartUp();
            TestStartUp.InitializeMapper();
            var db = startUp.GetDbContext();
            this.PopulateDb(db);
            var articleService = new BlogArticleService(db);

            // Act
            var result = await articleService.AllAsync();

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 4 && r.ElementAt(1).Id == 3 && r.ElementAt(2).Id == 2)
                .And
                .HaveCount(3);
        }


        private void PopulateDb(NinjaSubsDbContext db)
        {
            db.Articles.Add(new Article
            {
                Id = 1,
                Title = "Title1",
                Content = "Content1",
                PublishDate = DateTime.UtcNow,
                AuthorId = "Author1",
                Author = new User
                {
                    Id = "Author1",
                    UserName = "Author1",
                    Email = "user@user.com"
                }
            });

            db.Articles.Add(new Article
            {
                Id = 2,
                Title = "Title2",
                Content = "Content2",
                PublishDate = DateTime.UtcNow,
                AuthorId = "Author2",
                Author = new User
                {
                    Id = "Author2",
                    UserName = "Author2",
                    Email = "user@user2.com"
                }
            });

            db.Articles.Add(new Article
            {
                Id = 3,
                Title = "Title3",
                Content = "Content3",
                PublishDate = DateTime.UtcNow,
                AuthorId = "Author3",
                Author = new User
                {
                    Id = "Author3",
                    UserName = "Author3",
                    Email = "user@user3.com"
                }
            });

            db.Articles.Add(new Article
            {
                Id = 4,
                Title = "Title4",
                Content = "Content4",
                PublishDate = DateTime.UtcNow,
                AuthorId = "Author4",
                Author = new User
                {
                    Id = "Author4",
                    UserName = "Author4",
                    Email = "user@user4.com"
                }
            });

            db.SaveChanges();
        }
    }
}
