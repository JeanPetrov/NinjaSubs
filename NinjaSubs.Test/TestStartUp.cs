namespace NinjaSubs.Test
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using NinjaSubs.Data;
    using NinjaSubs.Web.Infrastructure.Mapping;
    using System;

    public class TestStartUp
    {
        private static bool mapperInitialized = false;

        public static void InitializeMapper()
        {
            if (!mapperInitialized)
            {
                Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());

                mapperInitialized = true;
            }
        }

        public NinjaSubsDbContext GetDbContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<NinjaSubsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbContext = new NinjaSubsDbContext(dbContextOptions.Options);

            return dbContext;
        }
    }
}
