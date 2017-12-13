namespace NinjaSubs.Web.Models.HomeViewModels
{
    using NinjaSubs.Services.Subtitles.Models;
    using System.Collections.Generic;

    public class HomeIndexViewModel : SearchFormModel
    {
        public IEnumerable<SubtitlesListingServiceModel> Subtitles { get; set; }
    }
}
