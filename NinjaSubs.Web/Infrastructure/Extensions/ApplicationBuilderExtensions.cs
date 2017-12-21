namespace NinjaSubs.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NinjaSubs.Data;
    using NinjaSubs.Data.Models;
    using System;
    using System.Threading.Tasks;

    using static WebConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<NinjaSubsDbContext>().Database.Migrate();
                
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();

                Task.Run(async () =>
                {
                    var adminRoleExists = await roleManager.RoleExistsAsync(AdminRole);
                    if (!adminRoleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(AdminRole));
                    }

                    var blogAuthorRoleExists = await roleManager.RoleExistsAsync(BlogAuthorRole);
                    if (!blogAuthorRoleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(BlogAuthorRole));
                    }

                    var translatorRoleExists = await roleManager.RoleExistsAsync(TranslatorRole);
                    if (!blogAuthorRoleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(TranslatorRole));
                    }

                    var adminUser = await userManager.FindByNameAsync("Admin");

                    if (adminUser == null)
                    {
                        adminUser = new User
                        {
                            Email = "Admin@admin.com",
                            UserName = "Admin",
                            Birthdate = DateTime.UtcNow,
                            FullName = "Admin admin"
                        };

                        await userManager.CreateAsync(adminUser, "Admin123");

                        await userManager.AddToRoleAsync(adminUser, AdminRole);
                    }

                }).Wait();
            }

            return app;
        }
    }
}
