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

        public DateTime PublishDate { get; set; }

        public LanguageType Language { get; set; }

        [Required]
        public byte[] File { get; set; }

        public int DownloadCount { get; set; } = 0;

        public string AuthorId { get; set; }
        public User Author { get; set; }

        public ICollection<GenreSubtitles> Genres { get; set; } = new List<GenreSubtitles>();
    }
}
