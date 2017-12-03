namespace NinjaSubs.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NinjaSubs.Data;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<NinjaSubsDbContext>().Database.Migrate();

                //var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                //Task.Run(async () =>
                //{
                //    var adminRoleExists = await roleManager.RoleExistsAsync(GlobalConstants.AdminRoleName);
                //    if (!adminRoleExists)
                //    {
                //        await roleManager.CreateAsync(new IdentityRole(GlobalConstants.AdminRoleName));
                //    }

                //    var studentRoleExists = await roleManager.RoleExistsAsync(GlobalConstants.StudentRoleName);
                //    if (!studentRoleExists)
                //    {
                //        await roleManager.CreateAsync(new IdentityRole(GlobalConstants.StudentRoleName));
                //    }

                //    var trainerRoleExists = await roleManager.RoleExistsAsync(GlobalConstants.TrainerRoleName);
                //    if (!trainerRoleExists)
                //    {
                //        await roleManager.CreateAsync(new IdentityRole(GlobalConstants.TrainerRoleName));
                //    }

                //    var blogAuthorRoleExists = await roleManager.RoleExistsAsync(GlobalConstants.BlogAuthorRolename);
                //    if (!blogAuthorRoleExists)
                //    {
                //        await roleManager.CreateAsync(new IdentityRole(GlobalConstants.BlogAuthorRolename));
                //    }
                //}).Wait();
            }

            return app;
        }
    }
}
