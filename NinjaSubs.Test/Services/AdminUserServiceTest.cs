namespace NinjaSubs.Test.Services
{
    using Data;
    using Data.Models;
    using NinjaSubs.Services.Admin.Implementations;
    using System.Threading.Tasks;
    using Xunit;

    public class AdminUserServiceTest
    {
        [Fact]
        public async Task TotalUsersShouldReturnCorrectResult()
        {
            // Arrange
            TestStartUp startUp = new TestStartUp();
            var db = startUp.GetDbContext();

            PopulateDb(db);

            var userService = new AdminUserService(db,null,null);

            // Act
            var result = await userService.TotalAsync();

            // Assert
            Assert.True(result == 6);
        }

        private void PopulateDb(NinjaSubsDbContext db)
        {
            db.Users.Add(new User
            {
                Id = "1",
                UserName = "User1"
            });

            db.Users.Add(new User
            {
                Id = "2",
                UserName = "User2"
            });

            db.Users.Add(new User
            {
                Id = "3",
                UserName = "User3"
            });

            db.Users.Add(new User
            {
                Id = "4",
                UserName = "User4"
            });

            db.Users.Add(new User
            {
                Id = "5",
                UserName = "User5"
            });

            db.Users.Add(new User
            {
                Id = "6",
                UserName = "User6"
            });
            
            db.SaveChanges();
        }
    }
}
