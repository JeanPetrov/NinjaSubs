namespace NinjaSubs.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class NinjaSubsDbContext : IdentityDbContext<User>
    {
        public NinjaSubsDbContext(DbContextOptions<NinjaSubsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
