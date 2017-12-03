namespace NinjaSubs.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Genre
    {
        public int Id { get; set; }

        [Required]
        [MinLength(GenerNameMinLength)]
        [MaxLength(GenerNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<GenreSubtitles> Subtitles { get; set; } = new List<GenreSubtitles>();
    }
}
