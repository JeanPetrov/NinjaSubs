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

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Subtitles> Subtitless { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<GenreSubtitles>()
                .HasKey(gs => new { gs.GenreId, gs.SubtitlesId });

            builder.Entity<Subtitles>()
                .HasMany(s => s.Genres)
                .WithOne(g => g.Subtitles)
                .HasForeignKey(g => g.SubtitlesId);

            builder.Entity<Genre>()
                .HasMany(g => g.Subtitles)
                .WithOne(s => s.Genre)
                .HasForeignKey(s => s.GenreId);

            builder.Entity<User>()
                .HasMany(u => u.Subtitles)
                .WithOne(s => s.Author)
                .HasForeignKey(s => s.AuthorId);

            builder.Entity<User>()
                .HasMany(u => u.Articles)
                .WithOne(a => a.Author)
                .HasForeignKey(a => a.AuthorId);

            base.OnModelCreating(builder);
        }
    }
}
