namespace NinjaSubs.Data.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Subtitles
    {
        public int Id { get; set; }

        [Required]
        [MinLength(SubtitlesTitleMinLength)]
        [MaxLength(SubtitlesTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(SubtitlesDescriptionMinLenght)]
        [MaxLength(SubtitlesDescriptionMaxLenght)]
        public string Description { get; set; }

        DateTime PublishDate { get; set; }

        public LanguageType Language { get; set; }

        public byte[] File { get; set; }

        public int DownloadCount { get; set; } = 0;

        public string AuthorId { get; set; }
        public User Author { get; set; }

        public IEnumerable<GenreSubtitles> Genres { get; set; } = new List<GenreSubtitles>();
    }
}
