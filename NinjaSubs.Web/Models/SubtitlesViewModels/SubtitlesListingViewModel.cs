namespace NinjaSubs.Web.Models.SubtitlesViewModels
{
    using NinjaSubs.Services;
    using NinjaSubs.Services.Subtitles.Models;
    using System;
    using System.Collections.Generic;

    public class SubtitlesListingViewModel
    {
        public IEnumerable<SubtitlesListingServiceModel> Subtitles { get; set; }

        public int TotalSubtitles { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalSubtitles / ServiceConstants.SubtitlesPageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}
