namespace NinjaSubs.Services.Subtitles.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using NinjaSubs.Data;
    using NinjaSubs.Data.Models;
    using NinjaSubs.Data.Models.Enums;
    using NinjaSubs.Services.Subtitles.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static ServiceConstants;

    public class SubtitlesService : ISubtitlesService
    {
        private readonly NinjaSubsDbContext db;

        public SubtitlesService(NinjaSubsDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<SubtitlesListingServiceModel>> AllAsync(int page = 1)
            => await this.db
                .Subtitless
                .OrderByDescending(a => a.PublishDate)
                .Skip((page - 1) * SubtitlesPageSize)
                .Take(SubtitlesPageSize)
                .ProjectTo<SubtitlesListingServiceModel>()
                .ToListAsync();

        public async Task<IEnumerable<SubtitlesListingServiceModel>> FindeAsync(string searchText,LanguageType language)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                .Subtitless
                .OrderByDescending(a => a.PublishDate)
                .Where(s => s.Title.ToLower().Contains(searchText.ToLower()) && s.Language == language)
                .ProjectTo<SubtitlesListingServiceModel>()
                .ToListAsync();
        }

        public async Task<int> TotalAsync()
            => await this.db.Subtitless.CountAsync();

        public async Task<SubtitlesDetailsServiceModel> ById(int id)
           => await this.db
               .Subtitless
               .Where(a => a.Id == id)
               .ProjectTo<SubtitlesDetailsServiceModel>()
               .FirstOrDefaultAsync();

        public async Task CreateAsync(string title, string description, DateTime publishDate, LanguageType language, byte[] file, string authorId, string genres)
        {
            var subtitles = new Subtitles
            {
                Title = title,
                Description = description,
                PublishDate = publishDate,
                Language = language,
                File = file,
                AuthorId = authorId
            };

            var genresSplit = genres.Split(new[] { ' ', ',', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var genre in genresSplit)
            {
                var gen = this.db.Genres.FirstOrDefault(g => g.Name.ToLower() == genre.ToLower());

                if (gen == null)
                {
                    gen = new Genre
                    {
                        Name = genre
                    };

                    this.db.Add(gen);
                    await this.db.SaveChangesAsync();
                }

                subtitles.Genres.Add(new GenreSubtitles
                {
                    GenreId = gen.Id
                });
            }

            this.db.Add(subtitles);
            await this.db.SaveChangesAsync();
        }

        public async Task DeleteSubtitlesAsync(int id)
        {
            var subtitles = await this.db.Subtitless.FirstOrDefaultAsync(s => s.Id == id);

            if (subtitles == null)
            {
                return;
            }

            this.db.Subtitless.Remove(subtitles);

            await this.db.SaveChangesAsync();
        }

        public async Task AddDownload(int id)
        {
            var subtitles = await this.db.Subtitless.FirstOrDefaultAsync(s => s.Id == id);

            subtitles.DownloadCount++;

            await this.db.SaveChangesAsync();
        }
    }
}
