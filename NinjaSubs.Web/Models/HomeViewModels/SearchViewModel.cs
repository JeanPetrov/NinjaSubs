namespace NinjaSubs.Web.Models.HomeViewModels
{
    using NinjaSubs.Data.Models.Enums;
    using NinjaSubs.Services.Subtitles.Models;
    using System.Collections.Generic;

    public class SearchViewModel
    {
        public string SearchText { get; set; }

        public LanguageType Language { get; set; }

        public IEnumerable<SubtitlesListingServiceModel> Subtitles { get; set; }
    }
}
