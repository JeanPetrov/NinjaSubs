namespace NinjaSubs.Services.Subtitles.Models
{
    using AutoMapper;
    using Data.Models;
    using NinjaSubs.Common.AutoMapper;
    using NinjaSubs.Data.Models.Enums;
    using System;

    public class SubtitlesListingServiceModel : IMapFrom<Subtitles>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }

        public LanguageType Language { get; set; }

        public byte[] File { get; set; }

        public int DownloadCount { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Article, SubtitlesListingServiceModel>()
                .ForMember(a => a.Author, cfg => cfg.MapFrom(a => a.Author.UserName));
    }
}
