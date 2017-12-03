namespace NinjaSubs.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NinjaSubs.Data;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<NinjaSubsDbContext>().Database.Migrate();
                
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                Task.Run(async () =>
                {
                    var adminRoleExists = await roleManager.RoleExistsAsync(WebConstants.AdminRole);
                    if (!adminRoleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(WebConstants.AdminRole));
                    }

                    var blogAuthorRoleExists = await roleManager.RoleExistsAsync(WebConstants.BlogAuthorRole);
                    if (!blogAuthorRoleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(WebConstants.BlogAuthorRole));
                    }

                    var translatorRoleExists = await roleManager.RoleExistsAsync(WebConstants.TranslatorRole);
                    if (!blogAuthorRoleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(WebConstants.TranslatorRole));
                    }

                }).Wait();
            }

            return app;
        }
    }
}
