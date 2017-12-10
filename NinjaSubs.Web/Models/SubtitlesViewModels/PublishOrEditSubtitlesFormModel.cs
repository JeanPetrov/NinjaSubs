namespace NinjaSubs.Web.Models.SubtitlesViewModels
{
    using Microsoft.AspNetCore.Http;
    using NinjaSubs.Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class PublishOrEditSubtitlesFormModel
    {
        [Required]
        [MinLength(SubtitlesTitleMinLength)]
        [MaxLength(SubtitlesTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(SubtitlesDescriptionMinLenght)]
        [MaxLength(SubtitlesDescriptionMaxLenght)]
        public string Description { get; set; }

        [Required]
        public LanguageType Language { get; set; }

        [Required]
        public IFormFile File { get; set; }

        [Required]
        public string Genres { get; set; }
    }
}
