namespace NinjaSubs.Services.Subtitles.Models
{
    using AutoMapper;
    using NinjaSubs.Common.AutoMapper;
    using NinjaSubs.Data.Models;
    using NinjaSubs.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SubtitlesDetailsServiceModel : IMapFrom<Subtitles>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishDate { get; set; }

        public LanguageType Language { get; set; }

        public byte[] File { get; set; }

        public int DownloadCount { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public string Author { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Subtitles, SubtitlesDetailsServiceModel>()
                .ForMember(a => a.Author, cfg => cfg.MapFrom(a => a.Author.UserName))
                .ForMember(g => g.Genres, cfg => cfg.MapFrom(g => g.Genres.Select(x => x.Genre.Name)));
    }
}
