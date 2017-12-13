namespace NinjaSubs.Services.Subtitles
{
    using NinjaSubs.Data.Models.Enums;
    using NinjaSubs.Services.Subtitles.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubtitlesService
    {
        Task<IEnumerable<SubtitlesListingServiceModel>> AllAsync(int page = 1);

        Task<IEnumerable<SubtitlesListingServiceModel>> FindeAsync(string searchText, LanguageType language);

        Task<int> TotalAsync();

        Task<SubtitlesDetailsServiceModel> ById(int id);

        Task CreateAsync(string title, string description, DateTime publishDate, LanguageType language , byte[] file, string authorId, string genres);

        Task DeleteSubtitlesAsync(int id);

        Task AddDownload(int id);
    }
}
